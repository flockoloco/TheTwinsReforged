using System.Collections.Generic;
using UnityEngine;

public class AtkPatterns : MonoBehaviour
{
    [SerializeField]
    public List<AtkInfo> atkInfos = new List<AtkInfo>();

    public bool UpdateCheck = false;
    public float baseTimer;
    public int currentAtkIndex;

    public GameObject firePoint;

    public Animator animator;

 
    public float Attack(int indexdoataque)
    {
        currentAtkIndex = indexdoataque;
        UpdateCheck = true;
        foreach (BulletInfo bullet in atkInfos[indexdoataque].allBullets)
        {
            bullet.fired = false;
        }
        return atkInfos[indexdoataque].atkDuration;
    }

    private void Update()
    {
        if (UpdateCheck == true)
        {
            //Debug.Break();
            if (gameObject.GetComponent<StatsHolder>().enemyID == 5)
            {
                animator.SetBool("attack", true);
            }
            else if(gameObject.GetComponent<StatsHolder>().enemyID == 1)
            {
                animator.SetBool("attack", true);
            }
            else if (gameObject.GetComponent<StatsHolder>().enemyID == 2)
            {
                animator.SetBool("attack", true);
            }
            else if (gameObject.GetComponent<StatsHolder>().enemyID == 3)
            {
                animator.SetBool("attack", true);
            }
            else if (gameObject.GetComponent<StatsHolder>().enemyID == 4)
            {

                animator.SetBool("attack2", true);
            }
            else if (gameObject.GetComponent<StatsHolder>().enemyID == 6)
            {
                animator.SetBool("attack", true);
            }
            else if (gameObject.GetComponent<StatsHolder>().enemyID == 7)
            {
                animator.SetBool("attack", true);
            }

            baseTimer += Time.deltaTime;

            foreach (BulletInfo bullet in atkInfos[currentAtkIndex].allBullets)
            {
                if (baseTimer >= bullet.shotTimer && bullet.fired == false)
                {
                    EnemyFire(bullet.bulletPrefab, firePoint.transform, bullet.angleDiff, bullet.accuracy, bullet.randomSpeed);
                    bullet.fired = true;
                }
            }
            int checker = 0;
            foreach (BulletInfo bulletsss in atkInfos[currentAtkIndex].allBullets)
            {
                if (bulletsss.fired == false)
                {
                    checker += 1;
                }
            }
            if (checker == 0)
            {
                baseTimer = 0;
                UpdateCheck = false;
                gameObject.GetComponent<StatsHolder>().ableToAttack = true;
            }
        }
        else if (UpdateCheck == false)
        {
            if (gameObject.GetComponent<StatsHolder>().enemyID == 5)
            {
                animator.SetBool("attack", false);
            }
            else if (gameObject.GetComponent<StatsHolder>().enemyID == 1)
            {
                animator.SetBool("attack", false);
            }
            else if (gameObject.GetComponent<StatsHolder>().enemyID == 2)
            {
                animator.SetBool("attack", false);
            }
            else if (gameObject.GetComponent<StatsHolder>().enemyID == 3)
            {
                animator.SetBool("attack", false);
            }
            else if (gameObject.GetComponent<StatsHolder>().enemyID == 4)
            {

                //Debug.Break();
                animator.SetBool("attack2", false);
            }
            else if (gameObject.GetComponent<StatsHolder>().enemyID == 6)
            {
                animator.SetBool("attack", false);
            }
            else if (gameObject.GetComponent<StatsHolder>().enemyID == 7)
            {
                animator.SetBool("attack", false);
            }
        }
    }

    public void EnemyFire(GameObject BulletPrefab, Transform firepoint, float angleDiff, float accuracy, float speed)//put normal or uni
    {
        float randomSpeed = 1;
        if (speed != 0)
        {
            randomSpeed = Random.Range(speed, 1 + speed);
        }

        float randomAccuracy = Random.Range(-accuracy, accuracy);
        Quaternion finalrotation = firepoint.rotation;
        finalrotation = Quaternion.Euler(0, 0, firepoint.rotation.eulerAngles.z + angleDiff + randomAccuracy);
        GameObject bullet = Instantiate(BulletPrefab, firepoint.position, finalrotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        bullet.GetComponent<BulletScript>().EnemyDamage(gameObject.GetComponent<StatsHolder>().damage);
        rb.velocity = bullet.transform.right * gameObject.GetComponent<StatsHolder>().bulletSpd * randomSpeed;
    }
}