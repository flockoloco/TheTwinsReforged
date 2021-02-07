using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RunnerAI : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerPos;
    private Rigidbody2D rigidbody;
    public GameObject BulletPrefab;
    public StatsHolder stats;
    private readonly float agroDist = 4;
    private float bulletTimer;
    public Transform FirePoint;
    public Transform Pivot;
    public bool triggered;
    private float currentAttackDuration = 0.5f;

    private Animator animator;

    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (triggered)
        {
            if (stats.health <= 0)
            {
                animator.SetBool("dead", true);
                animator.SetBool("moving", false);
                animator.SetBool("attack", false);
                animator.SetBool("hit", false);
            }
            if (animator.GetBool("dead") == false)
            {
                bulletTimer += Time.deltaTime;
                playerPos = player.GetComponent<Transform>().position;


                Vector2 playerDir = UsefulllFs.Dir(playerPos, transform.position, true);
                Vector2 direction = playerDir;
                Quaternion rotato = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg);
                Pivot.transform.rotation = rotato;

                if (UsefulllFs.Dist(playerPos, transform.position) < agroDist) //when running away, he faces away from the player and moves away
                {
                    rigidbody.velocity = new Vector2(playerDir.x, playerDir.y) * stats.moveSpeed;

                    animator.SetBool("moving", true);
                }
                else //attacking cuz hes far away enough
                {
                    rigidbody.velocity = new Vector2(0, 0);

                    animator.SetBool("moving", false);
                    if (bulletTimer > currentAttackDuration && gameObject.GetComponent<StatsHolder>().ableToAttack == true)
                    {
                        animator.SetBool("attack", true);
                        bulletTimer = 0;
                        currentAttackDuration = gameObject.GetComponent<AtkPatterns>().Attack(0);

                    }
                }
                if (rigidbody.velocity.x >= 0.01f)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
                else if (rigidbody.velocity.x <= 0.01f)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
            }
        }
    }
}
