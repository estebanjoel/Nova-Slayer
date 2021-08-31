using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticSpawner : MonoBehaviour
{
    public Spawner mySpawner;
    public GameObject novaMagnetic;
    public float spawnRate;
    public float remainingTimeToSpawn;

    void Start()
    {
        remainingTimeToSpawn = spawnRate;    
        mySpawner.prefabToSpawn = novaMagnetic;
    }

    // Update is called once per frame
    void Update()
    {
        if(DifficultyManager.instance.GetCurrentDifficultyMode() == 2)
        {
            if(!GameManager.instance.victoryCondition && !GameManager.instance.failCondition)
            {
                if(remainingTimeToSpawn <= 0)
                {
                    mySpawner.Create();
                    remainingTimeToSpawn = spawnRate;
                }
                else
                {
                    remainingTimeToSpawn -= Time.deltaTime;
                }
            }
        }
    }
}
