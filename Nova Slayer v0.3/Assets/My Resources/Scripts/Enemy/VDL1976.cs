using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VDL1976 : EnemyBody
{
    public float minYPos, maxYPos, currentYTarget, ySpeed;
    public Spawner aditionalFireSpawner;
    public Spawner[] sinuosidalBulletSpawners;
    public Spawner bomberSpawner;
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
    public AudioClip sinusoidalClip;
    public AudioClip bomberClip;
    // Start is called before the first frame update
    void Start()
    {
        fireRate = Random.Range(1, 1.25f);
        switch(GameManager.instance.currentDifficulty)
        {
            case 0:
                fireRate += fireRate/2;
                maxHealth = 100;
                bulletPower -= bulletPower/2;
                bomberRate += bomberRate/4;
                minBombers = 0;
                maxBombers = 1;
                ySpeed -= ySpeed/4;
                break;
            case 2:
                maxHealth = 150;
                minBombers = 1;
                maxBombers = 2;
                break;
            case 1:
                maxHealth = 175;
                fireRate -= fireRate/4;
                bulletPower += bulletPower/2;
                bulletSpeed -= bulletSpeed/4;
                bomberRate -= bomberRate/2;
                minBombers = 2;
                maxBombers = 3;
                ySpeed += ySpeed/4;
                break;
        }
        health = maxHealth;
        currentYTarget = maxYPos;
        SetSpawnRate();
        remainingTimeToFire = fireRate;
        for(int i = 0; i < bomberYPos.Length; i++)
        {
            occupiedYpos.Add(false);
        }
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
        fireSpawner.prefabToSpawn = bullet;
        bullet.GetComponent<Bullet>().power = bulletPower;
        bullet.GetComponent<Bullet>().bulletClip = bulletClip;
        aditionalFireSpawner.prefabToSpawn = bullet;
        fireSpawner.Create();
        aditionalFireSpawner.Create();
        if(GameManager.instance.currentDifficulty>0) FireSinusoidalBullets();
    }

    public void FireSinusoidalBullets()
    {
        sinusoidalBullet.GetComponent<Bullet>().power = bulletPower;
        sinusoidalBullet.GetComponent<Bullet>().speed = bulletSpeed;
        sinusoidalBullet.GetComponent<Bullet>().ySpeed = sinusoidalYSpeed;
        sinusoidalBullet.GetComponent<Bullet>().bulletClip = sinusoidalClip;
        foreach(Spawner spawner in sinuosidalBulletSpawners)
        {
            spawner.prefabToSpawn = sinusoidalBullet;
        }
        sinuosidalBulletSpawners[1].Create();
        sinuosidalBulletSpawners[3].Create();
        sinuosidalBulletSpawners[0].prefabToSpawn.GetComponent<Bullet>().ySpeed = sinusoidalYSpeed * -1f;
        sinuosidalBulletSpawners[2].prefabToSpawn.GetComponent<Bullet>().ySpeed = sinusoidalYSpeed * -1f;
        sinuosidalBulletSpawners[0].Create();
        sinuosidalBulletSpawners[2].Create();
    }

    
    public void SetSpawnRate()
    {
        remainingTimeToSpawnBombers = bomberRate;
    }

    public void SpawnBomberWave()
    {
        SetSpawnRate();
        novaBomber.GetComponent<NovaBomber>().xPosition = bomberXPos;
        bomberSpawner.prefabToSpawn = novaBomber;
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
