using System.Collections.Generic;
using UnityEngine;

namespace TheTwins.Model
{
    [System.Serializable]
    public class JustStatus
    {
        public int Status;
        public JustStatus()
        {
        }
        public JustStatus(int status)
        {
            this.Status = status;
        }
    }

    [System.Serializable]
    public class PlayerInfo
    {
        public string Username;
        public string Password;
        public int UserID;
        public int Status;

        public PlayerInfo()
        {
            this.Username = null;
            this.Password = null;
        }

        public PlayerInfo(string user, string pass)
        {
            this.Username = user;
            this.Password = pass;
        }

        public PlayerInfo(int id)
        {
            this.UserID = id;
        }
    }

    [System.Serializable]
    public class PlayerStatsHolder
    {
        public int UserID;
        public int eSwordID;
        public int eArmorID;
        public int currentLvl;
        public int currentHP;
        public int oreArrowAmount;
        public int normalArrowAmount;
        public int potsAmount;
        public int gold;

        public PlayerStatsHolder()
        {
            this.eSwordID = 0;
            this.eArmorID = 4;
            this.currentLvl = 0;
            this.currentHP = 100;
            this.oreArrowAmount = 0;
            this.normalArrowAmount = 0;
            this.potsAmount = 2;
            this.gold = 50;
        }

        public PlayerStatsHolder(int swordID, int armorID, int currentLvl, int hp, int oreArrows, int normalArrows, int pots, int gold,int id)
        {
            this.UserID = id;
            this.eSwordID = swordID;
            this.eArmorID = armorID;
            this.currentLvl = currentLvl;
            this.currentHP = hp;
            this.oreArrowAmount = oreArrows;
            this.normalArrowAmount = normalArrows;
            this.potsAmount = pots;
            this.gold = gold;
        }
    }

    [System.Serializable]
    public class CurrencyHolder
    {
        public int Ores;
        public int Bars;
        public int UserID;

        public CurrencyHolder(int ores, int bars, int playerid)
        {
            this.Ores = ores;
            this.Bars = bars;
            this.UserID = playerid;
        }

        public CurrencyHolder()
        {
            this.Ores = 0;
            this.Bars = 0;
        }
    }
    [System.Serializable]
    public class EnchantTierHolder
    {
        public int UserID;
        public int e0tier;
        public int e1tier;
        public int e2tier;
        public int e3tier;
        public int e4tier;
        public int e5tier;
        public int e6tier;
        public int e7tier;

        public EnchantTierHolder(int e0tier, int e1tier, int e2tier, int e3tier, int e4tier, int e5tier, int e6tier, int e7tier,int id)
        {
            this.e0tier = e0tier;
            this.e1tier = e1tier;
            this.e2tier = e2tier;
            this.e3tier = e3tier;
            this.e4tier = e4tier;
            this.e5tier = e5tier;
            this.e6tier = e6tier;
            this.e7tier = e7tier;
            this.UserID = id;
        }

        public EnchantTierHolder()
        {
            this.e0tier = 0;
            this.e1tier = 0;
            this.e2tier = 0;
            this.e3tier = 0;
            this.e4tier = 0;
            this.e5tier = 0;
            this.e6tier = 0;
            this.e7tier = 0;
        }
    }
    [System.Serializable]
    public class DeliveryHolder
    {
        public int UserID;
        public int OresAmount;
        public int BarsAmount;
        public int Type = 0;
        public int Status;
        public DeliveryHolder(int ores, int bars, int playerid, int type)
        {
            this.OresAmount = ores;
            this.BarsAmount = bars;
            this.UserID = playerid;
            this.Type = type;
        }

        public DeliveryHolder()
        {
            this.OresAmount = 0;
            this.BarsAmount = 0;
            this.Type = 0;
        }
        public DeliveryHolder(int id, int type)
        {
            this.UserID = id;
            this.Type = type;
        }
    }
    [System.Serializable]
    public struct Enchants
    {
        public int tier;
        public int bonusHp;
        public int BonusDamage;
        public int price;

