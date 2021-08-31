using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class EnemyBody : MonoBehaviour
{
    //Enemy's speed
    public float speed;
    //Enemy's target position
    //Enemy's position in X
    public float xPosition;
    //Enemy's position in Y
    public float yPosition;
    //Enemy's health
    public float health;
    //Enemy's max health
    public float maxHealth;
    //Bool checker if its invincible
    public bool isInvencible;
    //Bool checker if is on shield
    public bool isOnShield;
    //Enemy's fireRate
    public float fireRate;
    //Enemy's remaining time to fire
    public float remainingTimeToFire;
    //Enemy's bullet speed
    public float bulletSpeed;
    //Enemy's bullet power
    public float bulletPower;
    //Enemy's fire spawner
    public Spawner fireSpawner;
    //Enemy's bullet game object
    public GameObject bullet;
    // Enemy's explosion game object
    public GameObject explosion;
    //Enemy's RigidBody2D Component
    public Rigidbody2D rb;
    //Enemy's Animator
    public Animator anim;
    //Secondary Effects
    //Bool Checker if enemy is frozen
    public bool isFrozen;
    //Frozen Texture GameObject
    public GameObject freezeTexture;
    //Bool Checker if enemy is on radiaton
    public bool isOnRadiation;
    //Radiation Texture GameObject
    public GameObject radiationTexture;
    //Heal Rate
    public float healRate;
    //Remaining time to heal
    public float remainingTimeToHeal;
    //Audio
    //Hit Audio
    public AudioClip hitAudio;
    //Explosion
    public AudioClip explosionAudio;
    void Start()
    {
    }

    public void SetEnemyStats()
    {
        transform.position=new Vector3(transform.position.x,yPosition,0);
        isInvencible=true;
        remainingTimeToFire=fireRate;
        fireSpawner.prefabToSpawn=bullet;
        fireSpawner.prefabToSpawn.layer=8;
    }

    public void GetDamage(float damage)
    {
        anim.SetTrigger("isDamaged");
        AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.sfxSources[3], hitAudio);
        AudioManager.instance.PlaySource(AudioManager.instance.sfxSources[3]);
        health -= damage;
    }

    public void Die()
    {
        AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.sfxSources[4], explosionAudio);
        AudioManager.instance.PlaySource(AudioManager.instance.sfxSources[4]);
        GameObject.Instantiate(explosion, transform.position, transform.rotation);
    }
    public abstract void FireBullet();

    public void MoveToTarget()
    {
        Vector3 movement = new Vector3(-1 * speed * Time.deltaTime, 0,0);
        Vector3 newPos = transform.position + movement;
        rb.MovePosition(newPos);
    }

    public void SetTextureActive(bool checker, GameObject texture)
    {
        texture.SetActive(checker);
    }

    public void Heal()
    {
        health+=5;
        if(health > maxHealth) health = maxHealth;
        setHealRate();
    }

    public void setHealRate()
    {
        remainingTimeToHeal = healRate;
    }

}
