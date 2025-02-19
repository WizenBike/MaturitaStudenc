using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [SerializeField] private TMP_Text waveText;
    [SerializeField] private int baseEnemiesPerWave = 10;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float difficultyMultiplier = 1.2f; 

    private int currentWave = 1;
    private int enemiesRemaining;
    private int totalEnemiesToSpawn;
    private int enemiesSpawned;
    [SerializeField] private List<EnemySpawner> spawners = new List<EnemySpawner>();
    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartNewWave();
    }

    public void RegisterSpawner(EnemySpawner spawner)
    {
        if (!spawners.Contains(spawner))
        {
            spawners.Add(spawner);
        }
    }

    private void StartNewWave()
    {
        totalEnemiesToSpawn = Mathf.RoundToInt((baseEnemiesPerWave + (currentWave - 1) * 2) * difficultyMultiplier);
        enemiesRemaining = totalEnemiesToSpawn;
        enemiesSpawned = 0;
        UpdateWaveText();

        if (spawners.Count == 0)
        {
            return;
        }

        StartCoroutine(SpawnEnemiesOverTime());
    }

    private IEnumerator SpawnEnemiesOverTime()
    {
        while (enemiesSpawned < totalEnemiesToSpawn)
        {
            for (int i = 0; i < spawners.Count; i++)
            {
                if (enemiesSpawned < totalEnemiesToSpawn)
                {
                    spawners[i].SpawnEnemy();
                    enemiesSpawned++;
                }
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }

    public void EnemySpawned(GameObject enemy)
    {
        activeEnemies.Add(enemy);
    }

    public void EnemyDefeated(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        enemiesRemaining--;

        if (enemiesRemaining <= 0)
        {
            currentWave++;
            StartCoroutine(WaitAndStartNextWave());
        }
    }

    private IEnumerator WaitAndStartNextWave()
    {
        yield return new WaitForSeconds(2f);
        StartNewWave();
    }

    private void UpdateWaveText()
    {
        if (waveText != null)
        {
            waveText.text = "Wave: " + currentWave;
        }
    }
}
