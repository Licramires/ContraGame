using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int maxHealth = 5;
    private int currentHealth;

    [Header("Movement & Shooting")]
    public EnemyType type;
    public float moveSpeed = 3f;
    public float range = 5f;             // distancia mínima para disparar
    public Transform firePoint;
    public GameObject bulletPrefab;
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

    // ===============================
    void NormalBehavior()
    {
        // Quedarse quieto
        float distance = Vector2.Distance(transform.position, player.position);
        playerInRange = distance <= range;

        if (playerInRange)
        {
            RotateToPlayer();
            Shoot();
        }
    }

    void KamikaseBehavior()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        playerInRange = distance <= range;

        // Mover hacia adelante constantemente
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

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

        // Mover lentamente hacia adelante
        transform.Translate(Vector2.up * moveSpeed * 0.5f * Time.deltaTime);

        if (playerInRange)
        {
            RotateToPlayer();
            Shoot();
        }
    }

    void RotateToPlayer()
    {
        Vector2 dir = player.position - transform.position;
        float angleZ = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, -angleZ);
    }

    void Shoot()
    {
        if (shootTimer < timeBtwShoot)
        {
            shootTimer += Time.deltaTime;
        }
        else
        {
            shootTimer = 0f;
            if (bulletPrefab != null && firePoint != null)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
        }
    }

    // ===============================
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy recibió daño: " + damage + " | Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Asignar puntaje según tipo
        switch (type)
        {
            case EnemyType.Normal:
                ScoreManager.instance.AddScore(5);  // puntaje para enemigos normales
                break;
            case EnemyType.Kamikase:
                ScoreManager.instance.AddScore(10);
                break;
            case EnemyType.Sniper:
                ScoreManager.instance.AddScore(20);
                break;
        }

        Debug.Log("Enemy murió | Puntaje total: " + ScoreManager.instance.score);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player p = collision.gameObject.GetComponent<Player>();
            if (p != null)
            {
                p.TakeDamage(1); // daño de colisión
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
