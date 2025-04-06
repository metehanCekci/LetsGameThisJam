using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // <-- Bunu ekle

public class EnemySpawnerScript : MonoBehaviour
{
    public SceneChangeScript sceneChangeScript;

    [Header("Spawn Settings")]
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;
    public int maxEnemies = 2;

    private int currentEnemyCount = 0;
    private int totalSpawnedEnemies = 0;
    public int totalEnemiesToSpawn = 10; // örnek: tüm düşmanlar bir defa üretilecekse

    void Start()
    {
        //sceneChangeScript =FindObjectOfType<SceneChangeScript>();
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (totalSpawnedEnemies < totalEnemiesToSpawn)
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

        GameObject selectedEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject newEnemy = Instantiate(selectedEnemy, spawnPoint.position, Quaternion.identity);
        currentEnemyCount++;
        totalSpawnedEnemies++;

        EnemyHealthScript enemyHealth = newEnemy.GetComponent<EnemyHealthScript>();
        if (enemyHealth != null)
        {
            enemyHealth.OnDeath += EnemyDied;
        }
    }

    void EnemyDied()
    {
        currentEnemyCount--;
        GameManagerScript.instance.GoldAmount += (5 * Mathf.RoundToInt(GameManagerScript.instance.goldMultiplier));
        // Hepsi öldü mü?
        if (totalSpawnedEnemies >= totalEnemiesToSpawn && currentEnemyCount <= 0)
        {
            Debug.Log("All enemies are dead. Loading next scene...");
            LoadNextScene();
        }
    }

public void LoadNextScene()
{
    GameManagerScript.instance.level++;
        sceneChangeScript.ToMarket();
}

}
