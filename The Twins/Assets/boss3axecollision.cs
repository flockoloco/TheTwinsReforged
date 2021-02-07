using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss3axecollision : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerStats>().invunerable == false)
        {
            collision.gameObject.GetComponent<PlayerStats>().invunerable = true;
            UsefulllFs.TakeDamage(collision.gameObject, gameObject.transform.parent.gameObject.GetComponent<StatsHolder>().damage);
        }
    }
}
