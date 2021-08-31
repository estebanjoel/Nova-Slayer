using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    public bool isBulletEnhanced;
    float enhancedPower;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        enhancedPower = power + ((power * 25) / 100);
        if(isBulletEnhanced) power = enhancedPower; 
        AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.sfxSources[1], bulletClip);
        AudioManager.instance.PlaySource(AudioManager.instance.sfxSources[1]);
    }

    public override void Move()
    {
        Vector3 movement = new Vector3(1 * speed * Time.deltaTime, 1 * ySpeed,0);
        Vector3 newPos = transform.position + movement;
        rb.MovePosition(newPos);
    }

    public override bool CheckCollision(GameObject target)
    {
        if(gameObject.tag == "SecondaryWeapon")
        {
            if(target.layer == 11 || target.tag == "Spaceship" || target.tag == "Boss") return true;
            else return false;
        }
        else
        {
            if(target.tag == "Spaceship" || target.tag == "SpecialBullet" 
            || target.layer == 11 || target.tag == "Boss") return true;
            else return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(lifespan>0){
            lifespan-=Time.deltaTime;
            Move();
        }

        else{
            Destroy(gameObject);
        }
    }
}
