using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AltarandTutorialUIScript : MonoBehaviour
{
    public GameObject player;
    public GameObject altarMenu;
    private GameManagerScript gameManager;

    private void Start()
    {
        altarMenu.SetActive(false);
        player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>();
    }

    public void Activate()
    {
        altarMenu.SetActive(true);
    }
    public void EndGame()
    {
        gameManager.GetComponent<GameManagerScript>().statsToUse.currentLvl = 0;
        
        
        if (gameManager.GetComponent<GameManagerScript>().logged == true)
        {
            gameManager.GetComponent<GameManagerScript>().SaveEnchants();
            gameManager.GetComponent<GameManagerScript>().SaveRunInfo();
            gameManager.GetComponent<GameManagerScript>().SaveBarsAndOres();
        }
        
        gameManager.GetComponent<GameManagerScript>().logged = false;
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }
    public void SkipTutorial()
    {
        player.transform.position = new Vector3(GameObject.Find("StairsPrefab").transform.position.x, GameObject.Find("StairsPrefab").transform.position.y - 3, 0);
    }
}