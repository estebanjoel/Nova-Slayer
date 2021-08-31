using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEnums;

public class TutorialNovaCruiser : MonoBehaviour
{
    public float speed;
    public float xPosition;
    public float yPosition;
    public bool isInvencible;
    public float fireRate;
    public float remainingTimeToFire;
    public float bulletSpeed;
    public float bulletPower;
    public Spawner fireSpawner;
    public GameObject bullet;
    public GameObject explosion;
    public Rigidbody2D rb;
    public bool isFrozen;
    public GameObject freezeTexture;
    public bool isOnRadiation;
    public GameObject radiationTexture;
    public AudioClip hitAudio;
    public AudioClip explosionAudio;
    public bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        transform.position=new Vector3(transform.position.x,yPosition,0);
        isInvencible=true;
        remainingTimeToFire=fireRate;
        fireSpawner.prefabToSpawn=bullet;
        fireSpawner.prefabToSpawn.layer=8;
        radiationTexture.SetActive(false);
    }

    void Update()
    {
         canMove = CheckIfEnemyCanMove();
         if(canMove) CheckTargetPosition();
         else if(CheckIfEnemyIsFrozen()) StartCoroutine(DefreezeCo());
    }

    public void Die()
    {
        AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.sfxSources[4], explosionAudio);
        AudioManager.instance.PlaySource(AudioManager.instance.sfxSources[4]);
        GameObject.Instantiate(explosion, transform.position, transform.rotation);
        StartCoroutine(DeathCoroutine());
    }

    public void MoveToTarget()
    {
        Vector3 movement = new Vector3(-1 * speed * Time.deltaTime, 0,0);
        Vector3 newPos = transform.position + movement;
        rb.MovePosition(newPos);
    }

    public bool CheckTargetPosition(){

        if(transform.position.x>=xPosition)
        {
            MoveToTarget();
            return false;
        }
        else{
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            isInvencible=false;
            return true;
        }
    }

    public void SetTextureActive(bool checker, GameObject texture)
    {
        texture.SetActive(checker);
    }

     public bool CheckIfEnemyIsFrozen()
    {
        SetTextureActive(isFrozen, freezeTexture);
        return isFrozen;
    }

    public IEnumerator DefreezeCo()
    {
        yield return new WaitForSeconds(3f);
        isFrozen = false;
    }

    //Check if Enemy can Move
    public bool CheckIfEnemyCanMove()
    {
        if(CheckIfEnemyIsFrozen()) return false;
        else return true;
    }

    public bool CheckIfEnemyIsOnRadiation()
    {
        SetTextureActive(isOnRadiation, radiationTexture);
        return isOnRadiation;
    }

    public void SetSecondaryEffect(GameObject target)
    {
        switch(target.GetComponent<SecondaryPlayerBullet>().secondary)
        {
            case SecondaryBulletType.Iceball:
                isFrozen = true;
                break;
            case SecondaryBulletType.RadioactiveShot:
                if(!isFrozen && !isOnRadiation)
                {
                    isOnRadiation = true;
                    SetTextureActive(isOnRadiation, radiationTexture);
                } 
                break;
            case SecondaryBulletType.EMP:
                Die();
                break;
        }
    }

    public IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(explosion.GetComponent<EffectLifeTime>().lifeTime);
        Destroy(gameObject);
    }

     private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer==9)
        {
            if(other.gameObject.tag == "SecondaryWeapon")
            {
                SetSecondaryEffect(other.gameObject);
            }

            else
            {
                if(!isInvencible)
                {
                   Die();
                }
            }
            
        }
    }
}
