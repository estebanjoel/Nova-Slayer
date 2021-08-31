using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onslaught : EnemyBody
{
    public float minYPos, maxYPos, currentYTarget, ySpeed;
    public Spawner[] aditionalFireSpawners;
    public Spawner[] sinuosidalBulletSpawners;
    public Spawner bomberSpawner;
    public GameObject laserObject;
    public float laserPower;
    bool canShootLaser;
    public float laserRate;
    float remainingTimeToFireLaser;
    public GameObject sinusoidalBullet;
    public float sinusoidalYSpeed;
    public GameObject novaBomber;
    public float bomberRate;
    float remainingTimeToSpawnBombers;
    public int minBombers, maxBombers;
    public float[] bomberYPos;
    public List<bool> occupiedYpos = new List<bool>();
    public float bomberXPos;
    public AudioClip bulletClip;
    public AudioClip laserClip;
    public AudioClip sinusoidalClip;
    public AudioClip bomberClip;
    // Start is called before the first frame update
    void Start()
    {
        switch(GameManager.instance.currentDifficulty)
        {
            case 0:
                maxHealth = 300;
                bulletPower -= bulletPower/4;
                fireRate += fireRate/4;
                laserPower = NovaSlayer.instance.body.maxHealth / 8f;
                minBombers = 1;
                maxBombers = 2;
                bomberRate += bomberRate/2;
                break;
            case 2:
                maxHealth = 500;
                laserPower = NovaSlayer.instance.body.maxHealth / 4f;
                minBombers = 1;
                maxBombers = 3;
                break;
            case 1:
                maxHealth = 650;
                bulletPower += bulletPower/4;
                fireRate -= fireRate/4;
                laserPower = NovaSlayer.instance.body.maxHealth / 2f;
                minBombers = 2;
                maxBombers = 4;
                bomberRate -= bomberRate/4;
                laserRate -= laserRate/4;
                break;
        }
        health = maxHealth;
        currentYTarget = maxYPos;
        SetLaserRate();
        SetSpawnRate();
        bullet.GetComponent<Bullet>().power = bulletPower;
        bullet.GetComponent<Bullet>().bulletClip = bulletClip;
        laserObject.GetComponent<Bullet>().power = laserPower;
        laserObject.GetComponent<Bullet>().bulletClip = laserClip;
        foreach(Spawner spawner in aditionalFireSpawners)
        {
            spawner.prefabToSpawn = bullet;
        }
        sinusoidalBullet.GetComponent<Bullet>().power = bulletPower;
        sinusoidalBullet.GetComponent<Bullet>().speed = bulletSpeed;
        sinusoidalBullet.GetComponent<Bullet>().ySpeed = sinusoidalYSpeed;
        sinusoidalBullet.GetComponent<Bullet>().bulletClip = sinusoidalClip;
        foreach(Spawner spawner in sinuosidalBulletSpawners)
        {
            spawner.prefabToSpawn = sinusoidalBullet;
        }
        for(int i = 0; i < bomberYPos.Length; i++)
        {
            occupiedYpos.Add(false);
        }
        novaBomber.GetComponent<NovaBomber>().xPosition = bomberXPos;
        bomberSpawner.prefabToSpawn = novaBomber;
        
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
                    if(transform.position.y >= currentYTarget)
                    {
                        transform.position -= transform.right * ySpeed * Time.deltaTime;
                    }
                    else if(transform.position.y <= currentYTarget)
                    {
                        transform.position += transform.right * ySpeed * Time.deltaTime;
                    }

                    if(transform.position.y <= minYPos) currentYTarget = maxYPos;
                    if(transform.position.y >= maxYPos) currentYTarget = minYPos;
                }
                
                if(GameManager.instance.currentDifficulty > 0)
                {
                    if(remainingTimeToFireLaser <= 0) canShootLaser = true;
                    else remainingTimeToFireLaser -= Time.deltaTime;
                }

                if(remainingTimeToSpawnBombers <= 0) SpawnBomberWave();
                else remainingTimeToSpawnBombers -= Time.deltaTime;
            }
            else
            {
                if(GameObject.FindObjectsOfType<NovaBomber>().Length > 0)
                {
                    for(int i = 0; i < GameObject.FindObjectsOfType<NovaBomber>().Length; i++)
                    {
                        GameObject.FindObjectsOfType<NovaBomber>()[i].health = 0;
                    }
                }
            }
        }
    }

    public override void FireBullet()
    {
        if(canShootLaser) FireLaser();
        else fireSpawner.prefabToSpawn = bullet;

        fireSpawner.Create();

        foreach(Spawner spawner in aditionalFireSpawners)
        {
            spawner.Create();
        }
        Invoke("FireSinusoidalBullets", 0.5f);
    }

    public void FireSinusoidalBullets()
    {
        sinuosidalBulletSpawners[0].Create();
        sinuosidalBulletSpawners[1].prefabToSpawn.GetComponent<Bullet>().ySpeed *= -1f;
        sinuosidalBulletSpawners[1].Create();
    }

    public void FireLaser()
    {
        fireSpawner.prefabToSpawn = laserObject;
        SetLaserRate();
        canShootLaser = false;
    }

    public void SetLaserRate()
    {
        remainingTimeToFireLaser = laserRate;
    }

    public void SetSpawnRate()
    {
        remainingTimeToSpawnBombers = bomberRate;
    }

    public void SpawnBomberWave()
    {
        SetSpawnRate();
        if(!AudioManager.instance.sfxSources[6].isPlaying)
        {
            AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.sfxSources[6], bomberClip);
            AudioManager.instance.PlaySource(AudioManager.instance.sfxSources[6]);
        }
        int bombers = Random.Range(minBombers, maxBombers);
        while(bombers > 0)
        {
            int yPos = Random.Range(0, bomberYPos.Length);
            if(!CheckOccupiedPosition(yPos))
            {
                novaBomber.GetComponent<NovaBomber>().yPosition = bomberYPos[yPos];
                occupiedYpos[yPos] = true;
                bomberSpawner.prefabToSpawn = novaBomber;
                bomberSpawner.Invoke("Create", 0.5f);
                bombers--;
            }
        }
        for(int i = 0; i < occupiedYpos.Count; i++)
        {
            occupiedYpos[i] = false;
        }
    }

    public bool CheckOccupiedPosition(int i)
    {
        if(occupiedYpos[i]) return true;
        else return false;
    }
}
