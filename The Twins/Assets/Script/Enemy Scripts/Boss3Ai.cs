using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Ai : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerPos;
    private Rigidbody2D rigidbody;
    public StatsHolder stats;
    private float bulletTimer;
    public Transform FirePoint;
    public bool triggered;

    private float currentAttackDuration;
    public int attack = 0;
    public bool duringAttack = false;
    private bool attackFired = false;
    public bool charging = false;
    public bool duringSwing = false;

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

            Vector2 playerDir = UsefulllFs.Dir(playerPos, transform.position, true);
            Vector2 direction = playerDir;
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
                    }


                    if (currentAttackDuration < 0.2f) // few frames of getting ready
                    {
                        rigidbody.velocity = new Vector2(0, 0);
                    }
                    if (currentAttackDuration > 0.2f && currentAttackDuration < 2f) //charge towards the player
                    {
                        charging = true;
                        
                        float dodgeSpeedDropper = 2f;
                        dodgeSpeed -= dodgeSpeed * dodgeSpeedDropper * Time.deltaTime;

                        float dodgeSpeedMin = 30f;

                        if (dodgeSpeed < dodgeSpeedMin)
                        {
                            gameObject.GetComponent<PlayerStats>().invunerable = false;
                            StopCharging();
                        }
                        rigidbody.velocity = new Vector2(playerDir.x, playerDir.y) * dodgeSpeed;

                    }
                    rigidbody.velocity = Vector2.down * stats.moveSpeed;
                }
                if (attack == 2) //being "stunned" for 5 secs
                {
                    bulletTimer += Time.deltaTime;
                    if (attackFired == false)
                    {
                        attackFired = true;
                        currentAttackDuration = 5;
                    }

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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerStats>().invunerable == false)
        {
            UsefulllFs.TakeDamage(collision.gameObject, stats.damage);
        }
        if (collision.gameObject.tag == "Wall" && charging == true) //forces the boss into atk1 
        {
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
        attack = PickAttack(attack);
        duringAttack = true;
        attackFired = false;
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