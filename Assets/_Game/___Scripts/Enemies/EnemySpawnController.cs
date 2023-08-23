using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public List<Enemy> EnemyPrefabs;

    public float minSlowSpawnTime;
    public float maxSlowSpawnTime;

    public float minFastSpawnTime;
    public float maxFastSpawnTime;

    public float spawnCircleRadius;

    private void Start()
    {
        StartCoroutine(SpawnSlowWaveCoroutine(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count - 1)], Random.Range(40, 80)));
        StartCoroutine(SpawnFastWaveCoroutine(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count - 1)], Random.Range(10, 30)));
    }

    private IEnumerator SpawnSlowWaveCoroutine(Enemy enemyPrefab, int enemyCount)
    {
       
        for (int j = 0; j < enemyCount; j++)
        {
            Vector3 randomPosition = Random.insideUnitCircle.normalized * spawnCircleRadius;

            Instantiate(enemyPrefab, GameManager.Instance.playerMover.transform.position + randomPosition, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(0, 2f));
        }

        yield return new WaitForSeconds(Random.Range(minSlowSpawnTime, maxSlowSpawnTime));
        StartCoroutine(SpawnSlowWaveCoroutine(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count - 1)], Random.Range(40, 80)));
    }

    private IEnumerator SpawnFastWaveCoroutine(Enemy enemyPrefab, int enemyCount)
    {
        yield return new WaitForSeconds(Random.Range(minFastSpawnTime, maxFastSpawnTime));

        for (int j = 0; j < enemyCount; j++)
        {
            Vector3 randomPosition = Random.insideUnitCircle.normalized * spawnCircleRadius;

            Instantiate(enemyPrefab, GameManager.Instance.playerMover.transform.position + randomPosition, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(0, 0.1f));
        }

        StartCoroutine(SpawnSlowWaveCoroutine(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count - 1)], Random.Range(10, 30)));
    }
}
