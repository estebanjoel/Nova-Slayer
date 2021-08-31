using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatNovaShip : EnemyBody
{
    public Spawner[] additionalFireSpawners;
    public GameObject superBullet;
    public Spawner[] superFireSpawners;
    public float superFireRate;
    public float remainingTimeToFireSuper;
    public float superBulletPower;
    public GameObject superSinusoidalBullet;
    public Spawner[] superSinusoidalFireSpawners;
    public float superSinusoidalYSpeed;
    public float superSinusoidalFireRate;
    public float remainingTimeToFireSuperSinusoidal;
    public GameObject laser;
    public Spawner[] laserSpawners;
    public float laserRate;
    public float remainingTimeToFireLaser;
    public GameObject novaSpacecraft;
    public Spawner[] spacecraftSpawners;
    public GameObject[] enemies;
    public float xPos;
    public float spawnRate;
    public float remainingTimeToSpawn;
    public Spawner[] randomEnemySpawners;
    public Spawner greatNovaSpawner;
    public GameObject greatNovaBullet;
    public float accumulatedDamageToFireGreatNovaBullet;
    public bool hasFiredGreatBullet;
    public GameObject shield;
    public AudioClip shieldClip;
    public float shieldRate;
    public float remainingTimeToDeactivateShield;
    public float accumulatedDamageToActivateShield;
    public bool activatedShield;
    // Start is called before the first frame update
    void Start()
    {
        fireRate = Random.Range(1.5f, 2.5f);
        switch(GameManager.instance.currentDifficulty)
        {
            case 0:
                maxHealth = 750;
                fireRate += fireRate/2;
                superBulletPower -= superBulletPower/2;
                bulletPower -= bulletPower/4;
                break;
            case 2:
                maxHealth = 900;
                laser.GetComponent<Bullet>().power = NovaSlayer.instance.body.maxHealth / 2;
                break;
            case 1:
                maxHealth = 1500;
                fireRate -= fireRate/2;
                shieldRate -= shieldRate/2;
                spawnRate -= spawnRate/4;
                laserRate -= laserRate/4;
                superBulletPower += superBulletPower/4;
                bulletPower += bulletPower/4;
                laser.GetComponent<Bullet>().power = NovaSlayer.instance.body.maxHealth / 2 + NovaSlayer.instance.body.maxHealth / 4;
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
        superBullet.GetComponent<Bullet>().power = superBulletPower;
        superBullet.GetComponent<Bullet>().speed = bulletSpeed;
        foreach(Spawner spawner in superFireSpawners)
        {
            spawner.prefabToSpawn = superBullet;
        }
        superSinusoidalBullet.GetComponent<Bullet>().power = superBulletPower;
        superSinusoidalBullet.GetComponent<Bullet>().speed = bulletSpeed;
        superSinusoidalBullet.GetComponent<Bullet>().ySpeed = superSinusoidalYSpeed;
        foreach(Spawner spawner in superSinusoidalFireSpawners)
        {
            spawner.prefabToSpawn = superSinusoidalBullet;
        }
        foreach(Spawner spawner in laserSpawners)
        {
            spawner.prefabToSpawn = laser;
        }
        foreach(Spawner spawner in spacecraftSpawners)
        {
            spawner.prefabToSpawn = novaSpacecraft;
        }
        greatNovaBullet.GetComponent<Bullet>().power = NovaSlayer.instance.body.maxHealth;
        greatNovaSpawner.prefabToSpawn = greatNovaBullet;
        setTimeRate(2);
        setTimeRate(3);
        setTimeRate(4);
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.victoryCondition && !GameManager.instance.failCondition)
        {
            if(GetComponent<EnemyBrain>().CheckTargetPosition() && health > 0)
            {
                if(GameManager.instance.currentDifficulty > 0)
                {
                    if(isOnShield)
                    {
                        if(remainingTimeToDeactivateShield < 0) DeactivateShield();
                        else remainingTimeToDeactivateShield -= Time.deltaTime;
                    }

                    if(GameManager.instance.currentDifficulty == 1)
                    {
                        if(accumulatedDamageToActivateShield >= 100) if(!activatedShield)ActivateShield();
                    }
                    if(GameManager.instance.currentDifficulty == 2)
                    {
                        if(accumulatedDamageToActivateShield >= 75) if(!activatedShield)ActivateShield();
                    }
                    else if(activatedShield) activatedShield = false;

                    if(remainingTimeToFireLaser <= 0) FireLasers();
                    else remainingTimeToFireLaser -= Time.deltaTime;
                }
                
                if(GameManager.instance.currentDifficulty == 2)
                {
                    if(accumulatedDamageToFireGreatNovaBullet >= 250) if(!hasFiredGreatBullet) FireGreatNovaBullet();
                    else hasFiredGreatBullet = false;
                }
                
                if(remainingTimeToSpawn <= 0) SpawnRandomEnemies();
                else remainingTimeToSpawn -= Time.deltaTime;

                
                if(GameObject.FindObjectOfType<NovaSpacecraft>() != null)
                {
                    switch(GameManager.instance.currentDifficulty)
                    {
                        case 0:
                            Invoke("SpawnSpacecrafts", 10f);
                            break;
                        case 1:
                            Invoke("SpawnSpacecrafts", 8f);
                            break;
                        case 2:
                            Invoke("SpawnSpacecrafts", 5f);
                            break;
                    }
                }
            }
        }
    }

    public void FireLasers()
    {
        foreach(Spawner spawner in laserSpawners)
        {
            spawner.Create();
        }
        setTimeRate(2);
    }

    public override void FireBullet()
    {
        fireSpawner.Create();
        foreach(Spawner spawner in additionalFireSpawners)
        {
            spawner.Create();
        }
        foreach(Spawner spawner in superFireSpawners)
        {
            spawner.Invoke("Create", 0.5f);
        }
        Invoke("FireSinusoidalBullets", 0.75f);
    }

    public void FireSinusoidalBullets()
    {
         foreach(Spawner spawner in superSinusoidalFireSpawners)
        {
            spawner.prefabToSpawn = superSinusoidalBullet;
        }
        superSinusoidalFireSpawners[1].Create();
        superSinusoidalFireSpawners[0].prefabToSpawn.GetComponent<Bullet>().ySpeed = superSinusoidalYSpeed * -1f;
        superSinusoidalFireSpawners[0].Create();
    }

    public void SpawnSpacecrafts()
    {
        foreach(Spawner spawner in spacecraftSpawners)
        {
            spawner.Create();
        }
    }

    public void SpawnRandomEnemies()
    {
        if(GameObject.FindGameObjectsWithTag("Spaceship") == null)
        {
            for(int i = 0; i < randomEnemySpawners.Length; i++)
            {
                GameObject enemyToSpawn = null;
                switch(GameManager.instance.currentDifficulty)
                {
                    case 0:
                        enemyToSpawn = enemies[Random.Range(0, enemies.Length-2)];
                        break;
                    case 1:
                        enemyToSpawn = enemies[Random.Range(0, enemies.Length-1)];
                        break;
                    case 2:
                        enemyToSpawn = enemies[Random.Range(0, enemies.Length)];
                        break;
                }
                
                enemyToSpawn.GetComponent<EnemyBody>().xPosition = xPos;
                enemyToSpawn.GetComponent<EnemyBody>().yPosition = randomEnemySpawners[i].transform.position.y;
                randomEnemySpawners[i].prefabToSpawn = enemyToSpawn;
                randomEnemySpawners[i].Create();
            }
        }
        
        setTimeRate(3);
    }
    public void setTimeRate(int i)
    {
        switch(i)
        {
            // case 0:
            //     remainingTimeToFireSuper = superFireRate;
            //     break;
            // case 1:
            //     remainingTimeToFireSuperSinusoidal = superSinusoidalFireRate;
            //     break;
            case 2:
                remainingTimeToFireLaser = laserRate;
                break;
            case 3:
                remainingTimeToSpawn = spawnRate;
                break;
            case 4:
                remainingTimeToDeactivateShield = shieldRate;
                break;
        }
    }

    public void ActivateShield()
    {
        if(!AudioManager.instance.sfxSources[4].isPlaying)
        {
            AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.sfxSources[4], shieldClip);
            AudioManager.instance.PlaySource(AudioManager.instance.sfxSources[4]);
        }
        shield.SetActive(true);
        isOnShield = true;
        setTimeRate(4);
        activatedShield = true;
        accumulatedDamageToActivateShield = 0;
    }
    public void DeactivateShield()
    {
        shield.SetActive(false);
        isOnShield = false;
        accumulatedDamageToActivateShield = 0;
    }

    public void FireGreatNovaBullet()
    {
        greatNovaSpawner.Create();
        hasFiredGreatBullet = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == 9)
        {
            if(!GetComponent<EnemyBrain>().isDamaged && !isInvencible && !isOnShield)
            {
                accumulatedDamageToActivateShield = AccumulateDamage(accumulatedDamageToActivateShield, other.gameObject.GetComponent<Bullet>().power);
                accumulatedDamageToFireGreatNovaBullet = AccumulateDamage(accumulatedDamageToFireGreatNovaBullet, other.gameObject.GetComponent<Bullet>().power);
            }    
        }
    }
    public float AccumulateDamage(float target, float power)
    {
        return target + power;
    }
}
