using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con Text

public class Final : MonoBehaviour
{
    [Header("UI")]
    public Text victoryText; // Arrastra aquí el Text desde el Canvas

    private void Start()
    {
        // Ocultar el texto al inicio
        if (victoryText != null)
            victoryText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta si el jugador llegó a la zona final
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Ganaste!");

            if (victoryText != null)
            {
                victoryText.gameObject.SetActive(true); // mostrar mensaje
                victoryText.text = "¡Victoria!";        // texto que quieras
            }

            Time.timeScale = 0f; // pausa el juego
        }
    }
}
