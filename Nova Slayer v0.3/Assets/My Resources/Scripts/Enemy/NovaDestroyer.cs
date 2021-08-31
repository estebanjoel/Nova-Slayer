using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaDestroyer : EnemyBody
{
    void Start()
    {
        fireRate=Random.Range(2f, 3.5f);
        SetEnemyStats();
        fireSpawner.prefabToSpawn.GetComponent<Bullet>().speed = bulletSpeed;
        fireSpawner.prefabToSpawn.GetComponent<Bullet>().power = bulletPower;
    }

    public override void FireBullet()
    {
        fireSpawner.Create();
        fireSpawner.Invoke("Create",0.2f);
        fireSpawner.Invoke("Create",0.4f);
    }
}
