using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelSettings : MonoBehaviour
{
    public ItemSpawner itemSpawner, ammoSpawner;
    public float[] itemMinRate, itemMaxRate, ammoMinRate, ammoMaxRate;
    public GameObject[] easyItemsToSpawn, mediumHardItemsToSpawn, easyAmmoToSpawn, mediumHardAmmoToSpawn;
    public LevelEstimatedTimes levelEstimatedTimes;
    public float[] easyEstimatedTime, mediumEstimatedTime, hardEstimatedTime;

    public abstract void SetEasyLevelSettings();
    public void SetEasyGeneralSettings()
    {
        itemSpawner.itemsToSpawn = easyItemsToSpawn;
        ammoSpawner.itemsToSpawn = easyAmmoToSpawn;
        itemSpawner.minSpawnRate = itemMinRate[0];
        itemSpawner.maxSpawnRate = itemMaxRate[0];
        ammoSpawner.minSpawnRate = ammoMinRate[0];
        ammoSpawner.maxSpawnRate = ammoMaxRate[0];
        itemSpawner.SetRemainingTimeToSpawn();
        ammoSpawner.SetRemainingTimeToSpawn();
        levelEstimatedTimes.estimatedTimes = easyEstimatedTime;
    }
    public abstract void SetMediumLevelSettings();
    public void SetMediumGeneralSettings()
    {
        itemSpawner.itemsToSpawn = mediumHardItemsToSpawn;
        ammoSpawner.itemsToSpawn = mediumHardAmmoToSpawn;
        itemSpawner.minSpawnRate = itemMinRate[1];
        itemSpawner.maxSpawnRate = itemMaxRate[1];
        ammoSpawner.minSpawnRate = ammoMinRate[1];
        ammoSpawner.maxSpawnRate = ammoMaxRate[1];
        itemSpawner.SetRemainingTimeToSpawn();
        ammoSpawner.SetRemainingTimeToSpawn();
        levelEstimatedTimes.estimatedTimes = mediumEstimatedTime;
    }
    public abstract void SetHardLevelSettings();
    public void SetHardGeneralSettings()
    {
        itemSpawner.itemsToSpawn = mediumHardItemsToSpawn;
        ammoSpawner.itemsToSpawn = mediumHardAmmoToSpawn;
        itemSpawner.minSpawnRate = itemMinRate[2];
        itemSpawner.maxSpawnRate = itemMaxRate[2];
        ammoSpawner.minSpawnRate = ammoMinRate[2];
        ammoSpawner.maxSpawnRate = ammoMaxRate[2];
        itemSpawner.SetRemainingTimeToSpawn();
        ammoSpawner.SetRemainingTimeToSpawn();
        levelEstimatedTimes.estimatedTimes = hardEstimatedTime;
    }

}
