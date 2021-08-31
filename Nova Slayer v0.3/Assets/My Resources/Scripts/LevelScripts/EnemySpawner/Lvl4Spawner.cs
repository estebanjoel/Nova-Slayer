using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl4Spawner : EnemySpawner
{
    [SerializeField] float[] singleBossPosition = new float[3];
    [SerializeField] float[] boss1Position = new float[3];
    [SerializeField] float[] boss2Position = new float[3];
    [SerializeField] GameObject[] miniBosses;
    public float magneticSpawnRate;
    float remainingTimeToSpawnMagnetic;

     void Start()
    {
        int TotalPositions = xPositions.Length * yPositions.Length;
        while(TotalPositions>0)
        {
            occupiedPositions.Add(false);
            TotalPositions--;
        }
        waves = 12;
        remainingTimeToSpawnMagnetic = magneticSpawnRate;
    }

    void Update()
    {
        if(!GameManager.instance.victoryCondition && !GameManager.instance.failCondition)
        {
            if(GameObject.FindObjectOfType<Hercules8999>() == null)
            {
                if(remainingTimeToSpawnMagnetic <= 0)
                {
                    SpawnEnemies(Random.Range(1,3), 9);
                    remainingTimeToSpawnMagnetic = magneticSpawnRate;
                }
                else
                {
                    remainingTimeToSpawnMagnetic -= Time.deltaTime;
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
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(6,8), Random.Range(10,12), Random.Range(16,18));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn, 0, 3);
                break;
            case 2:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(10,12), Random.Range(14,16), Random.Range(18,20));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn, 0, 5);
                break;
            case 3:
                if(GameManager.instance.currentDifficulty == 0) spawnMiniBoss(0);
                else spawnDoubleMiniBoss(0, 0);
                UICanvas.instance.bossHealthBars.SetHealthBars();
                break;
            case 4:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(14,16), Random.Range(18,20), Random.Range(22,24));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn, 3, 8);
                UICanvas.instance.bossHealthBars.HideHealthBars();
                break;
            case 5:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(18,20), Random.Range(22,24), Random.Range(26,28));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn, 6, 8);
                break;
            case 6:
                if(GameManager.instance.currentDifficulty != 0)
                {
                    spawnMiniBoss(1);
                    UICanvas.instance.bossHealthBars.SetHealthBars();
                }
                break;
            case 7:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(20,22), Random.Range(24,26), Random.Range(28,32));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn, 0, 8);
                UICanvas.instance.bossHealthBars.HideHealthBars();
                break;
            case 8:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(22,26), Random.Range(28,30), Random.Range(32,34));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn, 0, 8);
                break;
            case 9:
                if(GameManager.instance.currentDifficulty == 2) spawnMiniBoss(4);
                else spawnDoubleMiniBoss(2,3);
                UICanvas.instance.bossHealthBars.SetHealthBars();
                break;
            case 10:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(26,30), Random.Range(32,36), Random.Range(38,40));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn, 0, 9);
                UICanvas.instance.bossHealthBars.HideHealthBars();
                break;
            case 11:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(30,32), Random.Range(34,38), Random.Range(40,44));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn, 4, 9);
                break;
            case 12:
                Debug.Log("Boss");
                spawnBoss();
                UICanvas.instance.bossHealthBars.SetHealthBars();
                canCheckBoss = true;
                break;
        }
        
        for(int i=0;i<occupiedPositions.Count; i++)
        {
            if(occupiedPositions[i]) occupiedPositions[i]=false;
        }
    }

    public override void spawnBoss()
    {
        mySpawn.prefabToSpawn = boss;
        mySpawn.prefabToSpawn.GetComponent<EnemyBody>().xPosition = singleBossPosition[0];
        mySpawn.prefabToSpawn.GetComponent<EnemyBody>().yPosition = singleBossPosition[1];
        mySpawn.Create();
        if(GameManager.instance.currentDifficulty == 2)
        {
            SpawnEnemiesRandom(Random.Range(15,20), 0, 9);
        }
    }

    public void spawnMiniBoss(int i)
    {
        mySpawn.prefabToSpawn = miniBosses[i];
        mySpawn.prefabToSpawn.GetComponent<EnemyBody>().xPosition = singleBossPosition[0];
        mySpawn.prefabToSpawn.GetComponent<EnemyBody>().yPosition = singleBossPosition[1];
        mySpawn.Create();
        if(GameManager.instance.currentDifficulty == 2)
        {
            SpawnEnemiesRandom(Random.Range(12,25), 0, 7);
        }
    }

    public void spawnDoubleMiniBoss(int firstBoss, int secondBoss)
    {
        mySpawn.prefabToSpawn = miniBosses[firstBoss];
        mySpawn.prefabToSpawn.GetComponent<EnemyBody>().xPosition = boss1Position[0];
        mySpawn.prefabToSpawn.GetComponent<EnemyBody>().yPosition = boss1Position[1];
        mySpawn.Create();
        mySpawn.prefabToSpawn = miniBosses[secondBoss];
        mySpawn.prefabToSpawn.GetComponent<EnemyBody>().xPosition = boss2Position[0];
        mySpawn.prefabToSpawn.GetComponent<EnemyBody>().yPosition = boss2Position[1];
        mySpawn.Create();
        if(GameManager.instance.currentDifficulty == 2)
        {
            SpawnEnemiesRandom(Random.Range(6,10), 0, 4);
        }
    }
}
