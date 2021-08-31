using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaShip : EnemyBody
{
    void Start()
    {
        fireRate=Random.Range(2f, 2.5f);
        SetEnemyStats();
        fireSpawner.prefabToSpawn.GetComponent<Bullet>().speed=bulletSpeed;
        fireSpawner.prefabToSpawn.GetComponent<Bullet>().power=bulletPower;
    }

    public override void FireBullet()
    {
        fireSpawner.Create();
    }
}
