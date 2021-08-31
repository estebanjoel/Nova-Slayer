using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl4Settings : LevelSettings
{
    public GameObject[] easyEnemiesToSpawn, mediumEnemiesToSpawn, hardEnemiesToSpawn;
    public EnemiesForSpacecraftToSpawn enemiesForSpacecraftToSpawn;
    public GameObject gravityFieldSpawner;
    public AsteroidSpawner asteroidSpawner;
    public int[] easyQuantityOfAsteroidsToSpawn, mediumQuantityOfAsteroidsToSpawn, hardQuantityOfAsteroidsToSpawn;
    public float easyAsteroidSpawnRate, mediumAsteroidSpawnRate, hardAsteroidSpawnRate;
    public AsteroidSpawner spaceRockSpawner;
    public int[] easyQuantityOfSpaceRockToSpawn, mediumQuantityOfSpaceRockToSpawn, hardQuantityOfSpaceRockToSpawn;
    public float easySpaceRockSpawnRate, mediumSpaceRockSpawnRate, hardSpaceRockSpawnRate;
    public GravityFieldSpawner starExplosionSpawner;
    public int[] easyQuantityOfstarExplosionsToSpawn, mediumQuantityOfstarExplosionsToSpawn, hardQuantityOfstarExplosionsToSpawn;
    public float easystarExplosionsSpawnRate, mediumstarExplosionsSpawnRate, hardstarExplosionsSpawnRate;
    public SolarFlameSpawner solarFlameSpawner;
    public int[] easyQuantityOfSolarFlamesToSpawn, mediumQuantityOfSolarFlamesToSpawn, hardQuantityOfSolarFlamesToSpawn;
    public float easySolarFlamesSpawnRate, mediumSolarFlamesSpawnRate, hardSolarFlamesSpawnRate;
    public override void SetEasyLevelSettings()
    {
        SetEasyGeneralSettings();
        enemiesForSpacecraftToSpawn.enemies = easyEnemiesToSpawn;
        gravityFieldSpawner.SetActive(false);
        asteroidSpawner.quantityOfObstaclesToSpawn = easyQuantityOfAsteroidsToSpawn;
        asteroidSpawner.spawnRate = easyAsteroidSpawnRate;
        spaceRockSpawner.quantityOfObstaclesToSpawn = easyQuantityOfSpaceRockToSpawn;
        spaceRockSpawner.spawnRate = easySpaceRockSpawnRate;
        solarFlameSpawner.quantityOfObstaclesToSpawn = easyQuantityOfSolarFlamesToSpawn;
        solarFlameSpawner.spawnRate = easySolarFlamesSpawnRate;
        starExplosionSpawner.quantityOfObstaclesToSpawn = easyQuantityOfstarExplosionsToSpawn;
        starExplosionSpawner.spawnRate = easystarExplosionsSpawnRate;
    }
    public override void SetMediumLevelSettings()
    {
        SetMediumGeneralSettings();
        enemiesForSpacecraftToSpawn.enemies = mediumEnemiesToSpawn;
        gravityFieldSpawner.SetActive(false);
        asteroidSpawner.quantityOfObstaclesToSpawn = mediumQuantityOfAsteroidsToSpawn;
        asteroidSpawner.spawnRate = mediumAsteroidSpawnRate;
        spaceRockSpawner.quantityOfObstaclesToSpawn = mediumQuantityOfSpaceRockToSpawn;
        spaceRockSpawner.spawnRate = mediumSpaceRockSpawnRate;
        solarFlameSpawner.quantityOfObstaclesToSpawn = mediumQuantityOfSolarFlamesToSpawn;
        solarFlameSpawner.spawnRate = mediumSolarFlamesSpawnRate;
        starExplosionSpawner.quantityOfObstaclesToSpawn = mediumQuantityOfstarExplosionsToSpawn;
        starExplosionSpawner.spawnRate = mediumstarExplosionsSpawnRate;
    }
    public override void SetHardLevelSettings()
    {
        SetHardGeneralSettings();
        enemiesForSpacecraftToSpawn.enemies = hardEnemiesToSpawn;
        gravityFieldSpawner.SetActive(true);
        asteroidSpawner.quantityOfObstaclesToSpawn = hardQuantityOfAsteroidsToSpawn;
        asteroidSpawner.spawnRate = hardAsteroidSpawnRate;
        spaceRockSpawner.quantityOfObstaclesToSpawn = hardQuantityOfSpaceRockToSpawn;
        spaceRockSpawner.spawnRate = hardSpaceRockSpawnRate;
        solarFlameSpawner.quantityOfObstaclesToSpawn = hardQuantityOfSolarFlamesToSpawn;
        solarFlameSpawner.spawnRate = hardSolarFlamesSpawnRate;
        starExplosionSpawner.quantityOfObstaclesToSpawn = hardQuantityOfstarExplosionsToSpawn;
        starExplosionSpawner.spawnRate = hardstarExplosionsSpawnRate;
    }
}
