using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl3Settings : LevelSettings
{
    public GameObject magneticSpawner;
    public GameObject levelDust;
    public GameObject asteroidSpawner;
    public GravityFieldSpawner starExplosionSpawner;
    public int[] easyQuantityOfstarExplosionsToSpawn, mediumQuantityOfstarExplosionsToSpawn, hardQuantityOfstarExplosionsToSpawn;
    public float easystarExplosionsSpawnRate, mediumstarExplosionsSpawnRate, hardstarExplosionsSpawnRate;
    public SolarFlameSpawner solarFlameSpawner;
    public int[] easyQuantityOfSolarFlamesToSpawn, mediumQuantityOfSolarFlamesToSpawn, hardQuantityOfSolarFlamesToSpawn;
    public float easySolarFlamesSpawnRate, mediumSolarFlamesSpawnRate, hardSolarFlamesSpawnRate;
   public override void SetEasyLevelSettings()
    {
        SetEasyGeneralSettings();
        magneticSpawner.SetActive(false);
        levelDust.SetActive(false);
        asteroidSpawner.SetActive(false);
        starExplosionSpawner.quantityOfObstaclesToSpawn = easyQuantityOfstarExplosionsToSpawn;
        starExplosionSpawner.spawnRate = easystarExplosionsSpawnRate;
        solarFlameSpawner.quantityOfObstaclesToSpawn = easyQuantityOfSolarFlamesToSpawn;
        solarFlameSpawner.spawnRate = easySolarFlamesSpawnRate;
    }
    public override void SetMediumLevelSettings()
    {
        SetMediumGeneralSettings();
        magneticSpawner.SetActive(false);
        levelDust.SetActive(true);
        asteroidSpawner.SetActive(false);
        starExplosionSpawner.quantityOfObstaclesToSpawn = mediumQuantityOfstarExplosionsToSpawn;
        starExplosionSpawner.spawnRate = mediumstarExplosionsSpawnRate;
        solarFlameSpawner.quantityOfObstaclesToSpawn = mediumQuantityOfSolarFlamesToSpawn;
        solarFlameSpawner.spawnRate = mediumSolarFlamesSpawnRate;
    }
    public override void SetHardLevelSettings()
    {
        SetHardGeneralSettings();
        magneticSpawner.SetActive(true);
        levelDust.SetActive(true);
        asteroidSpawner.SetActive(true);
        starExplosionSpawner.quantityOfObstaclesToSpawn = hardQuantityOfstarExplosionsToSpawn;
        starExplosionSpawner.spawnRate = hardstarExplosionsSpawnRate;
        solarFlameSpawner.quantityOfObstaclesToSpawn = hardQuantityOfSolarFlamesToSpawn;
        solarFlameSpawner.spawnRate = hardSolarFlamesSpawnRate;
    }
}
