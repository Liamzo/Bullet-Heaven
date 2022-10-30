using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int initialWave;
    public int spawnsPerSecond;

    public List<SpawnChance> enemyPrefabs;
    public GameObject elitePrefab;


    public GameObject GetBasicEnemy() {
        int chance = 0;

        enemyPrefabs.ForEach(x => chance += x.spawnChance);

        int rand = Random.Range(1, chance+1);
        int counter = 0;
        while (rand > 0) {
            rand -= enemyPrefabs[counter].spawnChance;

            if (rand > 0) {
                counter++;
            }
        }
        
        return enemyPrefabs[counter].enemyPrefab;
    }


    [System.Serializable]
    public struct SpawnChance {
        public GameObject enemyPrefab;
        public int spawnChance;
    }
}
