using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHPBar : MonoBehaviour
{
    private Vector3 scaleChange;
    public GameObject player;
    private float maxHP;
    private float currentHP;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    void Update()
    {
        maxHP = player.GetComponent<PlayerStats>().maxHealth;
        currentHP = player.GetComponent<PlayerStats>().health;
        scaleChange = new Vector3(currentHP/ maxHP, 1, 0);
        transform.localScale = scaleChange;
    }
}
