using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    public EnemyType type;
    public float moveSpeed = 3f;
    public float range = 5f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float timeBtwShoot = 1f;
    private float shootTimer = 0f;

    private Transform player;
    private bool playerInRange = false;

    void Start()
    {
        currentHealth = maxHealth;
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO != null)
            player = playerGO.transform;
    }

    void Update()
    {
        if (player == null) return;

        switch (type)
        {
            case EnemyType.Normal:
                NormalBehavior();
                break;

            case EnemyType.Kamikase:
                KamikaseBehavior();
                break;

            case EnemyType.Sniper:
                SniperBehavior();
                break;
        }
    }

    void NormalBehavior()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        playerInRange = distance <= range;

        if (playerInRange)
        {
           
            Shoot();
        }
    }

    void KamikaseBehavior()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        playerInRange = distance <= range;

        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        if (playerInRange)
        {
            RotateToPlayer();
            Shoot();
        }
    }

    void SniperBehavior()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        playerInRange = distance <= range;

        transform.Translate(Vector3.right * moveSpeed * 0.5f * Time.deltaTime);

        if (playerInRange)
        {
            RotateToPlayer();
            Shoot();
        }
    }

    void RotateToPlayer()
    {
        Vector2 dir = player.position - transform.position;
        float angleZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angleZ);
    }



    void Shoot()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= timeBtwShoot)
        {
            shootTimer = 0f;
            if (bulletPrefab != null && firePoint != null)
            {
                GameObject b = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
                if (rb != null)
                    rb.velocity = firePoint.right * bulletSpeed;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        switch (type)
        {
            case EnemyType.Normal:
                ScoreManager.instance.AddScore(5);
                break;
            case EnemyType.Kamikase:
                ScoreManager.instance.AddScore(10);
                break;
            case EnemyType.Sniper:
                ScoreManager.instance.AddScore(20);
                break;
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player p = collision.gameObject.GetComponent<Player>();
            if (p != null)
            {
                p.TakeDamage(1);
            }
            Destroy(gameObject);
        }
    }
}

public enum EnemyType
{
    Normal,
    Kamikase,
    Sniper
}
