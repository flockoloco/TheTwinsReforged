using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ShottyAI : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerPos;
    private Rigidbody2D rigidbodya;
    public GameObject BulletPrefab;
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
        rigidbodya = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (triggered)
        {
            if (aiPath.desiredVelocity.x >= 0.01f)
            {
                animator.SetBool("moving", true);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (aiPath.desiredVelocity.x <= 0.01f)
            {
                animator.SetBool("moving", true);
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }

            bulletTimer += Time.deltaTime;
            playerPos = player.GetComponent<Transform>().position;

            Vector2 playerDir = UsefulllFs.Dir(playerPos, transform.position, true);

            //rigidbodya.velocity = new Vector2(-playerDir.x, -playerDir.y) * stats.moveSpeed;

            Vector2 direction = playerDir;
            Quaternion rotato = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg);
            Pivot.transform.rotation = rotato;
            if (bulletTimer > currentAttackDuration && gameObject.GetComponent<StatsHolder>().ableToAttack == true)
            {
                animator.SetBool("moving", false);
                gameObject.GetComponent<StatsHolder>().ableToAttack = false;
                bulletTimer = 0;
                currentAttackDuration = gameObject.GetComponent<AtkPatterns>().Attack(0);
                animator.SetBool("attack", true);
            }
        }
    }
}
