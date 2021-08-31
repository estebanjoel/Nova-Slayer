using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaBomber : EnemyBody
{
    float followSpeed;
    // Start is called before the first frame update
    void Start()
    {
        followSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<EnemyBrain>().CheckTargetPosition())
        {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindObjectOfType<NovaSlayer>().transform.position, Time.deltaTime * followSpeed);
        }
    }

    public override void FireBullet()
    {
        speed = speed * 0.1f;
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            health = 0;
        }
    }
}
