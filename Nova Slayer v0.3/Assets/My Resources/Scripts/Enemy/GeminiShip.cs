using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeminiShip : EnemyBody
{
    public Spawner[] aditionalFireSpawners;
    public Spawner[] rapidFireSpawners;
    public float rapidFireRate;
    private float remainingTimeToRapidFire;
    public Spawner[] miniExplosionSpawners;
    public bool canBeginMiniExplosions;
    public int miniExplosionsCounter;
    public Spawner laserSpawner;
    public GameObject geminiLaser;
    public GameObject geminiShield;
    public float accumulatedDamageToFireLaser;
    public bool activatedShield;
    public float shieldRate;
    private float remainingTimeToDeactivateShield;
    public float accumulatedDamageToActivateShield;
    public AudioClip laserClip;
    public AudioClip shieldClip;
    bool hasShotLaser;
    void Start()
    {
        fireRate=Random.Range(2f,2.5f);
        rapidFireRate = Random.Range(1,1.25f);
        switch(GameManager.instance.currentDifficulty)
        {
            case 0:
                bulletPower -= bulletPower/4;
                fireRate += fireRate/4;
                rapidFireRate += fireRate/4;
                shieldRate -= shieldRate/4;
                maxHealth = 100;
                break;
            case 2:
                maxHealth = 150;
                break;
            case 1:
                bulletPower += bulletPower/4;
                maxHealth = 200;
                fireRate -= fireRate/4;
                rapidFireRate -= fireRate/4;
                shieldRate += shieldRate/4;
                break;
        }
        health = maxHealth;
        canBeginMiniExplosions = true;
        hasShotLaser = false;
        geminiShield.SetActive(false);
        laserSpawner.prefabToSpawn = geminiLaser;
        SetEnemyStats();
        foreach(Spawner spawner in aditionalFireSpawners)
        {
            spawner.prefabToSpawn = bullet;
        }
        foreach(Spawner spawner in rapidFireSpawners)
        {
            spawner.prefabToSpawn = bullet;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<EnemyBrain>().CheckTargetPosition())
        {
            if(isOnShield)
            {
                if(remainingTimeToDeactivateShield < 0) DeactivateShield();
                else remainingTimeToDeactivateShield -= Time.deltaTime;
            }
            if(GameManager.instance.currentDifficulty < 2)
            {
                if(accumulatedDamageToActivateShield >= 25) if(!activatedShield)ActivateShield();
                else if(activatedShield) activatedShield = false;

                if(accumulatedDamageToFireLaser >= 35) if(!hasShotLaser) FireLaser();
                else hasShotLaser = false;
            }
            else
            {
                if(accumulatedDamageToActivateShield >= 20) if(!activatedShield)ActivateShield();
                else if(activatedShield) activatedShield = false;

                if(accumulatedDamageToFireLaser >= 30) if(!hasShotLaser) FireLaser();
                else hasShotLaser = false;
            }
            
            if(GetComponent<EnemyBrain>().CheckFireTarget())
            {
                if(remainingTimeToRapidFire < 0) RapidFire();
                else remainingTimeToRapidFire -= Time.deltaTime;
            }
        }
    }

    public override void FireBullet()
    {
        fireSpawner.Create();
        foreach(Spawner spawner in aditionalFireSpawners)
        {
            spawner.Create();
        }
    }

    public void RapidFire()
    {
        foreach(Spawner spawner in rapidFireSpawners)
        {
            spawner.Create();
        }
        remainingTimeToRapidFire = fireRate;
    }

    public void FireLaser()
    {
        laserSpawner.Create();
        hasShotLaser = true;
        if(!AudioManager.instance.sfxSources[2].isPlaying)
        {
            AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.sfxSources[2], laserClip);
            AudioManager.instance.PlaySource(AudioManager.instance.sfxSources[2]);
        }
        accumulatedDamageToFireLaser = 0;
    }

    public void ActivateShield()
    {
        if(!AudioManager.instance.sfxSources[4].isPlaying)
        {
            AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.sfxSources[4], shieldClip);
            AudioManager.instance.PlaySource(AudioManager.instance.sfxSources[4]);
        }
        geminiShield.SetActive(true);
        isOnShield = true;
        remainingTimeToDeactivateShield = shieldRate;
        activatedShield = true;
        accumulatedDamageToActivateShield = 0;
    }
    public void DeactivateShield()
    {
        geminiShield.SetActive(false);
        isOnShield = false;
        accumulatedDamageToActivateShield = 0;
    }
    public IEnumerator miniExplosionsCo()
    {
        Debug.Log(miniExplosionsCounter);
        canBeginMiniExplosions = false;
        int miniExplosionIndex = 0;
        while(miniExplosionsCounter>0)
        {
            miniExplosionSpawners[miniExplosionIndex].Create();
            yield return new WaitForSeconds(0.25f);
            miniExplosionIndex++;
            if(miniExplosionIndex == miniExplosionSpawners.Length) miniExplosionIndex = 0;
            miniExplosionsCounter--;
        }
        GetComponent<EnemyBrain>().canBossExplode = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == 9)
        {
            if(!GetComponent<EnemyBrain>().isDamaged && !isInvencible && !isOnShield)
            {
                accumulatedDamageToActivateShield = AccumulateDamage(accumulatedDamageToActivateShield, other.gameObject.GetComponent<Bullet>().power);
                accumulatedDamageToFireLaser = AccumulateDamage(accumulatedDamageToFireLaser, other.gameObject.GetComponent<Bullet>().power);
            }    
        }
    }

    public float AccumulateDamage(float target, float power)
    {
        return target + power;
    }
}
