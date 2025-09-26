using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpType type;
    public float duration = 10f; 

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.ApplyPowerUp(type, duration);
                Debug.Log("Power-Up recogido: " + type);
                Destroy(gameObject);
            }
        }
    }
}

public enum PowerUpType
{
    FastShoot, // disparo más rápido
    DoubleBullet // disparo doble
}