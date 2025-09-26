using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonn : MonoBehaviour
{

    public Transform firePoint;          
    public Bullets bulletPrefab;         
    public float timeBtwShoot = 1f;     
    public float bulletSpeed = 7f;       
    public float damage = 1f;            
    public bool criticalHit = false;     

    private float timer = 0f;

    void Update()
    {
        CheckIfCanShoot();
    }

    void CheckIfCanShoot()
    {
        if (timer <= timeBtwShoot)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Bullets b = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            b.damage = damage;
            b.moveSpeed = bulletSpeed;
            b.criticalHit = criticalHit;
            b.playerBullet = false; 
        }
    }
}
