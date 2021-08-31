using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEnums;

public class NovaSlayerBody : MonoBehaviour
{
    [Header("Main Stats")]
    //Nova Slayer's speed
    public float speed;
    //Nova Slayer's health
    public float health;
    //Nova Slayer's max health
    public float maxHealth;
    //Nova Slayer's lives
    public int lives;
    //Nova Slayer's movement bounds
    public float leftBound, rightBound, topBound, bottomBound;
    //Nova Slayer's fire rate
    public float fireRate;
    //Nova Slayer's remaining time to fire
    public float remainingTimeToFire;
    //Nova Slayer's bullet speed
    public float bulletSpeed;
    //Nova Slayer's bullet power
    public float bulletPower;
    //Nova Slayer fire spawner
    [SerializeField] Spawner mainFireSpawner;
    //Nova Slayer additional fire spawners
    [SerializeField] Spawner[] additionalFireSpawners;
    //Nova Slayer secondary fire spawner
    [SerializeField] Spawner SecondaryFireSpawner;
    //Nova Slayer bullet game object
    [SerializeField] GameObject bullet;
    [Header("Secondary Bullet Stats")]
    //Nova Slayer secondary bullets
    public GameObject[] secondaryBullets;
    //Nova Slayer secondary bullet type
    public SecondaryBulletType[] secondaryBulletTypes;
    //Nova Slayer secondary bullet ammo
    public int[] secondaryBulletAmmo;
    //Nova Slayer secondary bullet fireRate
    public float[] secondaryBulletFireRate;
    //Nova Slayer secondary bullet remaining time to fire
    public float[] secondaryBulletRemainingTimeToFire;
    //Nova Slayer active secondary bullet checkers
    public bool[] secondaryBulletActive;
    //Nova Slayer current secondary bullet
    public GameObject currentSecondaryBullet;
    //Nova Slayer secondary bullet on inventory checker
    public bool[] secondaryBulletOnInventory;
    [Header("Secondary Effects")]
    //Secondary Effects Bools
    public bool isShieldActive;
    public bool isWeaponEnhanced;
    public bool hasMultipleFire;
    public bool isShocked;
    //Secondary Effects Timers
    //Shield
    public float shieldRate;
    public float remainingTimeToDissapearShield;
    //Enhanced Weapon
    public float enhancedRate;
    public float remainingTimeToDeactivateEnhancement;
    //Multiple Fire
    public float multipleRate;
    public float remainingTimeToDeactivateMultiple;
    public float shockRate;
    public float remainingTimeToRecoverFromShock;
    //Plasma Bomb
    [Header("Plasma Bomb Info")]
    //Plasma Bomb GameObject
    public GameObject plasmaBomb;
    //Plasma Bomb Spawner
    public Spawner plasmaBombSpawner;
    //Plasma Bomb Power
    public float plasmaBombPower;
    //Plasma Bomb Ammo
    public int plasmaBombAmmo;
    //Plasma Bomb Fire Rate
    public float plasmaBombFireRate;
    //Plasma Bomb Remaining Time To Fire
    public float plasmaBombRemainingTimeToFire;
    [Header("Other GameObjects")]
    //Nova Slayer shield game object
    public GameObject shield;
    //Nova Slayer engine game object
    public GameObject engine;
    //Nova Slayer shockwave game object
    public GameObject shockEffect;
    //Nova Slayer explosion game object
    [SerializeField] GameObject explosion;
    //Nova Slayer respawn game object
    public GameObject respawn;
    [Header("Necesary Variables")]
    //Nova Slayer RigidBody2D Component
    public Rigidbody2D rb;
    //Nova Slayer SpriteRenderer Component
    public SpriteRenderer spriteRenderer;
    //Nova Slayer's bool checker if is invencible
    public bool isInvencible;
    //Nova Slayers' normal bullet sprite
    public Sprite normalBulletSprite;
    //Nova Slayer's enhanced bullet sprite
    public Sprite enhancedBulletSprite;
    [Header("Audio Clips")]
    //Nova Slayer Audio Source Component
    public AudioSource myAudio;
    //Nova Slayer hit audio clip
    public AudioClip hitAudio;
    //Nova Slayer explosion audio clip
    public AudioClip explosionAudio;
    //nOVA sLAYER null audio
    public AudioClip nullAudio;
   
    void Start()
    {
    }

    void FixedUpdate()
    {
        if(remainingTimeToFire>0) remainingTimeToFire-=Time.deltaTime;
        if(secondaryBulletRemainingTimeToFire[getActiveSecondaryBullet()] <= secondaryBulletFireRate[getActiveSecondaryBullet()]) secondaryBulletRemainingTimeToFire[getActiveSecondaryBullet()] += Time.deltaTime;
        if(plasmaBombRemainingTimeToFire > 0) plasmaBombRemainingTimeToFire -= Time.deltaTime;
    }

