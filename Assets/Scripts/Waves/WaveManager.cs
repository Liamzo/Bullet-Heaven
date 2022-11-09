using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject player;

    public float gameTimer = 0f;
    public float gameTimeLimit;
    public event System.Action GameFinished;
    public bool gameOver = false;
    
    public int wave;
    public int waveTime = 10;
    public List<Wave> waves = new List<Wave>();
    public Wave currentWave;
    float spawnCounter = 0f;
    public float spawnWidth;

    public List<GameObject> enemyPrefabs;


    // Extra Ideas
    float rampRate;
    List<int> extraWaves;

    // Start is called before the first frame update
    void Start()
    {
        if (waves.Count == 0) {
            Debug.LogError("No waves set");
            return;
        }

        gameTimeLimit = waves.Count * waveTime;
        wave = 0;

        spawnWidth = Camera.main.orthographicSize * Camera.main.aspect + 2;

        player.GetComponent<PlayerStatsHandler>().OnDeath += PlayerDeath;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true) {
            return;
        }

        gameTimer += Time.deltaTime;

        if (gameTimer >= gameTimeLimit) {
            // Game Won
            if (GameFinished != null) {
                gameOver = true;
                GameFinished();
            }
            return;
        }

        if (gameTimer >= wave * waveTime) {
            // New wave
            currentWave = waves[0];
            waves.RemoveAt(0);
            wave++;

            SpawnEnemies(currentWave.initialWave);

            if (currentWave.eliteStats != null) {
                // Pick random position
                Vector3 pos = UnityEngine.Random.insideUnitCircle.normalized * spawnWidth;

                pos += player.transform.position;

                GameObject enemy = Instantiate(enemyPrefabs[0], pos, Quaternion.identity);
                enemy.GetComponent<EnemyStatsHandler>().baseStats = currentWave.eliteStats;
            }
        } else {
            // Spawn enemies per sec
            // For now try constant, maybe try a wave every second
            spawnCounter += currentWave.spawnsPerSecond * Time.deltaTime;
            if (spawnCounter >= 1f) {
                int amount = (int)spawnCounter;
                SpawnEnemies(amount);
                spawnCounter -= amount;
            }

        }
    }

    void SpawnEnemies(int amount) {
        for (int i = 0; i < amount; i++) {
            // Pick random position
            Vector3 pos = UnityEngine.Random.insideUnitCircle.normalized * spawnWidth;

            pos += player.transform.position;

            BaseStats enemyStats = currentWave.GetBasicEnemy();

            GameObject enemy = Instantiate(enemyPrefabs[0], pos, Quaternion.identity);
            enemy.GetComponent<EnemyStatsHandler>().baseStats = enemyStats;
        }
    }

    void PlayerDeath(BaseController con) {
        gameOver = true;
    }
}
