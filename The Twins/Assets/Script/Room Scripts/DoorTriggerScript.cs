using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerScript : MonoBehaviour
{
    public DoorScript parentDoorScript;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && parentDoorScript.OpenOrClosed == 0)
        {
            if (parentDoorScript.RoomDoor.enemiesInside.Count >= 1 && parentDoorScript.RoomDoor.playerInside == true)
            {
                parentDoorScript.RoomDoor.CloseMyDoors();
            }
        }
        if (collision.gameObject.tag == "Room")
        {
            Debug.Log("AAAAAAAAAABC SAMFADSNF");
            parentDoorScript.rooms.Add(collision.gameObject);
        }
    }
}
