using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fountainUIScript : MonoBehaviour
{
    public GameObject player;
    public GameObject thronemenu;
    private GameManagerScript gameManager;
    public bool usee = true;
    public GameObject popUpPrefab;

    private void Start()
    {
        thronemenu.SetActive(false);
        player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>();
    }

    public void Activate()
    {
        thronemenu.SetActive(true);
    }
    public void Rest()
    {
        if (usee == true)
        {
            usee = false;
            player.GetComponent<PlayerStats>().health = player.GetComponent<PlayerStats>().maxHealth;
        }
        else if (usee == false)
        {
            GameObject popUp = Instantiate(popUpPrefab, gameObject.transform);
            popUp.transform.position = new Vector3(popUp.transform.position.x, popUp.transform.position.y - 300, popUp.transform.position.z);
            popUp.GetComponent<DialogScript>().GiveText("Already Rested.");
        }
        
    }
}