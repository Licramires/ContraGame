using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float moveSpeed = 10f;       // velocidad de la bala
    public float timeToDestroy = 5f;    // tiempo antes de destruirse
    public int damage = 1;              // daño base

    void Start()
    {
        // Destruir automáticamente después de cierto tiempo
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        // Mover la bala hacia la derecha (hacia adelante del jugador)
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Daño a cualquier enemigo
        if (collision.CompareTag("Enemy"))
        {
            Enemy e = collision.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(damage);
            }

            Destroy(gameObject); // destruye la bala al impactar
            return;
        }

        // Destruir la bala al chocar con la pared derecha
        if (collision.CompareTag("WallRight"))
        {
            Destroy(gameObject);
            return;
        }
    }
}
