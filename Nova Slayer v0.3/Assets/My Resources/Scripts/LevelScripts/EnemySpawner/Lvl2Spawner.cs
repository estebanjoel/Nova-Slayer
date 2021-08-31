using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl2Spawner : EnemySpawner
{
    [SerializeField] float[] boss1Position = new float[3];
    void Start()
    {
        int TotalPositions = xPositions.Length * yPositions.Length;
        while(TotalPositions>0)
        {
            occupiedPositions.Add(false);
            TotalPositions--;
        }
        waves = 8;    
    }
    
    public override void spawnWave()
    {
        actualWave++;
        switch(actualWave)
        {
            case 1:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(4,9), Random.Range(7, 12), Random.Range(10,15));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn, 0, 2);
                break;
            case 2:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(7, 12), Random.Range(10, 15), Random.Range(13,17));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn, 1, 3);
                break;
            case 3:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(6,10), Random.Range(9,13), Random.Range(12,16));
                SpawnEnemies(quantityOfEnemiesToSpawn, 2);
                break;
            case 4:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(9,13), Random.Range(12, 15), Random.Range(16,20));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn, 1, 4);
                break;
            case 5:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(12,15), Random.Range(15, 18), Random.Range(19,22));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn, 2, 4);
                break;
            case 6:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(13, 19), Random.Range(16, 22), Random.Range(20, 26));
                SpawnEnemiesRandom(Random.Range(16, 22), 1, 5);
                break;
            case 7:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(16,20), Random.Range(20,24), Random.Range(25,29));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn, 2, 5);
                break;
            case 8:
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

    public override void spawnBoss()
    {
        mySpawn.prefabToSpawn = boss;
        mySpawn.prefabToSpawn.GetComponent<EnemyBody>().xPosition = boss1Position[0];
        mySpawn.prefabToSpawn.GetComponent<EnemyBody>().yPosition = boss1Position[1];
        mySpawn.Create();
    }
}
