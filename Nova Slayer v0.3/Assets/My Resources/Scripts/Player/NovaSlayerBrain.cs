using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class NovaSlayerBrain : MonoBehaviour
{
    //Nova Slayer Body Component
    NovaSlayerBody myBody;
    //Horizontal & Vertical axis
    float horizontal, vertical;
    //Bool checker if is alive or not
    bool isAlive;
    //Bool checker if can move
    public bool canMove;
    //Bool check if game active
    public bool isGameActive;
    //Bool checker if has lose a life
    bool hasLoseALife;
    //Bool checker if has respawned
    bool hasRespawned;
    //Bool checker if has already fired
    bool hadFired = false;
    //int checker for the current secondary bullet
    int currentSecondaryBullet;
    //Bool checker if is damaged
    bool isDamaged= false;
    //Bool Checker if is on NullAura
    bool isOnNullAura = false;
    //Damage Timer
    float damageTimer=1f;
    // Alphanumeric Keys Array
    public KeyCode[] alphas;
    //Nova Slayer Effects Component
    EffectsManager myEffects;
    //Nova Slayer bool checkers for effects
    bool isSmoke, isFire;
    
    void Start()
    {
        canMove = true;
        isGameActive = true;
        myBody = GetComponent<NovaSlayerBody>();
        myEffects = GetComponent<EffectsManager>();
        currentSecondaryBullet = myBody.getActiveSecondaryBullet();
    }

    void Update()
    {
        if(isGameActive)
        {
            if(CheckHealth())
            {
                if(canMove)
                {
                    CheckKeys();
                    CheckAliveBools();
                }
            }
            else
            {
                LoseLife();
                CheckDeadBools();
            }
        }
    }
    
    public void CheckKeys(){
        
        horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        vertical = CrossPlatformInputManager.GetAxis("Vertical");
        //Checks horizontal and vertical axis
        if(horizontal!=0 || vertical!=0)
        {
            if(!myBody.isShocked) myBody.Move(horizontal, vertical);
        }
        else
        {
            if(myBody.rb.velocity!= Vector2.zero) myBody.rb.velocity= Vector2.zero;
        }
        //Checks fire1 button
        if(CrossPlatformInputManager.GetAxis("Fire1")>0){
            if(myBody.remainingTimeToFire <= 0 && !hadFired)
            {
                myBody.FireBullet();
                hadFired=true;
            }
        }
        if(CrossPlatformInputManager.GetAxis("Fire2") > 0)
        {
            if(!isOnNullAura)
            {
                if(myBody.secondaryBulletRemainingTimeToFire[myBody.getActiveSecondaryBullet()] >= myBody.secondaryBulletFireRate[myBody.getActiveSecondaryBullet()] && !hadFired && myBody.GetSecondaryBulletAmmo(myBody.getActiveSecondaryBullet())>0)
                {
                    myBody.FireSecondaryBullet();
                }
            }
            else
            {
                myBody.PlayPlayerAudio(myBody.nullAudio);
            }
            
        }
        if(CrossPlatformInputManager.GetAxis("Fire3") > 0)
        {
            if(!isOnNullAura)
            {
                if(myBody.plasmaBombRemainingTimeToFire <= 0 && myBody.plasmaBombAmmo > 0)
                {
                    myBody.FirePlasmaBomb();
                }
            }
            else
            {
                myBody.PlayPlayerAudio(myBody.nullAudio);
            }
            
        }
        for(int i = 0; i < alphas.Length; i++)
        {
            if(Input.GetKeyDown(alphas[i]))
            {
                if(myBody.GetIfSecondaryBulletIsOnInventory(i))
                {
                    if(myBody.secondaryBulletAmmo[i] > 0)
                    {
                        if(myBody.getActiveSecondaryBullet() != i)
                        {
                            UICanvas.instance.uIAudio.PlaySecondaryWeaponSelectAudio();
                            myBody.setActiveSecondaryBullet(i);
                        }
                    }

                    else
                    {
                        UICanvas.instance.uIAudio.PlayForbiddenAudio();
                    }
                }
            }
        }
    }

    public void CheckAliveBools(){

        //Checks the active secondary bullet
        if(currentSecondaryBullet != myBody.getActiveSecondaryBullet())
        {
            if(currentSecondaryBullet>-1) UICanvas.instance.secondaryWeaponsUI.HideBar(currentSecondaryBullet);
            currentSecondaryBullet = myBody.getActiveSecondaryBullet();
            myBody.SetCurrentSecondaryBullet();
            UICanvas.instance.secondaryWeaponsUI.ShowBar(currentSecondaryBullet);
        } 
        //Checks if can fire again
        if(myBody.remainingTimeToFire>=myBody.fireRate) hadFired=false;
        //Checks if can fire the secondary weapon again
        if(myBody.secondaryBulletRemainingTimeToFire[myBody.getActiveSecondaryBullet()] <= 0) hadFired = false;
        //Checks if can fire a plasma bomb again
        if(myBody.plasmaBombRemainingTimeToFire <= 0) hadFired = false;
        //Checks if is damaged
        if(isDamaged)
        {
            if(damageTimer<=0)
            {
                isDamaged=false;
                damageTimer=1f;
            }
            else
            {
                damageTimer-=Time.deltaTime;
            }
        }
        //Checks if is shield activated
        if(myBody.isShieldActive)
        {
            if(myBody.remainingTimeToDissapearShield < 0) myBody.DeactivateShield();
            else myBody.remainingTimeToDissapearShield -= Time.deltaTime;
        }
        //Checks if is weapon enhancement activated
        if(myBody.isWeaponEnhanced)
        {
            if(myBody.remainingTimeToDeactivateEnhancement < 0) myBody.DeactivateWeaponEnhancement();
            else myBody.remainingTimeToDeactivateEnhancement -= Time.deltaTime;
        }
        //Checks if is multiple fire activated
        if(myBody.hasMultipleFire)
        {
            if(myBody.remainingTimeToDeactivateMultiple < 0) myBody.DeactivateMultipleFire();
            else myBody.remainingTimeToDeactivateMultiple -= Time.deltaTime;
            
        }
        if(myBody.isShocked)
        {
            if(myBody.remainingTimeToRecoverFromShock < 0) myBody.DeactivateShock();
            else myBody.remainingTimeToRecoverFromShock -= Time.deltaTime;
        }
        //Checks if is invencible
        CheckInvencibility();
    }

    public void CheckDeadBools()
    {
        if(myBody.lives>0)
        {
            if(hasLoseALife)
            {
                if(!hasRespawned)
                {
                    myBody.engine.SetActive(false);
                    myBody.spriteRenderer.color = new Color(255, 255, 255, 0);
                    myBody.isInvencible = true;
                    hasRespawned = true;
                    StartCoroutine(RespawnCo());    
                }
            }
        }

        else
        {
            myBody.spriteRenderer.color = new Color(255, 255, 255, 0);
            canMove = false;
        }
    }

    public IEnumerator RespawnCo()
    {
        myBody.Respawn();
        yield return new WaitForSeconds(1.65f);
        myBody.health = myBody.maxHealth;
        myBody.spriteRenderer.color = new Color(255, 255, 255, 255);
        myBody.engine.SetActive(true);
        hasLoseALife = false;
        myBody.engine.SetActive(true);
    }

    public bool CheckHealth()
    {
        if(myBody.health<=0){
            canMove=false;
        }
        else
        {
            if(myBody.health <= (myBody.maxHealth / 2) && myBody.health > (myBody.maxHealth / 4))
            {
                if(!isSmoke)
                {
                    myEffects.SmokeEffect();
                    isSmoke=true;
                }
                
                if(isFire)
                {
                    myEffects.DestroyEffect("FireEffect");
                    isFire=false;
                }
            }

            else if(myBody.health <= (myBody.maxHealth / 4))
            {
                if(!isFire)
                {
                    myEffects.FireEffect();
                    isFire=true;
                }
            }

            else
            {
                if(isSmoke)
                {
                    myEffects.DestroyEffect("SmokeEffect");
                    isSmoke=false;
                }

                if(isFire)
                {
                    myEffects.DestroyEffect("FireEffect");
                    isFire=false;
                }
            }

            canMove=true;
        }

        return canMove;
    }

    public void LoseLife()
    {
        if(!hasLoseALife)
        {
            myEffects.DestroyEffect("FireEffect");
            myBody.Explode();
            hasLoseALife = true;
            hasRespawned = false;
            GameManager.instance.scoreController.SubstractLivesPoints();
        }
    }

    public void CheckInvencibility()
    {
        if(myBody.isInvencible)
        {
            StartCoroutine(InvencibilityCo());
        }
    }

    public IEnumerator InvencibilityCo()
    {
        yield return new WaitForSeconds(1.5f);
        myBody.isInvencible = false;
    }

    public void InstantiateShockwave()
    {
        myEffects.effectSpawner.prefabToSpawn = myBody.shockEffect;
        myEffects.effectSpawner.Create();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer==8 || (other.gameObject.layer == 11 && other.gameObject.tag == "Fire"))
        {
            if(!isDamaged && !myBody.isInvencible && !myBody.isShieldActive && !GameManager.instance.isGodModeActive)
            {
                myBody.GetDamage(other.gameObject.GetComponent<Bullet>().power);
                myEffects.HitEffect();
                isDamaged=true;
            }
        }

        if(other.gameObject.layer == 11 && other.GetComponent<PlasmaExplosion>()!=null)
        {
            if(!isDamaged && !myBody.isInvencible && !myBody.isShieldActive && !GameManager.instance.isGodModeActive)
            {
                myBody.GetDamage(other.gameObject.GetComponent<PlasmaExplosion>().power);
                myEffects.HitEffect();
                isDamaged=true;
            }
        }

        if(other.gameObject.tag == "Aura") if(!GameManager.instance.isGodModeActive) isOnNullAura = true;

        if(other.gameObject.tag == "Item")
        {
            other.GetComponent<Item>().UseItem(myBody);
            other.GetComponent<Item>().Disappear();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Spaceship")
        {
            if(other.gameObject.GetComponent<NovaBomber>() != null)
            {
                if(!isDamaged && !myBody.isInvencible && !myBody.isShieldActive && !GameManager.instance.isGodModeActive)
                {
                    myBody.GetDamage(other.gameObject.GetComponent<NovaBomber>().bulletPower);
                    myEffects.HitEffect();
                    isDamaged=true;
                }
            }
        }

        if(other.gameObject.layer == 11)
        {
            if(other.gameObject.tag == "Asteroid")
            {
                if(!isDamaged && !myBody.isInvencible && !myBody.isShieldActive && !GameManager.instance.isGodModeActive)
                {
                    myBody.GetDamage(other.gameObject.GetComponent<Asteroid>().impactPower);
                    myEffects.HitEffect();
                    isDamaged=true;
                }
            }
        }    
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Aura") isOnNullAura = false;
    }
}
