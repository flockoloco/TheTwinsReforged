using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramovement : MonoBehaviour
{
    private Vector2 mousePos;
    private GameObject player;
    private Vector2 playerPos;
    private Camera cam;
    private Vector2 finalvector;
    private Vector2 mouseDir;
    private float mouseDist;

    private Vector3 wayPointInteract;

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        if (!PauseMenu.gameIsPaused && GameObject.FindWithTag("Player").GetComponent<PlayerStats>().shopOpen == false)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            playerPos = player.transform.position;

            mouseDir = new Vector2(mousePos.x - playerPos.x, mousePos.y - playerPos.y).normalized;
            mouseDist = Mathf.Sqrt((mousePos.x - playerPos.x) * (mousePos.x - playerPos.x) + (mousePos.y - playerPos.y) * (mousePos.y - playerPos.y));
            finalvector = mouseDir * mouseDist / 8;

            gameObject.transform.position = new Vector3(finalvector.x + playerPos.x, finalvector.y + playerPos.y, -10);
        }
        else
        {
            gameObject.transform.position = Vector3.LerpUnclamped(gameObject.transform.position, wayPointInteract, 2 * Time.deltaTime);
        }
    }
    public void SetUpWayPoint(Vector3 desiredPosition)
    {
        wayPointInteract = desiredPosition;
    }
}
