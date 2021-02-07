using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsUIScript : MonoBehaviour
{
    public GameObject player;
    public GameObject stairsMenu;
    private GameManagerScript gameManager;
    public GameObject popUpPrefab;

    public GameObject Panel1;
    public GameObject Panel2;

    public Animator transition;

    public AudioClip stairssound;


    private void Start()
    {
        Debug.Log("Inside of the start, " + stairsMenu);
        stairsMenu.SetActive(false);
        player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>();
        transition = GameObject.Find("Transition").GetComponent<Animator>();
    }

    public void Activate()
    {
        Debug.Log("inside the activate");
        stairsMenu.SetActive(true);
    }

    IEnumerator LoadTransition()
    {
        transition.SetBool("Start", true);
        stairsMenu.SetActive(false);
        yield return new WaitForSeconds(1f);
        Debug.Log(player.GetComponent<PlayerStats>().currentLevel + 1);
        gameManager.GetComponent<StartGameScript>().GenLevel(player.GetComponent<PlayerStats>().currentLevel + 1);
        player.GetComponent<PlayerStats>().currentLevel += 1;
        transition.SetBool("Start", false);
    }

    public void Continue()
    {

        gameObject.GetComponent<CameraSoundChanger>().changeAudio(stairssound);
        StartCoroutine(LoadTransition());
    }
    public void SaveAndExit()
    {
        if (gameManager.GetComponent<GameManagerScript>().logged == true)
        {
            player.GetComponent<PlayerStats>().currentLevel += 1;

            gameManager.GetComponent<GameManagerScript>().SaveEnchants();
            gameManager.GetComponent<GameManagerScript>().SaveRunInfo();
            gameManager.GetComponent<GameManagerScript>().SaveBarsAndOres();

            Application.Quit();
        }
        else if (gameManager.GetComponent<GameManagerScript>().logged == false)
        {
            GameObject popUp = Instantiate(popUpPrefab, stairsMenu.transform);
            popUp.transform.position = new Vector3(popUp.transform.position.x, popUp.transform.position.y - 300, popUp.transform.position.z);
            popUp.GetComponent<DialogScript>().GiveText("Can't save offline");

        }
    }

    public void CancelQuit()
    {
        Panel1.gameObject.SetActive(true);
        Panel2.gameObject.SetActive(false);
    }

    public void SaveAndQuitQuestion()
    {
        Panel1.gameObject.SetActive(false);
        Panel2.gameObject.SetActive(true);
    }

}
