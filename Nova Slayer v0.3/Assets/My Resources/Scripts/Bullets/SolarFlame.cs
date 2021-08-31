using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarFlame : Bullet
{
    
    void Start()
    {
    }

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
        return false;
    }
}
