using UnityEngine;

public class NpcScript : MonoBehaviour
{
    public bool playerInside;
    private bool used = false;
    private GameObject player;
    private PlayerMovement playerMovement;
    public GameObject NpcCanvas;
    private bool readyToDisable = false;

    private void Start()
    {
        NpcCanvas = GameObject.FindWithTag("GameManager").GetComponent<StartGameScript>().npcCanvas;
        player = GameObject.FindWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
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
        NpcCanvas.SetActive(false);
    }
    public void Interact()
    {
        if (used == false)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerStats>().shopOpen = true;
            GameObject.FindWithTag("MainCamera").GetComponent<cameramovement>().SetUpWayPoint(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, -10));
            used = true;

            NpcCanvas.SetActive(true);
            NpcCanvas.GetComponent<AltarandTutorialUIScript>().Activate();
        }
    }
}