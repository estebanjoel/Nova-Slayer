using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBomb : Bullet
{
    public PlasmaExplosion plasmaExplosion;
    // Start is called before the first frame update
    void Start()
    {
        plasmaExplosion = explosion.GetComponent<PlasmaExplosion>();
        plasmaExplosion.power = power;
        if(!AudioManager.instance.sfxSources[1].isPlaying)
        {
            AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.sfxSources[1], bulletClip);
            AudioManager.instance.PlaySource(AudioManager.instance.sfxSources[1]);
        }
        
    }

    // Update is called once per frame
     void Update()
    {
        if(lifespan>0){
            lifespan-=Time.deltaTime;
            Move();
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public override void Move()
    {
        Vector3 movement = new Vector3(1 * speed * Time.deltaTime, 0,0);
        Vector3 newPos = transform.position + movement;
        rb.MovePosition(newPos);
    }

    public override bool CheckCollision(GameObject target)
    {
        if(target.tag == "Spaceship" || target.layer == 11 || target.tag == "Boss") return true;
        else return false;
    }
}
