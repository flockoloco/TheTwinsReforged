using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TheTwins.Model;

public class MainMenuScript : MonoBehaviour
{
    public GameObject gameManager;
    private PlayerStatsHolder playerStats;
    public bool disableContinue = true;
    public GameObject popUpPrefab;
    public GameObject LevelLoader;
    public void StartUp()
    {
        gameManager = GameObject.FindWithTag("GameManager");
        Debug.Log(gameManager.GetComponent<GameManagerScript>().logged + "inside start of mainmenuscript");
        if (gameManager.GetComponent<GameManagerScript>().logged == true)
        {
            disableContinue = false;
            gameManager.GetComponent<GameManagerScript>().LoadRun();
            gameManager.GetComponent<GameManagerScript>().GetBarsAndOres();
            gameManager.GetComponent<GameManagerScript>().GetEnchants();
        }
        else
        {
            gameManager.GetComponent<GameManagerScript>().enchantTierHolder = new EnchantTierHolder();
            gameManager.GetComponent<GameManagerScript>().playerCurrency = new CurrencyHolder();
            gameManager.GetComponent<GameManagerScript>().statsToUse = new PlayerStatsHolder();
            disableContinue = true;
        }
    }
    public void Continue()
    {
        if (disableContinue == true)
        {
            GameObject popUp = Instantiate(popUpPrefab, gameObject.transform);
            popUp.transform.position = new Vector3(popUp.transform.position.x, popUp.transform.position.y - 300, popUp.transform.position.z);
            popUp.GetComponent<DialogScript>().GiveText("Cant retrieve saves while offline");
        }
        else if (disableContinue == false)
        {
            LevelLoader.GetComponent<LevelLoader>().LoadLevel("Level Generator");
        }
    }
    public void StartNewGame()
    {
        gameManager.GetComponent<GameManagerScript>().statsToUse = new PlayerStatsHolder();
        LevelLoader.GetComponent<LevelLoader>().LoadLevel("Level Generator");
    }
    public void Options()
    {
        Debug.Log("uhhga booga"); //canvas feito so ir buscar
    }
    public void Credits()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }
}
