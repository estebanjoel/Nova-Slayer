using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleSpawner : MonoBehaviour
{
    public float spawnRate;
    public float remainingTimeToSpawn;
    public GameObject[] obstaclesToSpawn;
    public Spawner obstacleSpawner;
    public int instancesToSpawn;
    public int currentQuantityToSpawn;
    public int[] quantityOfObstaclesToSpawn;
    
    void Start()
    {
        AssignTimeToRespawn();
    }
    void Update()
    {
    }

    public GameObject selectObstacleToSpawn()
    {
        return obstaclesToSpawn[Random.Range(0, obstaclesToSpawn.Length)];
    }

    public abstract void SpawnObstacle();

    public void AssignTimeToRespawn()
    {
        remainingTimeToSpawn = spawnRate;
    }
    
    public void SpawnObstacles(int quantity)
    {
        while(quantity>0)
        {
            quantity--;
            SpawnObstacle();
        }
    }
}
