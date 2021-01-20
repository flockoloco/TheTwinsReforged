using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TheTwins.Model;
using TMPro;

public class EnchantMenuScript : MonoBehaviour
{
    public GameObject player;
    public GameObject enchantMenu;

    public Button eArmorButton;
    public Button eSwordButton;
    private PlayerStats playerstats;

    public TextMeshProUGUI oldSwordLvlText;
    public TextMeshProUGUI newSwordLvlText;
    public TextMeshProUGUI swordStatsText;
    public TextMeshProUGUI swordBuyTextBars;
    public TextMeshProUGUI swordBuyTextOres;

    public TextMeshProUGUI oldArmorLvlText;
    public TextMeshProUGUI newArmorLvlText;
    public TextMeshProUGUI armorStatsText;
    public TextMeshProUGUI armorBuyTextBars;
    public TextMeshProUGUI armorBuyTextOres;

    [SerializeField]
    public List<Sprite> equipSprites;
    public Image swordImageRenderer1;
    public Image swordImageRenderer2;
    public Image armorImageRenderer1;
    public Image armorImageRenderer2;

    public GameObject popUpPrefab;

    private void Awake()
    {

        enchantMenu.SetActive(false);
        player = GameObject.FindWithTag("Player");
        playerstats = player.GetComponent<PlayerStats>();
       
        eArmorButton.onClick.AddListener(delegate { BuyEnchant(playerstats.equippedArmor.id); });//armors
        eSwordButton.onClick.AddListener(delegate { BuyEnchant(playerstats.equippedSword.id); });//swords
    }
    public void Activate()
    {
        enchantMenu.SetActive(true);
        UpdateTexts();
    }
    public void UpdateTexts()
    {
        if (playerstats.equippedSword.enchantTier < 3)
        {
            int enchanttierplus1 = playerstats.equippedSword.enchantTier + 1;
            oldSwordLvlText.text = "Lvl: " + playerstats.equippedSword.enchantTier;
            newSwordLvlText.text = "Lvl: " + (enchanttierplus1);
            swordStatsText.text = "Damage\n +" + EquipmentClass.Enchant[playerstats.equippedSword.enchantTier].BonusDamage + "  ->  +" + EquipmentClass.Enchant[playerstats.equippedSword.enchantTier + 1].BonusDamage;
            
            swordBuyTextBars.text = "" + EquipmentClass.Enchant[playerstats.equippedSword.enchantTier +1].price;
            swordBuyTextOres.text = "" + EquipmentClass.Enchant[playerstats.equippedSword.enchantTier +1].price * 3;

        }
        else
        {
            oldSwordLvlText.text = "Lvl: Max";
            newSwordLvlText.text = "Lvl: N/A";
            swordStatsText.text = "Damage\n +" + EquipmentClass.Enchant[playerstats.equippedSword.enchantTier].BonusDamage + "  ->  + N/A";

            swordBuyTextBars.text = "N/A";
            swordBuyTextOres.text = "N/A";
        }

        if (playerstats.equippedArmor.enchantTier < 3)
        {
            int enchanttierplus1 = playerstats.equippedArmor.enchantTier + 1;
            oldArmorLvlText.text = "Lvl: " + playerstats.equippedArmor.enchantTier;
            newArmorLvlText.text = "Lvl: " + enchanttierplus1;
            
            armorStatsText.text = "BonusHP \n +" + EquipmentClass.Enchant[playerstats.equippedArmor.enchantTier].bonusHp + "  ->  +" + EquipmentClass.Enchant[playerstats.equippedArmor.enchantTier + 1].bonusHp;

            armorBuyTextBars.text = "" + EquipmentClass.Enchant[playerstats.equippedArmor.enchantTier +1].price;
            armorBuyTextOres.text = "" + EquipmentClass.Enchant[playerstats.equippedArmor.enchantTier +1].price * 3;
        }
        else
        {
            oldArmorLvlText.text = "Lvl: Max";
            newArmorLvlText.text = "Lvl: N/A";
            armorStatsText.text = "BonusHP \n +" + EquipmentClass.Enchant[playerstats.equippedArmor.enchantTier].bonusHp + "  ->  + N/A";

            armorBuyTextBars.text = "N/A";
            armorBuyTextOres.text = "N/A";
        }

        swordImageRenderer1.sprite = equipSprites[playerstats.equippedSword.id];
        swordImageRenderer2.sprite = equipSprites[playerstats.equippedSword.id];
        armorImageRenderer1.sprite = equipSprites[playerstats.equippedArmor.id];
        armorImageRenderer2.sprite = equipSprites[playerstats.equippedArmor.id];

    }
    
    public void BuyEnchant(int itemToUpgradeID)
    {
        
        if (EquipmentClass.SwordandArmor[itemToUpgradeID].enchantTier < 3)
        {
            if (UsefulllFs.BuySomething(player, "bars", EquipmentClass.Enchant[EquipmentClass.SwordandArmor[itemToUpgradeID].enchantTier + 1].price) == true)
            {
                EquipmentClass.SwordandArmor[itemToUpgradeID].enchantTier += 1;

                if (itemToUpgradeID < 4) //sword
                {
                    playerstats.RemoveEquipedItem("Sword");
                    playerstats.EquipItem("Sword", itemToUpgradeID);
                }
                else if (itemToUpgradeID > 3) //armor
                { 
                    playerstats.RemoveEquipedItem("Armor");
                    playerstats.EquipItem("Armor", itemToUpgradeID);
                }
                
                GameObject popUp = Instantiate(popUpPrefab, gameObject.transform);
                popUp.transform.position = new Vector3(popUp.transform.position.x, popUp.transform.position.y - 300, popUp.transform.position.z);
                popUp.GetComponent<DialogScript>().GiveText("Enchantment bought!");
            }
            else
            {
                GameObject popUp = Instantiate(popUpPrefab, gameObject.transform);
                popUp.transform.position = new Vector3(popUp.transform.position.x, popUp.transform.position.y - 300, popUp.transform.position.z);
                popUp.GetComponent<DialogScript>().GiveText("Not enough money!");
            }
        }
        else
        {
            GameObject popUp = Instantiate(popUpPrefab, gameObject.transform);
            popUp.transform.position = new Vector3(popUp.transform.position.x, popUp.transform.position.y - 300, popUp.transform.position.z);
            popUp.GetComponent<DialogScript>().GiveText("Already max level!");
        }
        UpdateTexts();
    }

}
