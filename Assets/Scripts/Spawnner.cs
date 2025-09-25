using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{
    [Header("Spawn Points")]
    public Transform topPoint;
    public Transform bottomPoint;

    [Header("Enemies")]
    public List<GameObject> enemyPrefabs;
    public float timeBtwSpawn = 1.5f;

    void Start()
    {
        // Comenzar a spawnear enemigos automáticamente
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true) // spawnea infinitamente
        {
            if (enemyPrefabs.Count > 0)
            {
                // Elegir un enemigo al azar
                GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

                // Calcular posición aleatoria entre los puntos
                float x = transform.position.x;
                float y = Random.Range(bottomPoint.position.y, topPoint.position.y);
                Vector2 spawnPos = new Vector2(x, y);

                // Instanciar enemigo
                Instantiate(enemy, spawnPos, Quaternion.identity);
            }

            // Esperar antes de spawnear el siguiente
            yield return new WaitForSeconds(timeBtwSpawn);
        }
    }
}
