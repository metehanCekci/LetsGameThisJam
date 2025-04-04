using System.Collections;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] enemyPrefabs; // Add multiple enemy prefabs here
    public Transform[] spawnPoints;   // Add multiple spawn positions
    public float spawnInterval = 5f;
    public int maxEnemies = 20;

    private int currentEnemyCount = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (currentEnemyCount < maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No enemies or spawn points assigned!");
            return;
        }

        // Pick a random enemy type and spawn point
        GameObject selectedEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject newEnemy = Instantiate(selectedEnemy, spawnPoint.position, Quaternion.identity);
        currentEnemyCount++;

        // Subscribe to death event if supported
        EnemyHealthScript enemyHealth = newEnemy.GetComponent<EnemyHealthScript>();
        if (enemyHealth != null)
        {
            enemyHealth.OnDeath += EnemyDied;
        }
    }

    void EnemyDied()
    {
        currentEnemyCount--;
    }
}
