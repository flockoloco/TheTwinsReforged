﻿using UnityEngine;
using Pathfinding;

public class Boss1Ai : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerPos;
    private Rigidbody2D rigidbody;
    public StatsHolder stats;
    private readonly float agroDist = 4;
    private float bulletTimer;
    public Transform FirePoint;
    public Transform Pivot;
    public bool triggered;
    public float fightDuration = 0;
    private float currentAttackDuration;
    public int attack = 0;
    public bool duringAttack = false;
    private bool attackFired = false;

    private Animator animator;

    //public AIPath aiPath;
    //public AIDestinationSetter destinationSetter;

    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }
    private void Update()
    {  
        if(stats.health <= 0)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        /*
        if (destinationSetter.enabled == true)
        {
            animator.SetBool("moving", true);
            if (aiPath.desiredVelocity.x >= 0.01f)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (aiPath.desiredVelocity.x <= 0.01f)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else
        {
            animator.SetBool("moving", false);
        }   */
    }

    private void FixedUpdate()
    {
        if (triggered)
        {
            if (playerPos.x > gameObject.transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (playerPos.x < gameObject.transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            fightDuration += Time.deltaTime;
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
                        animator.SetBool("attack", true);
                        attackFired = true;
                        currentAttackDuration = gameObject.GetComponent<AtkPatterns>().Attack(0);
                    }
                    Quaternion rotatoBoss = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg);
                    Pivot.transform.rotation = rotatoBoss;
                    if (UsefulllFs.Dist(playerPos, transform.position) < agroDist) // movement part
                    {
                        animator.SetBool("moving", true);
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
                        animator.SetBool("attack", true);
                        attackFired = true;
                        currentAttackDuration = gameObject.GetComponent<AtkPatterns>().Attack(1);
                    }
                    Quaternion rotatoBoss = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg);
                    Pivot.transform.rotation = rotatoBoss;

                    if (UsefulllFs.Dist(playerPos, transform.position) > agroDist/1.5)//movement part
                    {
                        animator.SetBool("moving", true);
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