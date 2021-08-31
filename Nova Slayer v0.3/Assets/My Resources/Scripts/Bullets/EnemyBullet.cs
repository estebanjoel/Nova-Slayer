using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
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
        Vector3 movement = new Vector3(1 * speed * Time.deltaTime, 0,0);
        Vector3 newPos = transform.position + movement;
        rb.MovePosition(newPos);
    }

    public override bool CheckCollision(GameObject target)
    {
        if((target.tag == "Player" || target.layer == 9) && target.tag != "SecondarySecondaryWeapon") return true;
        else return false;
    }

}
