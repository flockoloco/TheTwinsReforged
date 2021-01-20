using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public RoomDoorScript RoomDoor;

    public Sprite doorOpened;
    public Sprite doorClosed;

    private Animator dooranimator;

    public List<ContactPoint2D> bigList;

    public List<GameObject> rooms = new List<GameObject>();

    public int verticalOrHozirontal; //0 vertical 1 horizontal
    public bool onlyNow = false;
    public int OpenOrClosed = 0; //0 open 1 closed

    private void Start()
    {
        dooranimator = GetComponent<Animator>();
    }
    void Update()
    {
        if (onlyNow)
        {
            if (RoomDoor.enemiesInside.Count == 0)
            {
                Invoke("OpeningDoor", 1f);
            }
        } 
    } 

    public void OpeningDoor()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = doorOpened;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        OpenOrClosed = 0;
    }

    public void ClosingDoor()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = doorClosed;
        RoomDoor.EnemyTriggerCheck();
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        OpenOrClosed = 1;
    }
    public void CheckContacts()
    {
        Debug.Log(rooms.Count);
        foreach (GameObject roomConnect in rooms)
        {
            Debug.Log("Nao queria por isso ca mas coloquei");
            RoomDoor = roomConnect.GetComponent<RoomDoorScript>();
            onlyNow = true;
        }
    }
}
