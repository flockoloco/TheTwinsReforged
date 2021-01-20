using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public GameObject shopCanvas;
    public bool playerInside = false;
    public bool oneTime = true;

    void Start()
    {
        shopCanvas = GameObject.FindWithTag("GameManager").GetComponent<StartGameScript>().shopCanvas;
    }
    public void Interact()
    {
        shopCanvas.SetActive(true);
        shopCanvas.GetComponent<ShopMenuScript>().Activate();
        Debug.Log("setting active");
    }
    public void Deinteract()
    {
        shopCanvas.SetActive(false);
    }

    void Update()
    {
        if (playerInside == true && oneTime == true)
        {
            oneTime = false;
            Interact();


        } else if (playerInside == false && oneTime == false)
        {
            Deinteract();
            oneTime = true;
        }
    }
}
