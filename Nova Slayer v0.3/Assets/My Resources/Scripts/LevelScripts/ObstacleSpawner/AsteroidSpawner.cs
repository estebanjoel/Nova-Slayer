using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : ObstacleSpawner
{
    public float initialXMinPosition, initialXMaxPosition;
    public float[] initialYPosition;
    // Start is called before the first frame update
    void Start()
    {
        AssignTimeToRespawn();
    }

    // Update is called once per frame
    void Update()
    {
        if(remainingTimeToSpawn <= 0)
        {
            if(instancesToSpawn <= 3) currentQuantityToSpawn = quantityOfObstaclesToSpawn[0];
            else if(instancesToSpawn > 3 && instancesToSpawn <= 7) currentQuantityToSpawn = quantityOfObstaclesToSpawn[1];
            else currentQuantityToSpawn = quantityOfObstaclesToSpawn[2];
            instancesToSpawn++;
            SpawnObstacles(currentQuantityToSpawn);
            AssignTimeToRespawn();
        } 
        else
        {
            remainingTimeToSpawn -= Time.deltaTime;
        }
    }

    public override void SpawnObstacle()
    {
        float xPos = Random.Range(initialXMinPosition, initialXMaxPosition);
        float yPos = initialYPosition[Random.Range(0, initialYPosition.Length)];
        obstacleSpawner.prefabToSpawn = selectObstacleToSpawn();
        obstacleSpawner.prefabToSpawn.transform.position = new Vector3(xPos, yPos, 0);
        // obstacleSpawner.prefabToSpawn.GetComponent<Asteroid>().AssignInitialPosition();
        obstacleSpawner.Create();
    }
}
