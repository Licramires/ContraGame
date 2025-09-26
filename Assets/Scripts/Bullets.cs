using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    
    public float moveSpeed = 6f;                 
    public float timeToDestroy = 5f;             
    public float damage = 1f;                   
    public bool playerBullet = false;           
    public bool criticalHit = false;             
    public float criticalDamage = 2f;            
    [Range(0f, 1f)] public float criticalChance = 0.3f; 

    void Start()
    {
       
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
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
        
        if (!playerBullet && collision.CompareTag("Player"))
        {
            Player p = collision.GetComponent<Player>();
            if (p != null)
                p.TakeDamage(Mathf.RoundToInt(GetCriticalDamage()));

            Destroy(gameObject);
            return; 
        }

       
        if (playerBullet && collision.CompareTag("Enemy"))
        {
            Enemy e = collision.GetComponent<Enemy>();
            if (e != null)
                e.TakeDamage(Mathf.RoundToInt(GetCriticalDamage()));

            Destroy(gameObject);
            return;
        }

        
        if (collision.CompareTag("WallLeft") || collision.CompareTag("WallRight"))
        {
            Destroy(gameObject);
            return;
        }
    }

}
