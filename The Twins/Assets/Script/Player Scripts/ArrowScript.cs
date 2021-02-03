using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheTwins.Model;

public class ArrowScript : MonoBehaviour
{
    private int arrowDamage;


    public Sprite ArrowNormal;
    public Sprite ArrowOre;
    private PlayerStats playerStats;


    public void Start()
    {
        arrowDamage = EquipmentClass.Quiver[GameObject.FindWithTag("Player").GetComponent<PlayerStats>().selectedArrow].damage;
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    private void Update()
    {
		if (playerStats.selectedArrow == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = ArrowNormal;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = ArrowOre;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && collision.gameObject.GetComponent<StatsHolder>().invunerable == false)
        {
            UsefulllFs.TakeDamage(collision.gameObject, arrowDamage);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * 0; //reseting velocity right before doing the knockback

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.GetComponent<Rigidbody2D>().velocity.normalized * 5, ForceMode2D.Impulse); //knocking the target towards the projectiles velocity
            Destroy(gameObject);
        }
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
