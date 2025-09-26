using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator; 


    public float velocity = 5f;
    public float jumpForce = 7f;
    private float playerHalfHeight;


    public SpriteRenderer spriteRenderer;

  
    public int maxLives = 3;
    private int currentLives;
    public int hpPerLife = 1;
    private int currentHP;


    public GameObject bulletPrefab;   
    public Transform firePoint;       
    public float bulletSpeed = 10f;
    public float timeBtwShoot = 0.5f;
    private float shootTimer = 0f;


    private float originalTimeBtwShoot;
    private bool hasFastShoot = false;
    private bool hasDoubleBullet = false;
    private float fastShootTimer = 0f;
    private float doubleBulletTimer = 0f;
    public int GetCurrentLives() { return currentLives; }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
        playerHalfHeight = spriteRenderer.bounds.extents.y;

        currentLives = maxLives;
        currentHP = hpPerLife;

       
        originalTimeBtwShoot = timeBtwShoot;
    }

    void Update()
    {
        Movement();
        Jump();
        Shoot();
        UpdatePowerUps();
    }

    void Movement()
    {
        float movementInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movementInput * velocity, rb.velocity.y);

        bool isMoving = Mathf.Abs(movementInput) > 0.1f;
        if (animator != null)
            animator.SetBool("isWalking", isMoving);

        if (movementInput > 0) 
        {
            spriteRenderer.flipX = false;
        }
        else if (movementInput < 0) 
        {
            spriteRenderer.flipX = true;
        }
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
                if (hasDoubleBullet)
                {
                    // Disparo doble - una bala arriba y una abajo
                    Vector3 pos1 = firePoint.position + Vector3.up * 0.3f;
                    Vector3 pos2 = firePoint.position + Vector3.down * 0.3f;

                    GameObject b1 = Instantiate(bulletPrefab, pos1, firePoint.rotation);
                    GameObject b2 = Instantiate(bulletPrefab, pos2, firePoint.rotation);

                    Rigidbody2D b1Rb = b1.GetComponent<Rigidbody2D>();
                    Rigidbody2D b2Rb = b2.GetComponent<Rigidbody2D>();

                    if (b1Rb != null) b1Rb.velocity = transform.right * bulletSpeed;
                    if (b2Rb != null) b2Rb.velocity = transform.right * bulletSpeed;
                }
                else
                {
                    // Disparo normal
                    GameObject b = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    Rigidbody2D bRb = b.GetComponent<Rigidbody2D>();
                    if (bRb != null)
                        bRb.velocity = transform.right * bulletSpeed;
                }
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
        currentHP -= damage;

        if (currentHP <= 0)
        {
            currentLives--;
            if (currentLives > 0)
            {
                currentHP = hpPerLife; 
            }
            else
            {
                Die();
            }
        }
    }

    void Die()
    {
        SceneManager.LoadScene("GameOver");
    }

    // SISTEMA DE POWER-UPS
    public void ApplyPowerUp(PowerUpType type, float duration)
    {
        switch (type)
        {
            case PowerUpType.FastShoot:
                hasFastShoot = true;
                fastShootTimer = duration;
                timeBtwShoot = originalTimeBtwShoot / 1.5f;
                Debug.Log("Disparo rápido activado por " + duration + " segundos");
                break;

            case PowerUpType.DoubleBullet:
                hasDoubleBullet = true;
                doubleBulletTimer = duration;
                Debug.Log("Disparo doble activado por " + duration + " segundos");
                break;
        }
    }

    void UpdatePowerUps()
    {
        if (hasFastShoot)
        {
            fastShootTimer -= Time.deltaTime;
            if (fastShootTimer <= 0)
            {
                hasFastShoot = false;
                timeBtwShoot = originalTimeBtwShoot;
                
            }
        }
        if (hasDoubleBullet)
        {
            doubleBulletTimer -= Time.deltaTime;
            if (doubleBulletTimer <= 0)
            {
                hasDoubleBullet = false;
         
            }
        }
    }
}