using UnityEngine;

public class Caja : MonoBehaviour
{
    public float life = 1; 


    public GameObject[] powerUpPrefabs;

    void Update()
    {

        transform.Translate(Vector2.right * Time.deltaTime * 2f);
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Break();
        }
    }

    void Break()
    {
        DropPowerUp();
        Destroy(gameObject);
    }

    void DropPowerUp()
    {
        if (powerUpPrefabs.Length > 0)
        {
            int rand = Random.Range(0, powerUpPrefabs.Length);
            Instantiate(powerUpPrefabs[rand], transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }
}
