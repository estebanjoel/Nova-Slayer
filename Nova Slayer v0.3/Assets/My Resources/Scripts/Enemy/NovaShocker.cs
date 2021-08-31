using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaShocker : EnemyBody
{
    void Start()
    {
        fireRate=Random.Range(5f,7f);
        SetEnemyStats();
        fireSpawner.prefabToSpawn.GetComponent<Bullet>().speed=bulletSpeed;
        fireSpawner.prefabToSpawn.GetComponent<Bullet>().power=bulletPower;
    }

    public override void FireBullet()
    {
        fireSpawner.Create();
    }
}
