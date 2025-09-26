using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
  
    public float moveSpeed = 10f;       
    public float timeToDestroy = 5f;    
    public int damage = 1;              

    void Start()
    {
        
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
     
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Enemy"))
        {
            Enemy e = collision.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(damage);
            }

            Destroy(gameObject); 
            return;
        }

        
        if (collision.CompareTag("WallRight"))
        {
            Destroy(gameObject);
            return;
        }
    }
}
