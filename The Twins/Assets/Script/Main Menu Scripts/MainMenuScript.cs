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
    public GameObject continueButton;
    public void StartUp()
    {
        gameManager = GameObject.FindWithTag("GameManager");
        Debug.Log(gameManager.GetComponent<GameManagerScript>().logged + "inside start of mainmenuscript");
        
        if (gameManager.GetComponent<GameManagerScript>().logged == false)
        {
            gameManager.GetComponent<GameManagerScript>().enchantTierHolder = new EnchantTierHolder();
            gameManager.GetComponent<GameManagerScript>().playerCurrency = new CurrencyHolder();
            gameManager.GetComponent<GameManagerScript>().statsToUse = new PlayerStatsHolder();
        }
        Debug.Log(gameManager.GetComponent<GameManagerScript>().statsToUse + "stats before the if");
        if (gameManager.GetComponent<GameManagerScript>().statsToUse.currentLvl == 0)
        {
            Debug.Log("yo wtf");
            continueButton.SetActive(false);
        }
    }
    public void Continue()
    {
        LevelLoader.GetComponent<LevelLoader>().LoadLevel("Level Generator");
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
    public void LogOut()
    {
        gameManager.GetComponent<GameManagerScript>().logged = false;
        GameObject.Find("CanvasChanger").GetComponent<UIPopUpScript>().CanvasSwitcher(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
