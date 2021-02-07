using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Ai : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerPos;
    private Rigidbody2D rigidbody;
    public StatsHolder stats;
    public float bulletTimer;
    public Transform FirePoint;
    public bool triggered;
    public float fightDuration = 0;
    public  float currentAttackDuration;
    public int attack = 0;
    public bool duringAttack = false;
    public  bool attackFired = false;
    public bool charging = false;
    public bool duringSwing = false;
    public Vector2 chargeDirection = new Vector2(0,0);

    public float dodgeSpeed = 10f;
    private Animator animator;

    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(stats.health <= 0)
        {
            animator.SetBool("dead", true);
            animator.SetBool("moving", false);
            animator.SetBool("attack", false);
            animator.SetBool("charge", false);
            animator.SetBool("hit", false);
        }
       
    }

    private void FixedUpdate()
    {
        if (triggered)
        {
            fightDuration += Time.deltaTime;
            playerPos = player.GetComponent<Transform>().position;

            if (duringSwing == false)
            {
                if (bulletTimer > currentAttackDuration)
                {
                    bulletTimer = 0;
                    duringAttack = false;
                }
            }

            if (duringAttack == false)
            {
                //gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                attack = PickAttack(attack); // choosing next attack
                duringAttack = true;
                attackFired = false;
                charging = false;
                duringSwing = false;
                bulletTimer = 0;
            }
            else if (duringAttack == true)
            {
                if (attack == 0 || attack == 1)  //atk 0 is a charge towards the player after charging for a bit
                {
                    animator.SetBool("moving", false);
                    bulletTimer += Time.deltaTime;
                    if (attackFired == false) // atk part
                    {
                        attackFired = true;
                        currentAttackDuration = 2;
                        Vector2 playerDir = UsefulllFs.Dir(playerPos, transform.position, true);

                        chargeDirection = playerDir;
                        dodgeSpeed = 10f;
                        if (playerPos.x > gameObject.transform.position.x)
                        {
                            gameObject.GetComponent<SpriteRenderer>().flipX = false;
                            gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.5008869f, -0.05444527f);
                        }
                        else if (playerPos.x < gameObject.transform.position.x)
                        {
                            gameObject.GetComponent<SpriteRenderer>().flipX = true;
                            gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0.44f, 0.05444527f) ;
                        }
                    }

                    if (bulletTimer < 0.5f) // few frames of getting ready
                    {
                        Debug.Log("angry");
                        rigidbody.velocity = new Vector2(0, 0);
                        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                        animator.SetBool("charge", true);
                    }
                    if (bulletTimer > 0.5f && bulletTimer < 2f) //charge towards the player
                    {
                        animator.SetBool("charge", false);
                        animator.SetBool("moving", true);
                        Debug.Log("devia tar a atackar");
                        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                        charging = true;

                        float dodgeSpeedDropper = 4f;
                        dodgeSpeed -=  dodgeSpeedDropper * Time.deltaTime;

                        float dodgeSpeedMin = 6.5f;

                        if (dodgeSpeed < dodgeSpeedMin)
                        {
                            StopCharging();
                        }
                        rigidbody.velocity =  - chargeDirection * dodgeSpeed;

                    }
                }
                if (attack == 2) //being "stunned" for 5 secs
                {
                    animator.SetBool("moving", false);
                    bulletTimer += Time.deltaTime;
                    gameObject.GetComponent<SpriteRenderer>().color = new Color (255, 255, 0);
                    if (attackFired == false)
                    {
                        attackFired = true;
                        currentAttackDuration = 3;
                    }
                    //player stun anim
                    rigidbody.velocity = new Vector2(0, 0);
                }
                if (attack == 3 || attack == 4) //walking towards the player and swinging the axe when in range
                {
                    bulletTimer += Time.deltaTime;
                    if (attackFired == false)
                    {
                        attackFired = true;
                        currentAttackDuration = 4;
                    }

                    if (duringSwing == false) //walking towards the player
                    {
                        animator.SetBool("moving", true);
                        Vector2 playerDir = UsefulllFs.Dir(playerPos, transform.position, true);
                        rigidbody.velocity = - new Vector2(playerDir.x, playerDir.y) * stats.moveSpeed;
                        if (playerPos.x > gameObject.transform.position.x)
                        {
                            gameObject.GetComponent<SpriteRenderer>().flipX = false;
                            gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.5008869f, -0.05444527f);
                        }
                        else if (playerPos.x < gameObject.transform.position.x)
                        {
                            gameObject.GetComponent<SpriteRenderer>().flipX = true;
                            gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0.44f, 0.05444527f);
                        }
                    }
                    else if (duringSwing == true) //not moving due to swinging
                    {
                        animator.SetBool("moving", false);
                        animator.SetBool("attack", true);
                        rigidbody.velocity = new Vector2(0, 0);
                    }
                    
                    if ( Vector2.Distance(player.transform.position,gameObject.transform.position) < gameObject.GetComponent<StatsHolder>().agroRange)
                    {
                        duringSwing = true;
                        //do swing
                    }
                }
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (attack != 2)
        {
            if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerStats>().invunerable == false)
            {
                collision.gameObject.GetComponent<PlayerStats>().invunerable = true;
                UsefulllFs.TakeDamage(collision.gameObject, stats.damage);
            }
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" && charging == true) //forces the boss into atk1 
        {
            Debug.Log("nao tas aqui pois nao?");
            attack = 2;
            duringAttack = true;
            attackFired = false;
            charging = false;
            bulletTimer = 0;
        }
    }
    public void FinishSwing()
    {
        attack = PickAttack(attack);
        duringAttack = true;
        attackFired = false;
        charging = false;
        bulletTimer = 0;
        duringSwing = false;
        animator.SetBool("attack", false);
    }
    public void StopCharging()
    {
        rigidbody.velocity = new Vector2(0,0);
        duringAttack = false;
        charging = false;
        bulletTimer = 0;
        duringSwing = false;
        animator.SetBool("moving", false);
    }

    private int PickAttack(int currentAttack)
    {
        if (currentAttack == 0)
        {
            return 1;
        }
        else if (currentAttack == 1)
        {
            return 3;
        }
        else if (currentAttack == 3)
        {
            return 4;
        }
        return (0);
    }
}