using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float moveSpeed = 6f;                 // velocidad de la bala
    public float timeToDestroy = 5f;             // tiempo antes de destruirse
    public float damage = 1f;                    // daño base
    public bool playerBullet = false;            // marcar si la bala es del jugador
    public bool criticalHit = false;             // habilitar críticos
    public float criticalDamage = 2f;            // multiplicador de daño crítico
    [Range(0f, 1f)] public float criticalChance = 0.3f; // probabilidad de crítico

    void Start()
    {
        // Destruir automáticamente después de cierto tiempo
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        // Moverse siempre hacia la izquierda
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    float GetCriticalDamage()
    {
        if (criticalHit)
        {
            if (Random.value <= criticalChance)
            {
                return damage * criticalDamage;
            }
        }
        return damage;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 🔹 Bala enemiga -> daña al Player
        if (!playerBullet && collision.CompareTag("Player"))
        {
            Player p = collision.GetComponent<Player>();
            if (p != null)
                p.TakeDamage(Mathf.RoundToInt(GetCriticalDamage()));

            Destroy(gameObject);
            return; // importante para que no siga evaluando
        }

        // 🔹 Bala del jugador -> daña a Enemigos
        if (playerBullet && collision.CompareTag("Enemy"))
        {
            Enemy e = collision.GetComponent<Enemy>();
            if (e != null)
                e.TakeDamage(Mathf.RoundToInt(GetCriticalDamage()));

            Destroy(gameObject);
            return;
        }

        // 🔹 Pared
        if (collision.CompareTag("WallLeft") || collision.CompareTag("WallRight"))
        {
            Destroy(gameObject);
            return;
        }
    }

}
