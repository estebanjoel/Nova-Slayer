using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl1Spawner : EnemySpawner
{
    [SerializeField] float[] boss1Position = new float[3];
    [SerializeField] float[] boss2Position = new float[3];

    void Start()
    {
        int TotalPositions = xPositions.Length * yPositions.Length;
        while(TotalPositions>0)
        {
            occupiedPositions.Add(false);
            TotalPositions--;
        }
        waves = 7;    
    }
    public override void spawnWave()
    {
        actualWave++;
        switch(actualWave)
        {
            case 1:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(2,4), Random.Range(4,7), Random.Range(8,10));
                SpawnEnemies(quantityOfEnemiesToSpawn,0);
                break;
            
            case 2:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(4,7), Random.Range(5,9), Random.Range(9,13));
                SpawnEnemies(quantityOfEnemiesToSpawn,0);
                break;
            
            
            case 3:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(5,7), Random.Range(6,9), Random.Range(10,13));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn,0,2);
                break;
            
            case 4:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(5,8), Random.Range(6,10), Random.Range(10,14));
                SpawnEnemies(quantityOfEnemiesToSpawn,1);
                break;
            
            case 5:
            quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(7,11), Random.Range(8,13), Random.Range(12,16));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn,0,3);
                break;

            case 6:
                quantityOfEnemiesToSpawn = SetQuantityOfEnemiesToSpawn(Random.Range(8,12), Random.Range(10,15), Random.Range(13, 17));
                SpawnEnemiesRandom(quantityOfEnemiesToSpawn,0,3);
                break;

            case 7:
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
        mySpawn.prefabToSpawn.GetComponent<EnemyBody>().xPosition = boss2Position[0];
        mySpawn.prefabToSpawn.GetComponent<EnemyBody>().yPosition = boss2Position[1];
        mySpawn.Create();
    }
}
