using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellScript : MonoBehaviour
{
    public bool playerInside;
    private GameObject player;
    private bool oneTime = true;
    public GameObject WellCanvas;
    public GameObject popUpPrefab;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        WellCanvas = GameObject.FindWithTag("GameManager").GetComponent<StartGameScript>().wellCanvas;
    }
    public void Interact()
    {
        if (GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>().logged == true)
        {
             WellCanvas.SetActive(true);

            WellCanvas.GetComponent<WellMenuScript>().Activate();

            Debug.Log("setting active");
            GameManagerScript manager = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>();
            manager.SaveBarsAndOres();
        }
        else
        {
            GameObject popUp = Instantiate(popUpPrefab, WellCanvas.transform);
            popUp.transform.position = new Vector3(popUp.transform.position.x, popUp.transform.position.y - 300, popUp.transform.position.z);
            popUp.GetComponent<DialogScript>().GiveText("Cant open while offline!");
        }
       
        // mandar para o server saveCurrency()
        // no script do canvas mandar o send para o email currency
        // subtrair o email currency ao currency


    }

    void Update()
    {
        if (playerInside == true && oneTime == true)
        {
            oneTime = false;
            Interact();


        }
        else if (playerInside == false && oneTime == false)
        {
            Deinteract();
            oneTime = true;
        }
    }

   
    public void Deinteract()
    {
        WellCanvas.SetActive(false);
    }

}