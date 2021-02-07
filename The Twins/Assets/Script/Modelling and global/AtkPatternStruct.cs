using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletInfo
{
    [SerializeField]
    public int angleDiff;

    [SerializeField]
    public float shotTimer;

    public GameObject bulletPrefab;
    public bool fired;
    public float accuracy;
    public float randomSpeed;

    public BulletInfo(int angle, float time, GameObject bulletPrefab, float accuracy, float randomSpeed)
    {
        this.angleDiff = angle;
        this.shotTimer = time;
        this.bulletPrefab = bulletPrefab;
        this.fired = false;
        this.accuracy = accuracy;
        this.randomSpeed = randomSpeed;
    }
}

[System.Serializable]
public class AtkInfo
{
    [SerializeField]
    public string Name;

    [SerializeField]
    public float atkDuration;

    [SerializeField]
    public List<BulletInfo> allBullets;

    private AtkInfo(List<BulletInfo> bullets, string name, float atkDuration)
    {
        this.allBullets = bullets;
        this.Name = name;
        this.atkDuration = atkDuration;
    }
}