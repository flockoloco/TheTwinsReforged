using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 6f;
    private Rigidbody2D rb;
    private Animator animator;

    public GameObject spawnRoom;

    private Vector3 moveDirection;
    private Vector3 dodgeDirection;
    private float dodgeSpeed;
    private float dodgeTimer = 0f;
    public GameObject playersword;
    public GameObject playerbow;
    public swordscript swordScript;
    public BowScript bowScript;


    public GameObject UICanvas;
    public SpawnPoint RoomSpawn;

    public enum weaponState
    {
        sword,
        bow,
    }

    private weaponState weapon;

    // define a type of state
    public enum State
    {
        walking,
        dodging,
        hit,
    }

    public State state;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        state = State.walking;
        weapon = weaponState.sword;
        playersword.SetActive(true);
        playerbow.SetActive(false);
        gameObject.GetComponent<PlayerStats>().EquipItem("Sword", gameObject.GetComponent<PlayerStats>().equippedSword.id);
    }

    public void PlayerTeleport()
    {
        spawnRoom = GameObject.FindWithTag("Respawn");
        transform.position = spawnRoom.transform.position;
    }

    private void Update()
    {
        if (gameObject.GetComponent<PlayerStats>().shopOpen == false)
        {
            if (Input.GetKeyDown(KeyCode.E) && gameObject.GetComponent<PlayerStats>().healthPotions >= 1)
            {
                gameObject.GetComponent<PlayerStats>().UseHealthPotion();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                gameObject.GetComponent<PlayerStats>().selectedArrow = 0;
                UICanvas.GetComponent<UITextManager>().CheckArrow();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                gameObject.GetComponent<PlayerStats>().selectedArrow = 1;
                UICanvas.GetComponent<UITextManager>().CheckArrow();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (weapon == weaponState.sword && swordScript.rotatoFrezeto == false)
                {
                    weapon = weaponState.bow;
                    playersword.SetActive(false);
                    playerbow.SetActive(true);
                }
                else if (weapon == weaponState.bow && bowScript.rotatoFrezeto == false)
                {
                    weapon = weaponState.sword;
                    playersword.SetActive(true);
                    playerbow.SetActive(false);
                    gameObject.GetComponent<PlayerStats>().EquipItem("Sword", gameObject.GetComponent<PlayerStats>().equippedSword.id);
                }
            }
        }

        //switch between the walking state to
        switch (state)
        {
            case State.walking:

                dodgeTimer -= Time.deltaTime;

                float moveX = 0f;
                float moveY = 0f;

                moveX = Input.GetAxisRaw("Horizontal");
                moveY = Input.GetAxisRaw("Vertical");

                moveDirection = new Vector3(moveX, moveY).normalized;

                animator.SetFloat("Vertical", moveY);
                animator.SetFloat("Horizontal", moveX);
                animator.SetFloat("Speed", moveDirection.sqrMagnitude);

                if (Input.GetKeyDown(KeyCode.Space) && (dodgeTimer <= 0) && (moveX != 0 || moveY != 0))
                {
                    dodgeDirection = moveDirection;
                    dodgeSpeed = 40f;
                    state = State.dodging;
                    dodgeTimer = 2f;
                }

                break;

            //this dodging state
            case State.dodging:
                //transicao para estado de walking enquanto diminui a velocidade do dodge

                gameObject.GetComponent<PlayerStats>().invunerable = true;

                float dodgeSpeedDropper = 2f;
                dodgeSpeed -= dodgeSpeed * dodgeSpeedDropper * Time.deltaTime;

                float dodgeSpeedMin = 30f;
                if (dodgeSpeed < dodgeSpeedMin)
                {
                    gameObject.GetComponent<PlayerStats>().invunerable = false;
                    state = State.walking;
                }
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.hit:
                break;

            case State.walking:
                rb.velocity = moveDirection * moveSpeed;
                break;

            case State.dodging:
                rb.velocity = dodgeDirection * dodgeSpeed;
                break;
        }
    }
}