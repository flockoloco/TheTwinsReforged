using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantsScript : MonoBehaviour
{
    public GameObject enchantCanvas;
    public bool playerInside = false;
    public bool oneTime = true;

    void Start()
    {
        enchantCanvas = GameObject.FindWithTag("GameManager").GetComponent<StartGameScript>().enchantCanvas;
    }
    public void Interact()
    {
        Debug.Log("setting active");
        enchantCanvas.SetActive(true);
        
        enchantCanvas.GetComponent<EnchantMenuScript>().Activate();

    }
    public void DeInteract()
    {
      
        enchantCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInside == true && oneTime == true)
        {
            oneTime = false;
            Interact();
        }
        else if (playerInside == false && oneTime == false)
        {
            Debug.Log("helo......");
            DeInteract();
            oneTime = true;
        }
    }
}
