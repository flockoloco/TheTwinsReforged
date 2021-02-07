using UnityEngine;
using Pathfinding;

public class StatsHolder : MonoBehaviour
{
    public float health;
    public int armor;
    public int damage;
    public float moveSpeed;
    public float bulletSpd;
    public bool invunerable;
    private float vunerableTimer;
    public bool hit;
    public int lootTier;
    public int agroRange;
    public GameObject goldPrefab;
    public GameObject nuggetsPrefab;
    private bool triggered;
    public bool ableToAttack = true;
    public int enemyID;

    private Animator animator;
    public GameObject roomIn;
    public CameraSoundChanger sound;
    public AudioClip defaultMusic;

    private void Start()
    {
        animator = GetComponent<Animator>();
        sound = FindObjectOfType<CameraSoundChanger>();
        ScaleEnemyStats(GameObject.FindWithTag("Player").GetComponent<PlayerStats>().currentLevel, enemyID);
    }

    private void Update()
    {
        if (health <= 0)
        {
            if(gameObject.GetComponent<StatsHolder>().enemyID == 6 || gameObject.GetComponent<StatsHolder>().enemyID == 7 || gameObject.GetComponent<StatsHolder>().enemyID == 8 || gameObject.GetComponent<StatsHolder>().enemyID == 2)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
            else if(gameObject.GetComponent<StatsHolder>().enemyID == 5 || gameObject.GetComponent<StatsHolder>().enemyID == 1 || gameObject.GetComponent<StatsHolder>().enemyID == 3 || gameObject.GetComponent<StatsHolder>().enemyID == 4)
            {
                gameObject.GetComponent<AIPath>().maxSpeed = 0f;
            }

            animator.SetBool("hit", false);
            if (gameObject.GetComponent<StatsHolder>().enemyID == 4)
            {
                animator.SetBool("attack2", false);
            }
            animator.SetBool("dead", true);
            if (enemyID > 5)
            {
                GameObject.FindWithTag("UICanvas").GetComponent<UITextManager>().BossDied();
            }
            roomIn.GetComponent<RoomDoorScript>().RemoveEnemy(gameObject);
        }
        if (hit == true)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(155, 0, 0);
                
            animator.SetBool("hit", true);
            invunerable = true;
            vunerableTimer += Time.deltaTime;
            if (vunerableTimer > 1)
            {
                animator.SetBool("hit", false);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                hit = false;
                invunerable = false;
                vunerableTimer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Room")
        {
            roomIn = collision.gameObject;
        }
    }

    private void SpawnDrops(int tier, Transform enemyTransform)
    {
        int randomNumberGold = Random.Range(7 * tier, 10 * tier);
        if (randomNumberGold > 0)
        {
            GameObject goldDrop = Instantiate(goldPrefab, enemyTransform.position, Quaternion.identity);
            goldDrop.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f)), ForceMode2D.Impulse);
            goldDrop.GetComponent<DropableScript>().Value(randomNumberGold);
        }
        int randomNumberNuggets = Random.Range(4 * tier, 8 * tier);
        if (randomNumberNuggets > 0)
        {
            Debug.Log("hello :aaaa");
            GameObject nuggetsDrop = Instantiate(nuggetsPrefab, enemyTransform.position, Quaternion.identity);
            nuggetsDrop.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f)), ForceMode2D.Impulse);
            nuggetsDrop.GetComponent<DropableScript>().Value(randomNumberNuggets);
        }
    }

    public void destroyObject()
    {
        SpawnDrops(lootTier, gameObject.transform);
        sound.changeAudio(defaultMusic);
        Destroy(gameObject);
    }
    public void ScaleEnemyStats(int currentlvl, int enemyid)
    {
        
        if (currentlvl == 2)
        {
            //stats scaling independent on the enemy
            if (enemyid < 6)
            {
                health += 5;
                damage += 2;
            }
            
        }
        else if (currentlvl == 3) //doesnt exist rn
        {
            if (enemyid < 6)
            {
                health += 10;
                damage += 4;
            }
        }
    }

}