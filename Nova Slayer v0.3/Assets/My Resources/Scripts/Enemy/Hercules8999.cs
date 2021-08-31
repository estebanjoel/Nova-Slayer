using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hercules8999 : EnemyBody
{
    public Spawner nullSpawner;
    public GameObject nullAura;
    public Spawner[] additionalFireSpawners;
    public GameObject superBullet;
    public float superBulletSpeed;
    public float superBulletPower;
    public AudioClip superBulletClip;
    public Spawner[] superFireSpawners;
    public GameObject sinusoidalBullet;
    public float sinusoidalYSpeed;
    public Spawner[] sinusoidalFireSpawners;
    // Start is called before the first frame update
    void Start()
    {
        fireRate = Random.Range(2, 2.5f);
        switch(GameManager.instance.currentDifficulty)
        {
            case 0:
                maxHealth = 80;
                fireRate += fireRate/4;
                bulletPower -= bulletPower/4;
                superBulletPower -= superBulletPower/4;
                break;
            case 2:
                maxHealth = 100;
                break;
            case 1:
                maxHealth = 125;
                fireRate -= fireRate/4;
                bulletPower += bulletPower/4;
                bulletSpeed += bulletSpeed/8;
                superBulletPower += superBulletPower/4;
                break;
        }
        health = maxHealth;
        SetEnemyStats();
        nullSpawner.prefabToSpawn = nullAura;
        nullSpawner.prefabToSpawn.GetComponent<EnemyShield>().lifeTime = 1000000f;
        nullSpawner.Create();
        sinusoidalBullet.GetComponent<Bullet>().power = bulletPower;
        sinusoidalBullet.GetComponent<Bullet>().speed = bulletSpeed;
        sinusoidalBullet.GetComponent<Bullet>().ySpeed = sinusoidalYSpeed;
        foreach(Spawner spawner in additionalFireSpawners)
        {
            spawner.prefabToSpawn = bullet;
        }
        foreach(Spawner spawner in sinusoidalFireSpawners)
        {
            spawner.prefabToSpawn = sinusoidalBullet;
        }
        superBullet.GetComponent<Bullet>().power = superBulletPower;
        superBullet.GetComponent<Bullet>().speed = superBulletSpeed;
        superBullet.GetComponent<Bullet>().bulletClip = superBulletClip;
        foreach(Spawner spawner in superFireSpawners)
        {
            spawner.prefabToSpawn = superBullet;
        }
    }

    public override void FireBullet()
    {
        fireSpawner.Create();
        foreach(Spawner spawner in additionalFireSpawners)
        {
            spawner.Create();
        }
        if(GameManager.instance.currentDifficulty > 0) FireSinusoidalBullets();
        foreach(Spawner spawner in superFireSpawners)
        {
            switch(GameManager.instance.currentDifficulty)
            {
                case 0:
                    spawner.Invoke("Create", 1.5f);
                    break;
                case 1:
                    spawner.Invoke("Create", 0.75f);
                    break;
                case 2:
                    spawner.Invoke("Create", 0.5f);
                    break;
            }
            
        }
    }

    public void FireSinusoidalBullets()
    {
        sinusoidalFireSpawners[1].Create();
        sinusoidalFireSpawners[3].Create();
        sinusoidalFireSpawners[0].prefabToSpawn.GetComponent<Bullet>().ySpeed = sinusoidalYSpeed * -1f;
        sinusoidalFireSpawners[2].prefabToSpawn.GetComponent<Bullet>().ySpeed = sinusoidalYSpeed * -1f;
        sinusoidalFireSpawners[0].Create();
        sinusoidalFireSpawners[2].Create();
    }
}