        public Enchants(int tier, int bonusHp, int BonusDamage, int price)
        {
            this.tier = tier;
            this.bonusHp = bonusHp;
            this.BonusDamage = BonusDamage;
            this.price = price;
        }
    }
    [System.Serializable]
    public class Arrows
    {
        public string name { get; }
        public string type { get; set; }
        public int damage { get; set; }
        public int price { get; set; }
        public int amount { get; set; }

        public Arrows(string name, string type, int damage, int price, int amount)
        {
            this.name = name;
            this.type = type;
            this.damage = damage;
            this.price = price;
            this.amount = amount;
        }
    }
    [System.Serializable]
    public class SwordAndArmor
    {
        public string name;
        public string type;
        public int damage;
        public float atkSpeed;
        public int armor;
        public int maxHP;
        public int price;
        public int enchantTier;
        public int id;

        public SwordAndArmor()
        {
        }

        public SwordAndArmor(string name, int damage, float atkSpeed, int price, int enchantTier, int arrayIndex)//Sword
        {
            this.name = name;
            this.type = "Sword";
            this.damage = damage;
            this.atkSpeed = atkSpeed;
            this.price = price;
            this.enchantTier = enchantTier;
            this.armor = 0;
            this.maxHP = 0;
            this.id = arrayIndex;
        }

        public SwordAndArmor(string name, int armor, int maxHP, int price, int enchantTier, int arrayIndex)//Armor
        {
            this.name = name;
            this.type = "Armor";
            this.damage = 0;
            this.atkSpeed = 0;
            this.enchantTier = enchantTier;
            this.price = price;
            this.armor = armor;
            this.maxHP = maxHP;
            this.id = arrayIndex;
        }
    }
    [System.Serializable]
    public static class EquipmentClass
    {
        private static List<Enchants> enchant = new List<Enchants>();

        public static List<Enchants> Enchant
        {
            get
            {
                if (enchant.Count == 0)
                {
                    enchant.Add(new Enchants(0, 0, 0, 0));
                    enchant.Add(new Enchants(1, 10, 2, 50));
                    enchant.Add(new Enchants(2, 20, 5, 70));
                    enchant.Add(new Enchants(3, 50, 10, 100));
                }
                return enchant;
            }
        }

        private static List<SwordAndArmor> swordandArmor = new List<SwordAndArmor>();

        public static List<SwordAndArmor> SwordandArmor
        {
            get
            {
                if (swordandArmor.Count == 0)
                {
                    //ir buscar a save
                    EnchantTierHolder enchantTier = new EnchantTierHolder();
                    enchantTier = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>().enchantTierHolder; //busca da database

                    swordandArmor.Add(new SwordAndArmor("Wooden sword", 15, 1.5f, 0, enchantTier.e0tier, 0));
                    swordandArmor.Add(new SwordAndArmor("Iron sword", 25, 2f, 20, enchantTier.e1tier, 1));
                    swordandArmor.Add(new SwordAndArmor("Gold sword", 20, 1f, 40, enchantTier.e2tier, 2));
                    swordandArmor.Add(new SwordAndArmor("Diamond sword", 35, 1f, 100, enchantTier.e3tier, 3));

                    swordandArmor.Add(new SwordAndArmor("Wooden armor", 0, 0, 0, enchantTier.e4tier, 4));
                    swordandArmor.Add(new SwordAndArmor("Iron armor", 0, 20, 20, enchantTier.e5tier, 5));
                    swordandArmor.Add(new SwordAndArmor("Gold armor", 2, 30, 40, enchantTier.e6tier, 6));
                    swordandArmor.Add(new SwordAndArmor("Diamond armor", 4, 50, 100, enchantTier.e7tier, 7));
                }
                return swordandArmor; 
            }
        }

        private static List<Arrows> quiver = new List<Arrows>();

        public static List<Arrows> Quiver
        {
            get
            {
                if (quiver.Count == 0)
                {
                    PlayerStatsHolder statstoUse = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>().statsToUse; // getting arrow amounts
                    quiver = new List<Arrows>
                    {
                        new Arrows("NormalArrow", "Normal", 10, 5, statstoUse.normalArrowAmount), 
                        new Arrows("OreArrow", "Ore", 20, 1, statstoUse.oreArrowAmount) 
                    };
                }
                return quiver;
            }
        }
    }
}