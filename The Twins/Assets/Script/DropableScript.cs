using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropableScript : MonoBehaviour
{
    public int thisValue;
    public GameObject player;

    public void Value(int value)
    {
        thisValue = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            if (gameObject.tag == "GoldDrop")
            {

                Debug.Log(thisValue);
                player.GetComponent<PlayerStats>().gold = player.GetComponent<PlayerStats>().gold + thisValue;
            }
            else if (gameObject.tag == "NuggetsDrop")
            {
                player.GetComponent<PlayerStats>().nuggets += thisValue;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
