using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaCruiser : EnemyBody
{
    EffectsManager myEffects;
    void Start()
    {
        fireRate=Random.Range(2f,2.5f);
        SetEnemyStats();
        fireSpawner.prefabToSpawn.GetComponent<Bullet>().speed=bulletSpeed;
        fireSpawner.prefabToSpawn.GetComponent<Bullet>().power=bulletPower;
        //myEffects.fireEffect.GetComponentsInChildren<ParticleSystem>()[0].main.startSize = 0.5f;
        //myEffects.fireEffect.GetComponentsInChildren<ParticleSystem>()[0].main.startLifetime = new ParticleSystem.MinMaxCurve(0.3f,1f);
    }

    public override void FireBullet()
    {
        fireSpawner.Create();
    }
}
