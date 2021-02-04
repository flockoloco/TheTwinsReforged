using UnityEngine;

public class AltarScript : MonoBehaviour
{
    public bool playerInside;
    private bool used = false;
    private GameObject player;
    private PlayerMovement playerMovement;
    public GameObject AltarCanvas;
    private bool readyToDisable = false;

    private void Start()
    {
        AltarCanvas = GameObject.FindWithTag("GameManager").GetComponent<StartGameScript>().altarCanvas;
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
        AltarCanvas.SetActive(false);
    }
    public void Interact()
    {
        if (used == false)
        {
            used = true;

            AltarCanvas.SetActive(true);
            AltarCanvas.GetComponent<AltarandTutorialUIScript>().Activate();
        }
    }
}