using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator;
using UnityEngine;

public class StairsScript : MonoBehaviour
{
    public bool playerInside;
    private bool used = false;
    private GameObject player;
    private PlayerMovement playerMovement;
    public GameObject StairsCanvas;
    private bool readyToDisable = false;

    private void Start()
    {
        StairsCanvas = GameObject.FindWithTag("GameManager").GetComponent<StartGameScript>().stairsCanvas;
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
        StairsCanvas.SetActive(false);
    }
    public void Interact()
    {
        if (used == false)
        {
            used = true;

            StairsCanvas.SetActive(true);
            Debug.Log("activating panel");
            StairsCanvas.GetComponent<StairsUIScript>().Activate();
            Debug.Log("setting active");



            //GEN LVL USING CURRENT LVL


        }
    }
}