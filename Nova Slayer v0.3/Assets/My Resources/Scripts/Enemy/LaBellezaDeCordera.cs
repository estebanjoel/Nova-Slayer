using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaBellezaDeCordera : EnemyBody
{
    public Spawner[] additionalFireSpawners;
    public GameObject novaBomber;
    public Spawner[] novaBomberSpawners;
    public float bombSpawnRate;
    float remainingTimeToSpawnBombers;
    public GameObject novaSpacecraft;
    public Spawner[] novaSpacecraftSpawners;
    public float spacecraftSpawnRate;
    float remainingTimeToSpawnSpacecrafts;
    public GameObject relaxAndEnjoyLaser;
    public Spawner laserSpawner;
    public float laserFireRate;
    float remainingTimeToFireLaser;
    // Start is called before the first frame update
    void Start()
    {
        fireRate = Random.Range(2.5f, 3f);
        switch(GameManager.instance.currentDifficulty)
        {
            case 0:
                relaxAndEnjoyLaser.GetComponent<Bullet>().power = (NovaSlayer.instance.body.maxHealth / 2);
                maxHealth = 300;
                bulletPower -= bulletPower/2;
                bombSpawnRate += bombSpawnRate/2;
                spacecraftSpawnRate += spacecraftSpawnRate/4;
                laserFireRate += laserFireRate/2;
                break;
            case 2:
                relaxAndEnjoyLaser.GetComponent<Bullet>().power = (NovaSlayer.instance.body.maxHealth / 2 + NovaSlayer.instance.body.maxHealth / 4);
                maxHealth = 450;
                break;
            case 1:
                relaxAndEnjoyLaser.GetComponent<Bullet>().power = (NovaSlayer.instance.body.maxHealth-2);
                maxHealth = 600;
                bulletPower += bulletPower/4;
                bombSpawnRate -= bombSpawnRate/4;
                spacecraftSpawnRate -= spacecraftSpawnRate/4;
                laserFireRate -= laserFireRate/2;
                break;
        }
        health = maxHealth;
        bullet.GetComponent<Bullet>().power = bulletPower;
        bullet.GetComponent<Bullet>().speed = bulletSpeed;
        SetEnemyStats();
        foreach(Spawner spawner in additionalFireSpawners)
        {
            spawner.prefabToSpawn = bullet;
        }
        foreach(Spawner spawner in novaBomberSpawners)
        {
            spawner.prefabToSpawn = novaBomber;
        }
        foreach(Spawner spawner in novaSpacecraftSpawners)
        {
            spawner.prefabToSpawn = novaSpacecraft;
        }
        laserSpawner.prefabToSpawn = relaxAndEnjoyLaser;
        SetBomberSpawnRate();
        SetLaserFireRate();
        SetSpacecraftSpawnRate();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.victoryCondition && !GameManager.instance.failCondition)
        {
            if(health > 0)
            {
                if(GetComponent<EnemyBrain>().CheckTargetPosition())
                {
                    if(remainingTimeToFireLaser <= 0) FireLaser();
                    else remainingTimeToFireLaser -= Time.deltaTime;

                    if(GameManager.instance.currentDifficulty > 0)
                    {
                        if(remainingTimeToSpawnBombers <= 0) SpawnBombers();
                        else remainingTimeToSpawnBombers -= Time.deltaTime;

                        if(remainingTimeToSpawnSpacecrafts <= 0) SpawnSpacecrafts();
                        else remainingTimeToSpawnSpacecrafts -= Time.deltaTime;
                    }
                }
            }
            else
            {
                if(GameObject.FindObjectsOfType<EnemyBody>().Length > 0)
                {
                    for(int i = 0; i < GameObject.FindObjectsOfType<EnemyBody>().Length; i++)
                    {
                        GameObject.FindObjectsOfType<EnemyBody>()[i].health = 0;
                    }
                }
            }
        }
    }

    public override void FireBullet()
    {
        fireSpawner.Create();
        foreach(Spawner spawner in additionalFireSpawners)
        {
            spawner.Create();
        }
    }
    public void FireLaser()
    {
        laserSpawner.Create();
        SetLaserFireRate();
    }
    public void SpawnBombers()
    {
        novaBomberSpawners[0].prefabToSpawn.GetComponent<EnemyBody>().yPosition = novaBomberSpawners[0].transform.position.y;
        novaBomberSpawners[0].Create();
        novaBomberSpawners[1].prefabToSpawn.GetComponent<EnemyBody>().yPosition = novaBomberSpawners[1].transform.position.y;
        novaBomberSpawners[1].Create();
        SetBomberSpawnRate();
    }
    public void SpawnSpacecrafts()
    {
        novaSpacecraftSpawners[0].prefabToSpawn.GetComponent<EnemyBody>().yPosition = novaSpacecraftSpawners[0].transform.position.y;
        novaSpacecraftSpawners[0].Create();
        novaSpacecraftSpawners[1].prefabToSpawn.GetComponent<EnemyBody>().yPosition = novaSpacecraftSpawners[1].transform.position.y;
        novaSpacecraftSpawners[1].Create();
        SetSpacecraftSpawnRate();
    }
    public void SetLaserFireRate()
    {
        remainingTimeToFireLaser = laserFireRate;
    }
    public void SetBomberSpawnRate()
    {
        remainingTimeToSpawnBombers = bombSpawnRate;
    }
    public void SetSpacecraftSpawnRate()
    {
        remainingTimeToSpawnSpacecrafts = spacecraftSpawnRate;
    }
}
