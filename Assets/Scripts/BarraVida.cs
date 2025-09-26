using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image rellenoVida;
    private Player player;
    private float fullLife;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        fullLife = player.maxLives; 
    }

    void Update()
    {
      
        rellenoVida.fillAmount = (float)player.GetCurrentLives() / fullLife;
    }
}
