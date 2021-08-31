using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalBullet : Bullet
{
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        if(!AudioManager.instance.sfxSources[2].isPlaying)
        {
            AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.sfxSources[2], bulletClip);
            AudioManager.instance.PlaySource(AudioManager.instance.sfxSources[2]);
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
        float xMovement = transform.position.x + 1 * speed * Time.deltaTime;
        float yMovement = Mathf.Sin(xMovement * Mathf.PI/16) * ySpeed / 2;
        Vector3 movement = new Vector3(xMovement, yMovement, 0);
        rb.MovePosition(movement);
    }

    public override bool CheckCollision(GameObject target)
    {
        if((target.layer == 9 || target.tag == "Player") && target.tag != "SecondarySecondaryWeapon") return true;
        else return false;        
    }
}
