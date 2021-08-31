using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVl1Settings : LevelSettings
{
    public GameObject starExplosionSpawner;
    public GameObject magneticSpawner;
    public AsteroidSpawner asteroidSpawner;
    public int[] easyQuantityOfAsteroidsToSpawn, mediumQuantityOfAsteroidsToSpawn, hardQuantityOfAsteroidsToSpawn;
    public float easyAsteroidSpawnRate, mediumAsteroidSpawnRate, hardAsteroidSpawnRate;
    public AsteroidSpawner spaceRockSpawner;
    public int[] easyQuantityOfSpaceRockToSpawn, mediumQuantityOfSpaceRockToSpawn, hardQuantityOfSpaceRockToSpawn;
    public float easySpaceRockSpawnRate, mediumSpaceRockSpawnRate, hardSpaceRockSpawnRate;

    public override void SetEasyLevelSettings()
    {
        SetEasyGeneralSettings();
        starExplosionSpawner.SetActive(false);
        magneticSpawner.SetActive(false);
        asteroidSpawner.quantityOfObstaclesToSpawn = easyQuantityOfAsteroidsToSpawn;
        asteroidSpawner.spawnRate = easyAsteroidSpawnRate;
        spaceRockSpawner.quantityOfObstaclesToSpawn = easyQuantityOfSpaceRockToSpawn;
        spaceRockSpawner.spawnRate = easySpaceRockSpawnRate;
    }
    public override void SetMediumLevelSettings()
    {
        SetMediumGeneralSettings();
        starExplosionSpawner.SetActive(false);
        magneticSpawner.SetActive(false);
        asteroidSpawner.quantityOfObstaclesToSpawn = mediumQuantityOfAsteroidsToSpawn;
        asteroidSpawner.spawnRate = mediumAsteroidSpawnRate;
        spaceRockSpawner.quantityOfObstaclesToSpawn = mediumQuantityOfSpaceRockToSpawn;
        spaceRockSpawner.spawnRate = mediumSpaceRockSpawnRate;
    }
    public override void SetHardLevelSettings()
    {
        SetHardGeneralSettings();
        starExplosionSpawner.SetActive(true);
        magneticSpawner.SetActive(true);
        asteroidSpawner.quantityOfObstaclesToSpawn = hardQuantityOfAsteroidsToSpawn;
        asteroidSpawner.spawnRate = hardAsteroidSpawnRate;
        spaceRockSpawner.quantityOfObstaclesToSpawn = hardQuantityOfSpaceRockToSpawn;
        spaceRockSpawner.spawnRate = hardSpaceRockSpawnRate;
    }
}
