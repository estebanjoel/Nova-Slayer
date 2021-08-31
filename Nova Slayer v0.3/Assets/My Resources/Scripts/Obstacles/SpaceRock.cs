using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceRock : MonoBehaviour
{
    public float ySpeed;
    public float yTarget;
    private float yInitialPos;
    public float health;
    bool getDamaged;
    public float damageTimer;
    bool canBeDestroyed;
    public Animator anim;
    public GameObject explosion;
    public float explosionScale;
    public AudioClip explosionClip;
    void Start()
    {
        yInitialPos = transform.position.y;
        anim = GetComponent<Animator>();
        explosion.transform.localScale = new Vector3(explosionScale, explosionScale, 1);
    }

    void Update()
    {
        if(yInitialPos > 0)
        {
            if(transform.position.y >= yTarget) transform.position += transform.up * -1 * ySpeed * Time.deltaTime;
            else canBeDestroyed = true;
        }
        else
        {
            if(transform.position.y <= yTarget) transform.position += transform.up * ySpeed * Time.deltaTime;
            else canBeDestroyed = true;
        }

        if(health <= 0)
        {
            Explode();
        }
        else
        {
            // transform.Rotate(transform.forward * ySpeed);
            if(getDamaged)
            {
                if(damageTimer < 0) getDamaged = false;
                else damageTimer -= Time.deltaTime;
            }
        }
    }

    public void GetDamage(float pow)
    {
        health -= pow;
        damageTimer = 0.5f;
        getDamaged = true;
        anim.SetTrigger("damageTrigger");
    }

    public void Explode()
    {
        GameObject.Instantiate(explosion, transform.position, transform.rotation);
        if(!AudioManager.instance.sfxSources[7].isPlaying)
        {
            AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.sfxSources[7], explosionClip);
            AudioManager.instance.PlaySource(AudioManager.instance.sfxSources[7]);
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(((other.gameObject.tag == "Fire" || other.gameObject.tag == "SecondaryWeapon") && other.gameObject.layer == 9))
        {
            if(!GameManager.instance.isGodModeActive)
            {
                if(!getDamaged && canBeDestroyed)
                {
                    GetDamage(other.GetComponent<Bullet>().power);
                }
            }

            else
            {
                health = 0;
            }
            
        }
    }
}
