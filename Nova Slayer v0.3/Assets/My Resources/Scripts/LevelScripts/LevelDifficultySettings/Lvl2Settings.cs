using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl2Settings : LevelSettings
{
    public GameObject starExplosionSpawner;
    public GameObject magneticSpawner;
    public GameObject levelDust;
    public AsteroidSpawner asteroidSpawner;
    public int[] easyQuantityOfAsteroidsToSpawn, mediumQuantityOfAsteroidsToSpawn, hardQuantityOfAsteroidsToSpawn;
    public float easyAsteroidSpawnRate, mediumAsteroidSpawnRate, hardAsteroidSpawnRate;
    public AsteroidSpawner spaceRockSpawner;
    public int[] easyQuantityOfSpaceRockToSpawn, mediumQuantityOfSpaceRockToSpawn, hardQuantityOfSpaceRockToSpawn;
    public float easySpaceRockSpawnRate, mediumSpaceRockSpawnRate, hardSpaceRockSpawnRate;
    public GravityFieldSpawner gravityFieldSpawner;
    public int[] easyQuantityOfGravityFieldToSpawn, mediumQuantityOfGravityFieldToSpawn, hardQuantityOfGravityFieldToSpawn;
    public float easyGravityFieldSpawnRate, mediumGravityFieldSpawnRate, hardGravityFieldSpawnRate;
    public override void SetEasyLevelSettings()
    {
        SetEasyGeneralSettings();
        starExplosionSpawner.SetActive(false);
        magneticSpawner.SetActive(false);
        levelDust.SetActive(false);
        asteroidSpawner.quantityOfObstaclesToSpawn = easyQuantityOfAsteroidsToSpawn;
        asteroidSpawner.spawnRate = easyAsteroidSpawnRate;
        spaceRockSpawner.quantityOfObstaclesToSpawn = easyQuantityOfSpaceRockToSpawn;
        spaceRockSpawner.spawnRate = easySpaceRockSpawnRate;
        gravityFieldSpawner.quantityOfObstaclesToSpawn = easyQuantityOfGravityFieldToSpawn;
        gravityFieldSpawner.spawnRate = easyGravityFieldSpawnRate;
    }
    public override void SetMediumLevelSettings()
    {
        SetMediumGeneralSettings();
        starExplosionSpawner.SetActive(false);
        magneticSpawner.SetActive(false);
        levelDust.SetActive(false);
        asteroidSpawner.quantityOfObstaclesToSpawn = mediumQuantityOfAsteroidsToSpawn;
        asteroidSpawner.spawnRate = mediumAsteroidSpawnRate;
        spaceRockSpawner.quantityOfObstaclesToSpawn = mediumQuantityOfSpaceRockToSpawn;
        spaceRockSpawner.spawnRate = mediumSpaceRockSpawnRate;
        gravityFieldSpawner.quantityOfObstaclesToSpawn = mediumQuantityOfGravityFieldToSpawn;
        gravityFieldSpawner.spawnRate = mediumGravityFieldSpawnRate;
    }
    public override void SetHardLevelSettings()
    {
        SetHardGeneralSettings();
        starExplosionSpawner.SetActive(true);
        magneticSpawner.SetActive(true);
        levelDust.SetActive(true);
        asteroidSpawner.quantityOfObstaclesToSpawn = hardQuantityOfAsteroidsToSpawn;
        asteroidSpawner.spawnRate = hardAsteroidSpawnRate;
        spaceRockSpawner.quantityOfObstaclesToSpawn = hardQuantityOfSpaceRockToSpawn;
        spaceRockSpawner.spawnRate = hardSpaceRockSpawnRate;
        gravityFieldSpawner.quantityOfObstaclesToSpawn = hardQuantityOfGravityFieldToSpawn;
        gravityFieldSpawner.spawnRate = hardGravityFieldSpawnRate;
    }
}
