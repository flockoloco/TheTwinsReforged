using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TheTwins.Model;
using TMPro;
using UnityEngine.SceneManagement;

public class DeadUIScript : MonoBehaviour
{
    public GameObject player;
    public GameObject deadMenu;
    private GameManagerScript gameManager;

    public TextMeshProUGUI swordLvlText;
    public TextMeshProUGUI armorLvlText;
    public TextMeshProUGUI currentLvlText;
    public TextMeshProUGUI goldText;

    [SerializeField]
    public List<Sprite> equipSprites;
    public Image swordImage;
    public Image armorImage;

    private void Start()
    {
        deadMenu.SetActive(false);
        player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>();
    }

    public void Activate()
    {
        deadMenu.SetActive(true);

        LockInteractions();
        FillInfo();
    }

    public void LockInteractions()
    {
        Time.timeScale = 0;
        player.GetComponent<PlayerMovement>().enabled = false;
    }
    public void FillInfo()
    {
        goldText.text = "Gold Lost: " + player.GetComponent<PlayerStats>().gold.ToString() + " coins";
        if (player.GetComponent<PlayerStats>().currentLevel == 0)
        {
            currentLvlText.text = "Died in level: how did you even manage to die in the tutorial?";
        }
        else
        {
            currentLvlText.text = "Died in level: " + player.GetComponent<PlayerStats>().currentLevel.ToString();
        }
        swordLvlText.text = "Enchant Level: " + player.GetComponent<PlayerStats>().equippedSword.enchantTier;
        armorLvlText.text = "Enchant Level: " + player.GetComponent<PlayerStats>().equippedArmor.enchantTier;


        swordImage.sprite = equipSprites[player.GetComponent<PlayerStats>().equippedSword.id];
        armorImage.sprite = equipSprites[player.GetComponent<PlayerStats>().equippedArmor.id];

        player.GetComponent<PlayerStats>().currentLevel = 0; 
    }
    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
}