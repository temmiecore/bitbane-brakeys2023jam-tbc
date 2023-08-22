using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public List<Enemy> EnemyPrefabs;

    public float minSpawnTime;
    public float maxSpawnTime;

    public float spawnCircleRadius;

    private void Start()
    {
        StartCoroutine(SpawnWaveCoroutine(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count - 1)], Random.Range(30, 80)));
    }

    private IEnumerator SpawnWaveCoroutine(Enemy enemyPrefab, int enemyCount)
    {
       
        for (int j = 0; j < enemyCount; j++)
        {
            Vector2 randomPosition = Random.insideUnitCircle.normalized * spawnCircleRadius;

            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(0, 2f));
        }

        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        StartCoroutine(SpawnWaveCoroutine(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count - 1)], Random.Range(30, 80)));
    }

}
