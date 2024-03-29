using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int enemyCount;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnDistance;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy(spawnDistance, 1, true, 5f));
    }

    private IEnumerator SpawnEnemy(float distFromCenter, int amount, bool repeat, float interval)
    {
        while(true)
        {
            for (int i = 0; i < amount; i++)
            {
                float angle = Random.value * 360;
                Instantiate(enemyPrefab, new Vector3(distFromCenter * Mathf.Cos(angle * Mathf.Deg2Rad), distFromCenter * Mathf.Sin(angle * Mathf.Deg2Rad)), Quaternion.identity);
            }
            if (!repeat)
            {
                break;
            }
            yield return new WaitForSeconds(interval);
        }
    }
}
