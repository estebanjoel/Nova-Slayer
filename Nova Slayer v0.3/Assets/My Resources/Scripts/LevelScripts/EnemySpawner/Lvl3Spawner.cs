using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl3Spawner : EnemySpawner
{
    [SerializeField] float[] boss1Position = new float[3];
    [SerializeField] GameObject[] miniBosses;
    public float enemiesSpawnRateInBossTurn;
    float remainingTimeToSpawnEnemies;
    void Start()
    {
        int TotalPositions = xPositions.Length * yPositions.Length;
        while(TotalPositions>0)
        {
            occupiedPositions.Add(false);
            TotalPositions--;
        }
        waves = 11;
        remainingTimeToSpawnEnemies = enemiesSpawnRateInBossTurn;    
    }

    void Update()
    {
        if(canCheckBoss)
        {
            if(GameObject.FindGameObjectWithTag("Boss") != null)
            {
                if(remainingTimeToSpawnEnemies <= 0)
                {
                    SpawnEnemies(Random.Range(3,5), 0);
                    remainingTimeToSpawnEnemies = enemiesSpawnRateInBossTurn;    
                }

                else
                {
                    remainingTimeToSpawnEnemies -= Time.deltaTime;
                }
            }

            else
            {
                if(GameObject.FindGameObjectsWithTag("Spaceship").Length > 0)
                {
                    foreach(GameObject spaceship in GameObject.FindGameObjectsWithTag("Spaceship"))
                    {
                        spaceship.GetComponent<EnemyBody>().health = 0;
                    }
                }
            }
        }
    }
    
    public override void spawnWave()
    {
        actualWave++;
        switch(actualWave)
        {
            case 1:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(8,12), Random.Range(12,16), Random.Range(16,20));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn, 0, 3);
                break;
            case 2:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(10,14), Random.Range(14,18), Random.Range(18,22));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn, 1, 3);
                break;
            case 3:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(12,18), Random.Range(16,22), Random.Range(20,26));
                SpawnEnemies(quantityOfEnemiesToSpawn, 2);
                break;
            case 4:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(16,21), Random.Range(20,25), Random.Range(24,29));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn, 1, 4);
                break;
            case 5:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(18,22), Random.Range(22,28), Random.Range(26,32));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn, 2, 4);
                break;
            case 6:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(22,27), Random.Range(25,30), Random.Range(28,33));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn, 1, 5);
                break;
            case 7:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(20,28), Random.Range(24,32), Random.Range(28,36));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn, 2, 5);
                break;
            case 8:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(24,30), Random.Range(28,36), Random.Range(32,40));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn, 2, 6);
                break;
            case 9:
                spawnMiniBoss(0);
                UICanvas.instance.bossHealthBars.SetHealthBars();
                break;
            case 10:
                spawnMiniBoss(1);
                UICanvas.instance.bossHealthBars.SetHealthBars();
                break;
            case 11:
                spawnBoss();
                canCheckBoss = true;
                UICanvas.instance.bossHealthBars.SetHealthBars();
                break;
        }

        for(int i=0;i<occupiedPositions.Count; i++)
        {
            if(occupiedPositions[i]) occupiedPositions[i]=false;
        }
    }

    public void spawnMiniBoss(int i)
    {
        mySpawn.prefabToSpawn = miniBosses[i];
        mySpawn.prefabToSpawn.GetComponent<EnemyBody>().xPosition = boss1Position[0];
        mySpawn.prefabToSpawn.GetComponent<EnemyBody>().yPosition = boss1Position[1];
        mySpawn.Create();
        if(GameManager.instance.currentDifficulty == 2)
        {
            SpawnEnemiesRandom(Random.Range(6,10), 0, 6);
        }
    }

    public override void spawnBoss()
    {
        mySpawn.prefabToSpawn = boss;
        mySpawn.prefabToSpawn.GetComponent<EnemyBody>().xPosition = boss1Position[0];
        mySpawn.prefabToSpawn.GetComponent<EnemyBody>().yPosition = boss1Position[1];
        mySpawn.Create();
        if(GameManager.instance.currentDifficulty == 2)
        {
            SpawnEnemiesRandom(Random.Range(10,16), 0, 6);
        }
    }
}
