using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKIckStarter : MonoBehaviour
{
    public GameObject lvl0;

    // Start is called before the first frame update
    public void Start()
    {
        GameObject.FindWithTag("GameManager").GetComponent<StartGameScript>().StartGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void JustDoIt()
    {
        lvl0 = GameObject.Find("Generated Level");
        foreach (Transform child in lvl0.transform)
        {
            if (child.gameObject.name == "Rooms")
            {
                foreach (Transform childRoom in child.transform)
                {
                    if (childRoom.gameObject.name.Contains("Corridor"))
                    {
                        foreach (Transform childDoor in childRoom.transform)
                        {
                            if (childDoor.gameObject.tag == "Door")
                            {
                                childDoor.GetComponent<DoorScript>().CheckContacts();
                            }
                        }
                    }
                }
            }
        }

    }
}
