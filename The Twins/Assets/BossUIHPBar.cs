using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossUIHPBar : MonoBehaviour
{
    private Vector3 scaleChange;
    public GameObject Boss;
    private float maxHP;
    private float currentHP;
    public GameObject HealthBar;
    public TextMeshProUGUI bossHealthText;
    public void AssignBoss(GameObject BossToAssign)
    {
        Boss = BossToAssign; 
        maxHP = Boss.GetComponent<StatsHolder>().health; 
    }

    void Update()
    {
        currentHP = Boss.GetComponent<StatsHolder>().health;

        bossHealthText.text = (currentHP + "  /  " + maxHP);
        scaleChange = new Vector3(currentHP / maxHP, 1, 0);
        HealthBar.transform.localScale = scaleChange;
    }
}