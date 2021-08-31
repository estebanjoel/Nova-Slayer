using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public float lifeTime;
    public float xSpeed, ySpeed;
    public Rigidbody2D rb;
    public AudioClip itemClip;
    public EffectLifeTime itemEffect;

    void Update()
    {
        if(lifeTime > 0)
        {
            float xMovement = transform.position.x - xSpeed * Time.deltaTime;
            float yMovement = (Mathf.Sin(xMovement * Mathf.PI/8) * ySpeed);
            Vector3 movement = new Vector3(xMovement, yMovement, 0);
            rb.MovePosition(movement);
            lifeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public abstract void UseItem(NovaSlayerBody target);
    public void Disappear()
    {
        if(!AudioManager.instance.sfxSources[8].isPlaying)
        {
            AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.sfxSources[8], itemClip);
            AudioManager.instance.PlaySource(AudioManager.instance.sfxSources[8]);
        }
        Instantiate(itemEffect.gameObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
