using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAI : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerPos;
    private Rigidbody2D rigidbody;
    public GameObject BulletPrefab;
    public StatsHolder stats;
    private float bulletTimer;
    public Transform FirePoint;
    public bool triggered;
    private float currentAttackDuration = 0.5f;

    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");

        //updating the level the enemy is in, and changing some values due to it
        ScaleEnemyStats(player.GetComponent<PlayerStats>().currentLevel, stats.enemyID);
    }
    private void ScaleEnemyStats(int currentlvl,int enemyid)
    {
        if (currentlvl == 0)
        {
            //leave the stats the same
        }
        else if (currentlvl == 1)
        {
            //stats scaling independent on the enemy
            stats.health += 5;
            stats.damage += 2;

             // if we want to make stats scaling dependent on the enemies
            /*if (enemyid == 1)
            {
                stats.health += 5;
            }
            else if (enemyid == 2)
            {
                stats.health += 5;
            }
            else if (enemyid == 3)
            {

            }
            else if (enemyid == 4)
            {

            }
            else if (enemyid == 5)
            {

            }*/
        }
        else if (currentlvl == 2) //doesnt exist rn
        {
            stats.health += 10;
            stats.damage += 4;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (triggered)
        {
            bulletTimer += Time.deltaTime;
            playerPos = player.GetComponent<Transform>().position;

            Vector2 playerDir = UsefulllFs.Dir(playerPos, transform.position, true);

            rigidbody.velocity = new Vector2(-playerDir.x, -playerDir.y) * stats.moveSpeed;

            Vector2 direction = -playerDir;
            rigidbody.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (bulletTimer > currentAttackDuration && stats.ableToAttack == true)
            {
                bulletTimer = 0;
                currentAttackDuration = gameObject.GetComponent<AtkPatterns>().Attack(0);

            }
        }
    }
}
