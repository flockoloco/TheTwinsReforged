using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheTwins.Model;

public class PlayerStats : MonoBehaviour
{
    public int health;
    public int maxHealth;
    private int baseHPAmount = 100;
    private Animator animator;
    private int baseArmorAmount = 1;
    public float swordAtkSpeed;
    public float bowAtkSpeed;
    public int armor;
    public int swordDamage;
    public int selectedArrow = 0;
    public bool hit;
    public bool invunerable;
    public int gold;
    public int bars;
    public int nuggets;
    public int healthPotions;
    private float vunerableTimer;
    public GameObject playerSword;
    public SwordAndArmor equippedSword = new SwordAndArmor();
    public SwordAndArmor equippedArmor = new SwordAndArmor();
    private PauseMenu pauseMenu;
    public bool shopOpen = false;
    public int currentLevel;
    private void Awake()
    {
        pauseMenu = GameObject.FindWithTag("PauseUI").GetComponent<PauseMenu>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        
    }

    void Update()
    {
        if (health <= 0)
        {
            pauseMenu.ResetGame();
        }
        if (hit == true)
        {
            animator.SetBool("Hit", true);
            invunerable = true;
            if (vunerableTimer == 0)
            {
                gameObject.GetComponent<PlayerMovement>().state = PlayerMovement.State.hit;
            }
            vunerableTimer += Time.deltaTime;
            if (vunerableTimer > 0.25 && vunerableTimer < 0.5)
            {
                if (gameObject.GetComponent<PlayerMovement>().state == PlayerMovement.State.hit)
                {
                    gameObject.GetComponent<PlayerMovement>().state = PlayerMovement.State.walking;
                }
            }
            if (vunerableTimer > 0.5)
            {
                animator.SetBool("Hit", false);
                invunerable = false;
                hit = false;
                vunerableTimer = 0;
            }
        }
    }

    public void EquipItem(string type, int number)
    {
        if (type == "Sword")
        {
            equippedSword = EquipmentClass.SwordandArmor[number];
            swordDamage = equippedSword.damage + EquipmentClass.Enchant[equippedSword.enchantTier].BonusDamage;
            swordAtkSpeed = equippedSword.atkSpeed;
            playerSword.GetComponent<Animator>().SetInteger("Tier",number);
        }
        else if (type == "Armor")
        {
            equippedArmor = EquipmentClass.SwordandArmor[number];
            maxHealth = baseHPAmount + equippedArmor.maxHP + EquipmentClass.Enchant[equippedArmor.enchantTier].bonusHp;
            armor = baseArmorAmount + equippedArmor.armor;
            if (number == 4)
            {
                Animator animator = gameObject.GetComponent<Animator>();
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Default_hero_anim/Default_hero_animator");
            }
            else if (number == 5)
            {
                Animator animator = gameObject.GetComponent<Animator>();
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Iron_hero_anim/Iron_hero_animator");
            }
            else if (number == 6)
            {
                Animator animator = gameObject.GetComponent<Animator>();
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Gold_hero_anim/Gold_hero_animator");
            }
            else if (number == 7)
            {
                Animator animator = gameObject.GetComponent<Animator>();
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Diamond_hero_anim/Diamond_hero_animator");
            }
        }
    }
    public void RemoveEquipedItem(string type)
    {
        if (type == "Sword")
        {
            equippedSword = EquipmentClass.SwordandArmor[0];
            swordDamage = equippedSword.damage + EquipmentClass.Enchant[equippedSword.enchantTier].BonusDamage;
            swordAtkSpeed = equippedSword.atkSpeed;
            playerSword.GetComponent<Animator>().SetInteger("Tier", equippedSword.id);
        }
        else if (type == "Armor")
        {
            equippedArmor = EquipmentClass.SwordandArmor[4];
            maxHealth = baseHPAmount;
            armor = baseArmorAmount;
        }
    }
    public void UseHealthPotion()
    {
        healthPotions -= 1;
        health += maxHealth / 2;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}