    public void SetInitialElements()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetCameraBounds();
        myAudio = AudioManager.instance.sfxSources[0];
        remainingTimeToFire = fireRate;
        mainFireSpawner.prefabToSpawn = bullet;
        mainFireSpawner.prefabToSpawn.layer = 9;
        additionalFireSpawners[0].prefabToSpawn = bullet;
        additionalFireSpawners[0].prefabToSpawn.layer = 9;
        additionalFireSpawners[1].prefabToSpawn = bullet;
        additionalFireSpawners[1].prefabToSpawn.layer = 9;
        bullet.GetComponent<Bullet>().speed = bulletSpeed;
        bullet.GetComponent<Bullet>().power = bulletPower;
        plasmaBomb.GetComponent<PlasmaBomb>().power = plasmaBombPower;
        plasmaBombSpawner.prefabToSpawn = plasmaBomb;
        secondaryBulletActive[0] = true;
        SetCurrentSecondaryBullet();
        DeactivateShield();
        DeactivateMultipleFire();
        DeactivateShield();
    }

    public void SetCameraBounds()
    {
        float verExtent = Camera.main.orthographicSize;
        float horzExtent = verExtent * Screen.width / Screen.height;
        rightBound = horzExtent - horzExtent / 4f - transform.position.x / 16f;
        leftBound = transform.position.x / 16f - horzExtent + horzExtent / 8f;
        bottomBound = (transform.position.y / 2f - verExtent) + 1.05f;
        topBound = (verExtent - transform.position.y / 2f) - 1.05f;
    }

    public void GetDamage(float damage)
    {
        health-=damage;
        PlayPlayerAudio(hitAudio);
    }

    public void Move(float hAxis,float vAxis)
    {
        Vector3 movement = new Vector3(hAxis * speed * Time.deltaTime, vAxis * speed * Time.deltaTime,0);
        Vector3 newPos = transform.position + movement;
        newPos = CheckIfMovementExceedsBounds(newPos);
        rb.MovePosition(newPos);
    }

    public Vector3 CheckIfMovementExceedsBounds(Vector3 pos)
    {
        bool exceedLeftBound = false;
        bool exceedRightBound = false;
        bool exceedTopBound = false;
        bool exceedBottomBound = false;
        if(pos.x < leftBound) exceedLeftBound = true;
        if(pos.x > rightBound) exceedRightBound = true;
        if(pos.y > topBound) exceedTopBound = true;
        if(pos.y < bottomBound) exceedBottomBound = true;
        if(exceedLeftBound)
        {
            if(exceedTopBound) pos = new Vector3(leftBound, topBound, 0);
            else if(exceedBottomBound) pos = new Vector3(leftBound, bottomBound, 0);
            else pos = new Vector3(leftBound,pos.y, 0);
        }
        else if(exceedRightBound)
        {
            if(exceedTopBound) pos = new Vector3(rightBound, topBound, 0);
            else if(exceedBottomBound) pos = new Vector3(rightBound, bottomBound, 0);
            else pos = new Vector3(rightBound,pos.y, 0);
        }
        else
        {
            if(exceedTopBound) pos = new Vector3(pos.x, topBound, 0);
            else if(exceedBottomBound) pos = new Vector3(pos.x, bottomBound, 0);
            else pos = new Vector3(pos.x,pos.y, 0);
        }
        return pos;
    }

    public void FireBullet()
    {
        if(isWeaponEnhanced)
        {
            mainFireSpawner.prefabToSpawn.GetComponent<SpriteRenderer>().sprite = enhancedBulletSprite;
            mainFireSpawner.prefabToSpawn.GetComponent<PlayerBullet>().isBulletEnhanced = true;
            if(hasMultipleFire)
            {
                additionalFireSpawners[0].prefabToSpawn.GetComponent<SpriteRenderer>().sprite = enhancedBulletSprite;
                additionalFireSpawners[0].prefabToSpawn.GetComponent<PlayerBullet>().isBulletEnhanced = true;
                additionalFireSpawners[1].prefabToSpawn.GetComponent<SpriteRenderer>().sprite = enhancedBulletSprite;
                additionalFireSpawners[1].prefabToSpawn.GetComponent<PlayerBullet>().isBulletEnhanced = true;
            }
        } 
        else
        {
            mainFireSpawner.prefabToSpawn.GetComponent<SpriteRenderer>().sprite = normalBulletSprite;
            mainFireSpawner.prefabToSpawn.GetComponent<PlayerBullet>().isBulletEnhanced = false;
            if(hasMultipleFire)
            {
                additionalFireSpawners[0].prefabToSpawn.GetComponent<SpriteRenderer>().sprite = normalBulletSprite;
                additionalFireSpawners[0].prefabToSpawn.GetComponent<PlayerBullet>().isBulletEnhanced = false;
                additionalFireSpawners[1].prefabToSpawn.GetComponent<SpriteRenderer>().sprite = normalBulletSprite;
                additionalFireSpawners[1].prefabToSpawn.GetComponent<PlayerBullet>().isBulletEnhanced = false;
            }
        }
        mainFireSpawner.prefabToSpawn.GetComponent<Bullet>().ySpeed = 0;
        mainFireSpawner.Create();
        if(hasMultipleFire)
        {
            additionalFireSpawners[0].prefabToSpawn.GetComponent<Bullet>().ySpeed = 0.1f;
            additionalFireSpawners[0].Create();
            additionalFireSpawners[1].prefabToSpawn.GetComponent<Bullet>().ySpeed = -0.1f;
            additionalFireSpawners[1].Create();
        }
        remainingTimeToFire=fireRate;
    }

    public void AddSecondaryBulletToInventory(int i)
    {
        secondaryBulletOnInventory[i] = true;
    }

    public bool GetIfSecondaryBulletIsOnInventory(int i)
    {
        return secondaryBulletOnInventory[i];
    }

    public void FireSecondaryBullet()
    {
        SecondaryFireSpawner.Create();
        secondaryBulletRemainingTimeToFire[getActiveSecondaryBullet()] = 0;
        secondaryBulletAmmo[getActiveSecondaryBullet()]--;
        UICanvas.instance.secondaryWeaponsUI.SetAmmoText(getActiveSecondaryBullet(), NovaSlayer.instance.body.secondaryBulletAmmo[getActiveSecondaryBullet()]);
    }

    public void FirePlasmaBomb()
    {
        plasmaBombSpawner.Create();
        plasmaBombRemainingTimeToFire = plasmaBombFireRate;
        plasmaBombAmmo--;
        UICanvas.instance.plasmaBombCounter.SetCounter(plasmaBombAmmo);
    }

    public void SetCurrentSecondaryBullet()
    {
        currentSecondaryBullet = secondaryBullets[getActiveSecondaryBullet()];
        SecondaryFireSpawner.prefabToSpawn = secondaryBullets[getActiveSecondaryBullet()];
    }

    public void setActiveSecondaryBullet(int i)
    {
        DeactivateAllSecondaryBullets();
        secondaryBulletActive[i] = true;
    }

    public int getActiveSecondaryBullet()
    {
        for(int i = 0; i < secondaryBulletActive.Length; i++)
        {
            if(secondaryBulletActive[i]) return i;
        }
        return -1;
    }

    public void DeactivateAllSecondaryBullets()
    {
       for(int i = 0; i < secondaryBulletActive.Length; i++)
       {
           secondaryBulletActive[i] = false;
       }
    }

    public int GetSecondaryBulletAmmo(int i)
    {
        return secondaryBulletAmmo[i];
    }

    public void AddSecondaryBulletAmmo(int target, int ammo)
    {
        secondaryBulletAmmo[target] += ammo;
    }

    public void ActivateShield()
    {
        isShieldActive = true;
        shield.SetActive(true);
        remainingTimeToDissapearShield = shieldRate;
    }

    public void DeactivateShield()
    {
        isShieldActive = false;
        shield.SetActive(false);
    }

    public void ActivateWeaponEnhancement()
    {
        isWeaponEnhanced = true;
        remainingTimeToDeactivateEnhancement = enhancedRate;
        UICanvas.instance.gamePanels.ShowPanel(UICanvas.instance.gamePanels.EnhancedIcon);
    }

    public void DeactivateWeaponEnhancement()
    {
        isWeaponEnhanced = false;
        UICanvas.instance.gamePanels.HidePanel(UICanvas.instance.gamePanels.EnhancedIcon);
    }

    public void ActivateMultipleFire()
    {
        hasMultipleFire = true;
        remainingTimeToDeactivateMultiple = multipleRate;
    }

    public void DeactivateMultipleFire()
    {
        hasMultipleFire = false;
    }

    public void Shock()
    {
        isShocked = true;
        remainingTimeToRecoverFromShock = shockRate;
        GetComponent<NovaSlayerBrain>().InstantiateShockwave();
    }

    public void DeactivateShock()
    {
        isShocked = false;
    }

    public void Explode()
    {
        lives--;
        PlayPlayerAudio(explosionAudio);
        GameObject.Instantiate(explosion, transform.position, transform.rotation);
        UICanvas.instance.lifeCounter.ChangeLifeCounterText(lives.ToString());
        engine.SetActive(false);
    }

    public void Respawn()
    {
        GameObject.Instantiate(respawn, transform.position, transform.rotation);
    }



    public void PlayPlayerAudio(AudioClip audio)
    {
        AudioManager.instance.ChangeAudioClipFormSource(myAudio, audio);
        AudioManager.instance.PlaySource(myAudio);
    }

}
