using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEnums;
public class EnemyBrain : MonoBehaviour
{
    //Enemy's Body
    public EnemyBody myBody;
    //Bool checker if it gets damage
    public bool isDamaged = false;
    //Bool checker if enemy is boss
    public bool isBoss;
    //Bool checker if can boss explode
    public bool canBossExplode;
    //Damage's Timer
    public float damageTimer;
    //Actual Enemy's Type
    public int enemyType;
    //Bool checker if can move
    public bool canMove;
    // Radiation Damage
    float radiationDamage;
    // Radiation Rate
    public float radiationRate;
    // Remaining time to recieve radiation damage
    public float remainingTimeToRadiation;
    // Radiation Counter
    int radiationCounter = 3;
    // Current Radiation Counter
    int currentRadiationCounter;
    // bool checker if has set the radiation counter
    bool hasSetRadiationCounter;
    //Enemy's Effects Manager
    EffectsManager myEffects;
    //Effects boolean checkers
    bool isSmoke, isFire;
    //Checker if is on healField
    public bool isOnHealField;
    GameObject lifeBar;
    void Start()
    {
        myBody=GetComponent<EnemyBody>();
        myEffects = GetComponent<EffectsManager>(); 
        switch(GameManager.instance.currentDifficulty)
        {
            case 0:
                radiationRate = 2.5f;
                damageTimer=0.5f;
                myBody.healRate = 2f;
                myBody.fireRate += myBody.fireRate/2;
                break;
            case 1:
                radiationRate = 2;
                damageTimer=0.75f;
                myBody.healRate = 1.5f;
                break;
            case 2:
                radiationRate = 1.5f;
                damageTimer=1.25f;
                myBody.healRate = 1f;
                myBody.fireRate -= myBody.fireRate/2;
                break;
        }
        if(!isBoss)
        {
            switch(GameManager.instance.currentDifficulty)
            {
                case 0:
                    myBody.maxHealth-= myBody.maxHealth/4;
                    myBody.health = myBody.maxHealth;
                    break;
                case 1:
                    myBody.health = myBody.maxHealth;;
                    lifeBar =  GetComponentInChildren<EnemyHealthBar>().gameObject;
                    lifeBar.SetActive(false);
                    break;
                case 2:
                    myBody.maxHealth += myBody.maxHealth/2;
                    myBody.health = myBody.maxHealth;
                    myBody.bulletPower += myBody.bulletPower/4;
                    lifeBar =  GetComponentInChildren<EnemyHealthBar>().gameObject;
                    lifeBar.SetActive(false);
                    break;
            }
        }
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckLife())
        {
            canMove = CheckIfEnemyCanMove();
            CheckDamage();
            if(canMove)
            {
                if(CheckTargetPosition())
                {
                    if(CheckFireTarget()) CheckFireRate();
                }
                if(CheckIfEnemyIsOnRadiation())
                {
                    if(!hasSetRadiationCounter)
                    {
                        currentRadiationCounter = radiationCounter;
                        hasSetRadiationCounter = true;
                    }

                    if(currentRadiationCounter > 0)
                    {
                        RadiationDamage();
                    } 
                    else
                    {
                        hasSetRadiationCounter = false;
                        myBody.isOnRadiation = false;
                    } 
                }
                if(isOnHealField)
                {
                    if(myBody.remainingTimeToHeal <= 0) myBody.Heal();
                    else myBody.remainingTimeToHeal -= Time.deltaTime;
                }
            }
            else
            {
                if(CheckIfEnemyIsFrozen()) StartCoroutine(DefreezeCo());
            }
        }
        else
        {
            // if(isBoss)
            // {
            //     BossDeath();
            //     if(canBossExplode) StartCoroutine(DeathCoroutine());    
            // }
            // else
            // {
            //     StartCoroutine(DeathCoroutine());
            // }
            StartCoroutine(DeathCoroutine());
            
        } 
    }

    public void CheckHeal()
    {
        if(myBody.remainingTimeToHeal <= 0) myBody.Heal();
        else myBody.remainingTimeToHeal -= Time.deltaTime;
    }

    //Checks if is still alive
    public bool CheckLife(){

        if(myBody.health <= 0)
        {
            GameManager.instance.scoreController.AddKillPoints();
            if(!isBoss){}myBody.Die();
            return false;
        }

        else
        {
            if(myBody.health <= (myBody.maxHealth/2))
            {
                if(!isFire)
                {
                    myEffects.FireEffect();
                    isFire = true;
                }
            }

            else
            {
                if(isFire)
                {
                    myEffects.DestroyEffect("FireEffect");
                    isFire = false;
                }
            }
            return true;
        }    
    }

    //Check if reached the target's position
    public bool CheckTargetPosition(){

        if(transform.position.x>=myBody.xPosition)
        {
            myBody.MoveToTarget();
            return false;
        }
        else{
            if(!isBoss)
            {
                myBody.rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                myBody.rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
            myBody.isInvencible=false;
            return true;
        }
    }

    //Check if Enemy can Move
    public bool CheckIfEnemyCanMove()
    {
        if(CheckIfEnemyIsFrozen()) return false;
        else return true;
    }

    //Check if gets damaged
    public void CheckDamage(){
        if(isDamaged){
            if(damageTimer<=0)
            {
                isDamaged=false;
                damageTimer=0.75f;
                if(!isBoss) if(GameManager.instance.currentDifficulty == 1) lifeBar.SetActive(false);
            }
            else{
                if(!isBoss) if(GameManager.instance.currentDifficulty == 1) lifeBar.SetActive(true);
                damageTimer-=Time.deltaTime;
            }
        }
    }

    public bool CheckFireTarget()
    {
        if(NovaSlayer.instance.body.lives > 0) return true;
        else return false;
    }

    public void CheckFireRate(){
        if(myBody.remainingTimeToFire<=0)
        {
            myBody.FireBullet();
            myBody.remainingTimeToFire=myBody.fireRate;
        }
        else
        {
            myBody.remainingTimeToFire-=Time.deltaTime;
        }
    }

    public bool CheckIfEnemyIsFrozen()
    {
        myBody.SetTextureActive(myBody.isFrozen, myBody.freezeTexture);
        return myBody.isFrozen;
    }

    public IEnumerator DefreezeCo()
    {
        yield return new WaitForSeconds(3f);
        myBody.isFrozen = false;
    }

    public bool CheckIfEnemyIsOnRadiation()
    {
        myBody.SetTextureActive(myBody.isOnRadiation, myBody.radiationTexture);
        return myBody.isOnRadiation;
    }

    public void RadiationDamage()
    {
        if(remainingTimeToRadiation <= 0)
        {
            myBody.health -= radiationDamage;
            remainingTimeToRadiation = radiationRate;
            currentRadiationCounter--;
        }
        else
        {
            remainingTimeToRadiation -= Time.deltaTime;
        }
    }

    public void BossDeath()
    {
        switch(GameManager.instance.levelManager.currentScene)
        {
            case "Lvl 1":
                if(GetComponent<GeminiShip>().canBeginMiniExplosions) StartCoroutine(GetComponent<GeminiShip>().miniExplosionsCo());
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer==9)
        {
            if(GameManager.instance.isGodModeActive)
            {
                myBody.health = 0;
            }
            else
            {
                if(other.gameObject.tag == "SecondaryWeapon" && !isBoss)
                {
                    SetSecondaryEffect(other.gameObject);
                }
                
                if(!isDamaged && !myBody.isInvencible && !myBody.isOnShield)
                {
                    if(other.gameObject.GetComponent<PlasmaExplosion>() != null) myBody.GetDamage(other.gameObject.GetComponent<PlasmaExplosion>().power);
                    else myBody.GetDamage(other.gameObject.GetComponent<Bullet>().power);
                    isDamaged=true;
                    myEffects.HitEffect();
                    myBody.rb.velocity=Vector2.zero;
                }
            }
            
        }

        if(other.gameObject.tag == "Shield" && other.gameObject.layer == 10)
        {
            if(GetComponent<NovaShield>() == null) myBody.isOnShield = true;            
        }

        if(other.gameObject.tag == "Heal" && other.gameObject.layer == 10)
        {
            if(GetComponent<NovaShield>() == null) isOnHealField = true;            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Shield" && other.gameObject.layer == 10)
        {
            if(GetComponent<NovaShield>() == null) myBody.isOnShield = false;
        }
        if(other.gameObject.tag == "Heal" && other.gameObject.layer == 10)
        {
            if(GetComponent<NovaShield>() == null) isOnHealField = false;            
        }
    }

    public void SetSecondaryEffect(GameObject target)
    {
        switch(target.GetComponent<SecondaryPlayerBullet>().secondary)
        {
            case SecondaryBulletType.Iceball:
                myBody.isFrozen = true;
                break;
            case SecondaryBulletType.RadioactiveShot:
                if(!myBody.isFrozen && !myBody.isOnRadiation)
                {
                    myBody.isOnRadiation = true;
                    switch(GameManager.instance.currentDifficulty)
                    {
                        case 0:
                            radiationDamage = target.GetComponent<Bullet>().power * 1.75f;
                            break;
                        case 1:
                            radiationDamage = target.GetComponent<Bullet>().power * 1.5f;
                            break;
                        case 2:
                            radiationDamage = target.GetComponent<Bullet>().power * 1.25f;
                            break;
                    }
                    remainingTimeToRadiation = radiationRate;
                } 
                break;
            case SecondaryBulletType.EMP:
                myBody.health = 0;
                break;
        }
    }

    public IEnumerator DeathCoroutine()
    {
        canBossExplode = false;
        yield return new WaitForSeconds(myBody.explosion.GetComponent<EffectLifeTime>().lifeTime);
        Destroy(gameObject);
    }
}
