using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int initialWave;
    public int spawnsPerSecond;

    public List<SpawnChance> enemiesStats;
    public BaseStats eliteStats;


    public BaseStats GetBasicEnemy() {
        int chance = 0;

        enemiesStats.ForEach(x => chance += x.spawnChance);

        int rand = Random.Range(1, chance+1);
        int counter = 0;
        while (rand > 0) {
            rand -= enemiesStats[counter].spawnChance;

            if (rand > 0) {
                counter++;
            }
        }
        
        return enemiesStats[counter].enemyStats;
    }


    [System.Serializable]
    public struct SpawnChance {
        public BaseStats enemyStats;
        public int spawnChance;
    }
}
