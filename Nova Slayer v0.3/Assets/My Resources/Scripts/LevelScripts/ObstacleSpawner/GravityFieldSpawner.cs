using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFieldSpawner : ObstacleSpawner
{
    public float minXPosition, maxXPosition, minYPosition, maxYPosition;
    public AudioClip gravityClip;
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
            if(!AudioManager.instance.sfxSources[6].isPlaying)
            {
                AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.sfxSources[6], gravityClip);
                AudioManager.instance.PlaySource(AudioManager.instance.sfxSources[6]);
            }
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
        float xPos = Random.Range(minXPosition, maxXPosition);
        float yPos = Random.Range(minYPosition, maxYPosition);
        obstacleSpawner.prefabToSpawn = selectObstacleToSpawn();
        obstacleSpawner.prefabToSpawn.transform.position = new Vector3(xPos, yPos, 0);
        obstacleSpawner.Create();
    }
}
