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

    public  float currentAttackDuration;
    public int attack = 0;
    public bool duringAttack = false;
    public  bool attackFired = false;
    public bool charging = false;
    public bool duringSwing = false;
    public Vector2 chargeDirection = new Vector2(0,0);

    public float dodgeSpeed = 10f;

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
                attack = PickAttack(attack); // choosing next attack
                duringAttack = true;
                attackFired = false;
                charging = false;
                duringSwing = false;
            }
            else if (duringAttack == true)
            {
                if (attack == 0 || attack == 1)  //atk 0 is a charge towards the player after charging for a bit
                {
                    bulletTimer += Time.deltaTime;
                    if (attackFired == false) // atk part
                    {
                        attackFired = true;
                        currentAttackDuration = 2;
                        Vector2 playerDir = UsefulllFs.Dir(playerPos, transform.position, true);

                        chargeDirection = playerDir;
                        dodgeSpeed = 10f;
                    }

                    if (bulletTimer < 0.2f) // few frames of getting ready
                    {
                        Debug.Log("angry");
                        rigidbody.velocity = new Vector2(0, 0);
                        gameObject.GetComponent<SpriteRenderer>().color =new Color(255, 0, 0);
                    }
                    if (bulletTimer > 0.2f && bulletTimer < 2f) //charge towards the player
                    {
                        Debug.Log("devia tar a atackar");
                        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                        charging = true;
                        
                        float dodgeSpeedDropper = 4f;
                        dodgeSpeed -=  dodgeSpeedDropper * Time.deltaTime;

                        float dodgeSpeedMin = 5f;

                        if (dodgeSpeed < dodgeSpeedMin)
                        {
                            StopCharging();
                        }
                        rigidbody.velocity =  - chargeDirection * dodgeSpeed;

                    }
                }
                if (attack == 2) //being "stunned" for 5 secs
                {
                    bulletTimer += Time.deltaTime;
                    if (attackFired == false)
                    {
                        attackFired = true;
                        currentAttackDuration = 5;
                    }
                    //player stun anim
                    rigidbody.velocity = new Vector2(0, 0);
                }
                if (attack == 3) //walking towards the player and swinging the axe when in range
                {
                    bulletTimer += Time.deltaTime;
                    if (attackFired == false)
                    {
                        attackFired = true;
                        currentAttackDuration = 4;
                    }

                    if (duringSwing == false) //walking towards the player
                    {
                        Vector2 playerDir = UsefulllFs.Dir(playerPos, transform.position, true);
                        rigidbody.velocity = new Vector2(playerDir.x, playerDir.y) * stats.moveSpeed;
                    }
                    else if (duringSwing == true) //not moving due to swinging
                    {
                        rigidbody.velocity = new Vector2(0, 0);
                    }
                    
                    if ( Vector2.Distance(player.transform.position,gameObject.transform.position) < gameObject.GetComponent<StatsHolder>().agroRange){

                        duringSwing = true;

                           //do swing
                    }
                }
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerStats>().invunerable == false)
        {
            collision.gameObject.GetComponent<PlayerStats>().invunerable = true;
            UsefulllFs.TakeDamage(collision.gameObject, stats.damage);
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
        duringSwing = false;
    }
    public void StopCharging()
    {
        rigidbody.velocity = new Vector2(0,0);
        duringAttack = false;
    
        charging = false;
        duringSwing = false;
    }

    private int PickAttack(int currentAttack)
    {
        if (currentAttack == 0)
        {
            return 1;
        }
        if (currentAttack == 1)
        {
            return 3;
        }
        return (0);
    }
}