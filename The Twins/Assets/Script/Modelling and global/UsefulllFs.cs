using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsefulllFs
{
    public static Vector2 Dir (Vector2 pos1, Vector2 pos2, bool normalize)
    {
       Vector2 response = new Vector2(pos2.x - pos1.x, pos2.y - pos1.y);
        if (normalize == true)
        {
            response = response.normalized;
        }
        return response;
    }
    public static float Dist(Vector2 pos1, Vector2 pos2)
    {
        return Mathf.Sqrt(((pos2.x - pos1.x) * (pos2.x - pos1.x)) + ((pos2.y - pos1.y) * (pos2.y - pos1.y)));
    }
    public static void TakeDamage(GameObject target, int dealerDamage)
    {
        if (target.tag == "Player")
        { 
            int damagecalculations = dealerDamage - Mathf.FloorToInt(Mathf.Floor((target.GetComponent<PlayerStats>().armor / 2)));
            if (damagecalculations < 1)
            {
                damagecalculations = 1;
            }
            target.GetComponent<PlayerStats>().health -= damagecalculations;
            target.GetComponent<PlayerStats>().hit = true;
        }
        else if (target.tag == "Enemy") 
        {
            int damagecalculations = dealerDamage - Mathf.FloorToInt(Mathf.Floor((target.GetComponent<StatsHolder>().armor / 2)));
            if (damagecalculations < 1)
            {
                damagecalculations = 1;
            }
            target.GetComponent<StatsHolder>().health -= damagecalculations;
            target.GetComponent<StatsHolder>().hit = true;
        }
    }
    public static bool BuySomething(GameObject player, string type, int cost)
    {
        Debug.Log("inside the buysomething, type of currency " + type + " cost " + cost);
        if (type == "gold")
        {
            Debug.Log("should be inside the gold menu and has gold " + player.GetComponent<PlayerStats>().gold + " and cost " + cost);
            if (player.GetComponent<PlayerStats>().gold >= cost)
            {
                player.GetComponent<PlayerStats>().gold -= cost;
                Debug.Log("returnning true;");
                return true;
            }
        }else if (type == "bars")
        {
            if (player.GetComponent<PlayerStats>().bars >= cost)
            {
                player.GetComponent<PlayerStats>().bars -= cost;
                return true;
            }
            else if (player.GetComponent<PlayerStats>().nuggets >= (cost * 3))
            {
                player.GetComponent<PlayerStats>().nuggets -= (cost * 3);
                return true;
            }
        }
        return false;
    }
}
