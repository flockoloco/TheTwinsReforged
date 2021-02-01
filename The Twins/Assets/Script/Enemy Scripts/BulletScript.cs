﻿using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private int bounces = 0;

    private int bulletDamage;
    private float bullettimer = 8;
    void Update()
    {
        bullettimer -= Time.deltaTime;
        if (bullettimer <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void EnemyDamage(int damage)
    {
        bulletDamage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerStats>().invunerable == false)
            {
                UsefulllFs.TakeDamage(collision.gameObject, bulletDamage);
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * 0; //reseting velocity right before doing the knockback
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.GetComponent<Rigidbody2D>().velocity.normalized * 5, ForceMode2D.Impulse); //knocking the target towards the projectiles velocity
            }
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Door")
        {
            //if (bounces >= 0)
            //{
                bounces = bounces - 1;
                Vector2 a = collision.ClosestPoint(gameObject.transform.position);
                if (Mathf.Abs(a.x - gameObject.transform.position.x) > Mathf.Abs(a.y - gameObject.transform.position.y))
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y * -1);

                }
                else if (Mathf.Abs(a.x - gameObject.transform.position.x) < Mathf.Abs(a.y - gameObject.transform.position.y))
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x * -1, gameObject.GetComponent<Rigidbody2D>().velocity.y);
                }
                gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity * -1;
            //}
            /*if (bounces <= 0)
            {
                Destroy(gameObject);
            }              */
        }
    }   
}