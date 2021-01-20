using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainScript : MonoBehaviour
{
    public bool playerInside;
    private bool used = false;
    private void Update()
    {
        if (playerInside && Input.GetKey(KeyCode.E))
        {
            
            Interact();
        }
    }
    public void Interact()
    {
        if (used == false)
        {
            used = true;
            GameObject.FindWithTag("Player").GetComponent<PlayerStats>().health = GameObject.FindWithTag("Player").GetComponent<PlayerStats>().maxHealth;
        }
    }
}
