using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitBox : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "EProjectiles")
        {
            Destroy(collision.gameObject);
        }else if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<StatsHolder>().invunerable == false)
            {
                UsefulllFs.TakeDamage(collision.gameObject, player.GetComponent<PlayerStats>().swordDamage);
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos((transform.parent.rotation.z * Mathf.Deg2Rad)), Mathf.Sin((transform.parent.rotation.z * Mathf.Deg2Rad))) * 5, ForceMode2D.Impulse);  
            }
        }
        
    }
}
