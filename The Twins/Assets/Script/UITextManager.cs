using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TheTwins.Model;
public class UITextManager : MonoBehaviour
{
    private PlayerStats playerStats;
    private TextMeshProUGUI hpText;
    private TextMeshProUGUI goldText;
    private TextMeshProUGUI nuggetsText;
    private TextMeshProUGUI barsText;
    private TextMeshProUGUI normalArrowsText;
    private TextMeshProUGUI oreArrowsText;
    private TextMeshProUGUI potionText;

    public GameObject normalArrowUI;
    public GameObject oreArrowUI;

    public GameObject BossUI;
    void Start()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();

        hpText = GameObject.FindWithTag("HPText").GetComponent<TextMeshProUGUI>();
        goldText = GameObject.FindWithTag("GoldText").GetComponent<TextMeshProUGUI>();
        nuggetsText = GameObject.FindWithTag("NuggetsText").GetComponent<TextMeshProUGUI>();
        barsText = GameObject.FindWithTag("BarsText").GetComponent<TextMeshProUGUI>();
        normalArrowsText = GameObject.FindWithTag("NormalArrowText").GetComponent<TextMeshProUGUI>();
        oreArrowsText = GameObject.FindWithTag("OreArrowText").GetComponent<TextMeshProUGUI>();
        potionText = GameObject.FindWithTag("PotionText").GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        hpText.text = (playerStats.health.ToString() + "/" + playerStats.maxHealth.ToString());
        goldText.text = playerStats.gold.ToString();
        nuggetsText.text = playerStats.nuggets.ToString();
        barsText.text = playerStats.bars.ToString();

        normalArrowsText.text = EquipmentClass.Quiver[0].amount.ToString();
        oreArrowsText.text = EquipmentClass.Quiver[1].amount.ToString();

        potionText.text = playerStats.healthPotions.ToString();
    }

    public void CheckArrow()
    {
        if (playerStats.selectedArrow == 1)
        {
            normalArrowUI.GetComponent<RectTransform>().localPosition = new Vector3(-51.0999985f, 41.2000008f, 0);
            normalArrowUI.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            oreArrowUI.GetComponent<RectTransform>().localPosition = new Vector3(-22f, 93.5f, 0);
            oreArrowUI.GetComponent<RectTransform>().localScale = new Vector3(1.5f,1.5f,1);
        
        }
        else
        {
            normalArrowUI.GetComponent<RectTransform>().localPosition = new Vector3(-22.7000008f, 53.9000015f, 0);
            normalArrowUI.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1);

            oreArrowUI.GetComponent<RectTransform>().localPosition = new Vector3(-52.0999985f, 103.5f, 0);
            oreArrowUI.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        }
    }
    public void EnteredABossRoom(GameObject BossObject)
    {
        BossUI.SetActive(true);
        BossUI.GetComponent<BossUIHPBar>().AssignBoss(BossObject);
    }
    public void BossDied()
    {
        BossUI.SetActive(false);
    }
}
