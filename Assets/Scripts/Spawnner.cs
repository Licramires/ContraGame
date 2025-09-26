using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{

    public Transform topPoint;
    public Transform bottomPoint;
    public List<GameObject> enemyPrefabs;
    public float timeBtwSpawn = 1.5f;
    void Start()
    {
       
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true) 
        {
            if (enemyPrefabs.Count > 0)
            {
                
                GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

              
                float x = transform.position.x;
                float y = Random.Range(bottomPoint.position.y, topPoint.position.y);
                Vector2 spawnPos = new Vector2(x, y);

            
                Instantiate(enemy, spawnPos, Quaternion.identity);
            }

            
            yield return new WaitForSeconds(timeBtwSpawn);
        }
    }
}
