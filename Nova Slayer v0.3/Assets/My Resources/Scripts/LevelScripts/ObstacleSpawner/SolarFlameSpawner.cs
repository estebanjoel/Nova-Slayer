using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarFlameSpawner : ObstacleSpawner
{
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
        float yPos = initialYPosition[Random.Range(0, initialYPosition.Length)];
        obstacleSpawner.prefabToSpawn = selectObstacleToSpawn();
        if(!AudioManager.instance.sfxSources[6].isPlaying)
        {
            AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.sfxSources[6], obstacleSpawner.prefabToSpawn.GetComponent<Bullet>().bulletClip);
            AudioManager.instance.PlaySource(AudioManager.instance.sfxSources[6]);
        }
        obstacleSpawner.prefabToSpawn.transform.position = new Vector3(transform.position.x, yPos, 0);
        obstacleSpawner.Create();
    }
}
