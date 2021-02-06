﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TankAI : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerPos;
    private Rigidbody2D rigidbody; 
    public StatsHolder stats;
    private readonly float agroDist;
    private float bulletTimer;
    public Transform FirePoint;
    public Transform Pivot;
    public bool triggered;
    private float currentAttackDuration = 0.5f;

    private Animator animator;

    public AIPath aiPath;
    public AIDestinationSetter destinationsettler;

    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if (destinationsettler.enabled == true)
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
        }
    }

    void FixedUpdate()
    {
        if (triggered)
        {
            bulletTimer += Time.deltaTime;
            playerPos = player.GetComponent<Transform>().position;
            Vector2 playerDir = UsefulllFs.Dir(playerPos, transform.position, true);

            //rigidbody.velocity = new Vector2(-playerDir.x * stats.moveSpeed, -playerDir.y * stats.moveSpeed);

            Vector2 direction = -playerDir;
            Quaternion rotato = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg);
            Pivot.transform.rotation = rotato;
            if (bulletTimer > currentAttackDuration && gameObject.GetComponent<StatsHolder>().ableToAttack == true)
            {
                gameObject.GetComponent<StatsHolder>().ableToAttack = false;
                bulletTimer = 0;
                currentAttackDuration = gameObject.GetComponent<AtkPatterns>().Attack(0);

            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerStats>().invunerable == false)
        {
            animator.SetBool("attack", true);
            UsefulllFs.TakeDamage(collision.gameObject, stats.damage);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * 0; //reseting velocity right before doing the knockback

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.GetComponent<Rigidbody2D>().velocity.normalized * 5, ForceMode2D.Impulse); //knocking the target towards the enemy velocity
        }
        else
        {
            animator.SetBool("attack", false);
        }
    }
}

