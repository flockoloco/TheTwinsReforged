using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Ai : MonoBehaviour
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
            if (bulletTimer > currentAttackDuration)
            {
                bulletTimer = 0;
                duringAttack = false;
            }
            if (duringAttack == false)
            {
                attack = PickAttack(attack); // choosing next attack
                duringAttack = true;
                attackFired = false;
            }

            if (duringAttack == true) //moving towards the player using big machine gun
            {
                if (attack >= 0 && attack <= 3) //atk 0 to 3 (all shooting atacks where the boss runs towards the player)
                {
                    bulletTimer += Time.deltaTime;
                    if (attackFired == false) // atk part
                    {
                        attackFired = true;
                        currentAttackDuration = gameObject.GetComponent<AtkPatterns>().Attack(attack);
                    }
                    rigidbody.velocity = Vector2.down * stats.moveSpeed;
                }
                if (attack == 4) //"atk" 4 (the boss rests for 3 secs)
                {
                    bulletTimer += Time.deltaTime;

                    if (attackFired == false) //atk part
                    {
                        attackFired = true;
                        currentAttackDuration = gameObject.GetComponent<AtkPatterns>().Attack(attack);
                    }
                    rigidbody.velocity = new Vector2(0, 0);
                }
            }
        }
    }

    private int PickAttack(int currentAttack)
    {
        if (currentAttack == 4)
        {
            return 0;
        }
        return (currentAttack + 1);
    }
}