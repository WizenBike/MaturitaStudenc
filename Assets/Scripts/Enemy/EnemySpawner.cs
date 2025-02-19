using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    private bool isSpawning = false;

    private void Start()
    {
        if (WaveManager.Instance == null)
        {
            Debug.LogError("WaveManager Instance not found! Ensure WaveManager is in the scene.");
        }
        else
        {
            WaveManager.Instance.RegisterSpawner(this);
        }
    }

    public void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0)
        {
            Debug.LogError("No enemy prefabs assigned to spawner.");
            return;
        }

        int rand = Random.Range(0, enemyPrefabs.Length);
        GameObject enemy = Instantiate(enemyPrefabs[rand], transform.position, Quaternion.identity);

        if (enemy != null)
        {
            WaveManager.Instance.EnemySpawned(enemy);
        }
        else
        {
            Debug.LogError("Failed to spawn an enemy!");
        }
    }
}
