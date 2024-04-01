using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private float enemySpawnDistance;
    [SerializeField] private float enemyInitialInterval;
    [SerializeField] private float enemyIntervalMult;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyLoop(1, true, enemyInitialInterval));
    }

    private IEnumerator SpawnEnemyLoop(int amount, bool repeat, float interval)
    {
        while(true)
        {
            for (int i = 0; i < amount; i++)
            {
                
                int randomChance = Random.Range(1, 101);
                switch (randomChance)
                {
                    // Default
                    case <= 40:
                        SpawnEnemy(enemies[0], enemySpawnDistance);
                        break;
                    // Exploding
                    case <= 55:
                        SpawnEnemy(enemies[1], enemySpawnDistance);
                        break;
                    // Virus
                    case <= 65:
                        SpawnEnemy(enemies[2], enemySpawnDistance);
                        break;
                    // Shooting
                    case <= 85:
                        SpawnEnemy(enemies[3], enemySpawnDistance);
                        break;
                    // Splitting
                    case <= 100:
                        SpawnEnemy(enemies[4], enemySpawnDistance);
                        break;
                }
                
            }
            if (!repeat)
            {
                break;
            }
            if (interval > 1)
            {
                interval *= enemyIntervalMult;
            }
            yield return new WaitForSeconds(interval);
        }
    }

    private void SpawnEnemy(GameObject enemy, float dist)
    {
        float angle = Random.value * 360;
        Instantiate(enemy, new Vector3(dist * Mathf.Cos(angle * Mathf.Deg2Rad), dist * Mathf.Sin(angle * Mathf.Deg2Rad)), Quaternion.identity);
    }
}
