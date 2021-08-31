using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelaxAndEnjoyLaser : Bullet
{
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();    
    }
    void Update()
    {
        anim.SetFloat("lifeTime" ,lifespan);
        if(lifespan>0) lifespan-= Time.deltaTime;
        else Destroy(gameObject);
    }
    public override void Move()
    {
        power = power + 0.001f;
    }

    public override bool CheckCollision(GameObject target)
    {
        return false;
    }
}
