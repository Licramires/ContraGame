using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Movement Settings")]
    public float velocity = 5f;
    public float jumpForce = 7f;
    private float playerHalfHeight;

    [Header("Components")]
    public SpriteRenderer spriteRenderer;

    [Header("Health Settings")]
    public int maxHealth = 10;
    private int currentHealth;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;   // prefab de la bala
    public Transform firePoint;       // punto desde donde dispara
    public float bulletSpeed = 10f;
    public float timeBtwShoot = 0.5f;
    private float shootTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHalfHeight = spriteRenderer.bounds.extents.y;

        currentHealth = maxHealth; // inicializa vida
    }

    void Update()
    {
        Movement();
        Jump();
        Shoot();
    }

    void Movement()
    {
        float movementInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movementInput * velocity, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.X) && IsGround())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    bool IsGround()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, playerHalfHeight + 0.1f, LayerMask.GetMask("Ground"));
    }

    void Shoot()
    {
        if (Input.GetKey(KeyCode.Z) && shootTimer >= timeBtwShoot)
        {
            if (bulletPrefab != null && firePoint != null)
            {
                GameObject b = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D bRb = b.GetComponent<Rigidbody2D>();
                if (bRb != null)
                    bRb.velocity = transform.right * bulletSpeed; // dispara hacia la derecha del player
            }
            shootTimer = 0f;
        }
        else
        {
            shootTimer += Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player recibió daño: " + damage + " | Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player murió");
        gameObject.SetActive(false);
    }
}
