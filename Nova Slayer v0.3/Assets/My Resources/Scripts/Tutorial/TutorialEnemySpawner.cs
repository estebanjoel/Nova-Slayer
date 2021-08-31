using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemySpawner : MonoBehaviour
{
    public Spawner spawner;
    public GameObject enemy;
    public float[] yPos;
    public bool canSpawnFirstWave, canSpawnSecondWave, canSpawnThirdWave;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawnFirstWave) SpawnFirstWave();
        if(canSpawnSecondWave) SpawnSecondWave();
        if(canSpawnThirdWave) SpawnThirdWave();
    }

    public void SpawnFirstWave()
    {
        canSpawnFirstWave = false;
        enemy.GetComponent<TutorialNovaCruiser>().xPosition = 0;
        enemy.GetComponent<TutorialNovaCruiser>().yPosition = yPos[2];
        spawner.prefabToSpawn = enemy;
        spawner.Create();
        
    }
    public void SpawnSecondWave()
    {
        canSpawnSecondWave = false;
        enemy.GetComponent<TutorialNovaCruiser>().xPosition = 0;
        enemy.GetComponent<TutorialNovaCruiser>().yPosition = yPos[2];
        spawner.prefabToSpawn = enemy;
        spawner.Create();
        enemy.GetComponent<TutorialNovaCruiser>().xPosition = 0;
        enemy.GetComponent<TutorialNovaCruiser>().yPosition = yPos[1];
        spawner.prefabToSpawn = enemy;
        spawner.Create();
        enemy.GetComponent<TutorialNovaCruiser>().xPosition = 0;
        enemy.GetComponent<TutorialNovaCruiser>().yPosition = yPos[3];
        spawner.prefabToSpawn = enemy;
        spawner.Create();
    }
    public void SpawnThirdWave()
    {
        canSpawnThirdWave = false;
        enemy.GetComponent<TutorialNovaCruiser>().xPosition = 0;
        for(int i = 0; i < yPos.Length; i++)
        {
            enemy.GetComponent<TutorialNovaCruiser>().yPosition = yPos[i];
            spawner.prefabToSpawn = enemy;
            spawner.Create();
        }
    }
}