using UnityEngine;

public class Boss1Ai : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerPos;
    private Rigidbody2D rigidbody;
    public StatsHolder stats;
    private readonly float agroDist = 4;
    private float bulletTimer;
    public Transform FirePoint;
    public bool triggered;

    private float currentAttackDuration;
    public int attack = 0;
    public bool duringAttack = false;
    private bool attackFired = false;

    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (triggered)
        {
            playerPos = player.GetComponent<Transform>().position;

            Vector2 playerDir = UsefulllFs.Dir(playerPos, transform.position, true);
            Vector2 direction = playerDir;
            if (bulletTimer > currentAttackDuration)
            {
                bulletTimer = 0;
                duringAttack = false;
            }
            if (duringAttack == false)
            {
                attack = PickAttack(attack); // Random 1 - 2 do attack
                duringAttack = true;
                attackFired = false;
            }

            if (duringAttack == true) //moving towards the player using big machine gun
            {
                if (attack == 0) //atk 0
                {
                    bulletTimer += Time.deltaTime;
                    if (attackFired == false) // atk part
                    {
                        attackFired = true;
                        currentAttackDuration = gameObject.GetComponent<AtkPatterns>().Attack(0);
                    }
                        rigidbody.rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
                    if (UsefulllFs.Dist(playerPos, transform.position) < agroDist) // movement part
                    {
                        
                        rigidbody.velocity = new Vector2(playerDir.x, playerDir.y) * stats.moveSpeed;
                        
                    }
                    else
                    {
                        rigidbody.velocity = new Vector2(0, 0);
                    }
                }
                if (attack == 1) //atk 1
                {
                    bulletTimer += Time.deltaTime;

                    if (attackFired == false) //atk part
                    {
                        attackFired = true;
                        currentAttackDuration = gameObject.GetComponent<AtkPatterns>().Attack(1);
                    }
                    rigidbody.rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;

                    if (UsefulllFs.Dist(playerPos, transform.position) > agroDist/1.5)//movement part
                    {
                       
                        rigidbody.velocity = new Vector2(playerDir.x, playerDir.y) * -stats.moveSpeed;
                    }
                    else
                    {
                        rigidbody.velocity = new Vector2(0, 0);
                    }
                }
            }
        }
    }

    private int PickAttack(int currentAttack)
    {
        if (currentAttack == 0)
        {
            return 1;
        }
        else if (currentAttack == 1)
        {
            return 0;
        }
        else
        {
            return 0;
        }
    }
}