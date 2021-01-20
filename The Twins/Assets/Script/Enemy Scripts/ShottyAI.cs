using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool triggered;
    private float currentAttackDuration = 0.5f;
    void Start()
    {
        rigidbodya = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (triggered)
        {
            bulletTimer += Time.deltaTime;
            playerPos = player.GetComponent<Transform>().position;

            Vector2 playerDir = UsefulllFs.Dir(playerPos, transform.position, true);

            rigidbodya.velocity = new Vector2(-playerDir.x, -playerDir.y) * stats.moveSpeed;

            Vector2 direction = -playerDir;
            rigidbodya.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (bulletTimer > currentAttackDuration && gameObject.GetComponent<StatsHolder>().ableToAttack == true)
            {
                gameObject.GetComponent<StatsHolder>().ableToAttack = false;
                bulletTimer = 0;
                currentAttackDuration = gameObject.GetComponent<AtkPatterns>().Attack(0);
                
            }
        }
    }
}
