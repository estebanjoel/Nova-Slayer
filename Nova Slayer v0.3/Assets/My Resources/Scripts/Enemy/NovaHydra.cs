using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaHydra : EnemyBody
{
    public Spawner additionalFireSpawner;
    public GameObject sinusoidalBullet;
    public float sinusoidalYSpeed;
    public Spawner[] sinusoidalFireSpawners;
    // Start is called before the first frame update
    void Start()
    {
        fireRate = Random.Range(3, 3.5f);
        SetEnemyStats();
        bullet.GetComponent<Bullet>().power = bulletPower;
        bullet.GetComponent<Bullet>().speed = bulletSpeed;
        additionalFireSpawner.prefabToSpawn = bullet;
        sinusoidalBullet.GetComponent<Bullet>().ySpeed = sinusoidalYSpeed;
        sinusoidalBullet.GetComponent<Bullet>().speed = bulletSpeed;
        foreach(Spawner spawner in sinusoidalFireSpawners)
        {
            spawner.prefabToSpawn = sinusoidalBullet;
            spawner.prefabToSpawn.layer = 8;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void FireBullet()
    {
        fireSpawner.Create();
        additionalFireSpawner.Create();
        Invoke("FireSinusoidalBullets", 0.5f);
    }

    public void FireSinusoidalBullets()
    {
        sinusoidalFireSpawners[0].Create();
        sinusoidalFireSpawners[1].prefabToSpawn.GetComponent<Bullet>().ySpeed *= -1f;
        sinusoidalFireSpawners[1].Create();
    }

}
