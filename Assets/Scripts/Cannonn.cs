using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonn : MonoBehaviour
{
    [Header("Cannon Settings")]
    public Transform firePoint;          // Punto desde donde dispara
    public Bullets bulletPrefab;         // Prefab de la bala (Bullets.cs)
    public float timeBtwShoot = 1f;      // Tiempo entre disparos
    public float bulletSpeed = 7f;       // Velocidad de la bala
    public float damage = 1f;            // Daño base
    public bool criticalHit = false;     // Habilitar críticos

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
            b.playerBullet = false; // bala del cañón
        }
    }
}
