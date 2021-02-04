using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainScript : MonoBehaviour
{
    public bool playerInside;
    private bool used = false;
    private bool readyToDisable = false;
    public GameObject fountainCanvas;

    private void Start()
    {
        fountainCanvas = GameObject.FindWithTag("GameManager").GetComponent<StartGameScript>().fountainCanvas;
        
    }


    private void Update()
    {
        if (playerInside && Input.GetKey(KeyCode.E))
        {
            readyToDisable = true;

            Interact();
        }
        if (playerInside == false && readyToDisable)
        {
            readyToDisable = false;
            Deinteract();
            used = false;
        }
    }
    public void Deinteract()
    {
        fountainCanvas.SetActive(false);
    }
    public void Interact()
    {
        if (used == false)
        {
            used = true;

            fountainCanvas.SetActive(true);
            GameObject.FindWithTag("Player").GetComponent<PlayerStats>().shopOpen = true;
            GameObject.FindWithTag("MainCamera").GetComponent<cameramovement>().SetUpWayPoint(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, -10));
            Debug.Log("activating panel");
            fountainCanvas.GetComponent<fountainUIScript>().Activate();
            Debug.Log("setting active");


        }
    }
}



