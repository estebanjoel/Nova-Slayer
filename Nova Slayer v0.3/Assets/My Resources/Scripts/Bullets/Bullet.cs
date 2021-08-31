using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    //bullet's lifespan
    public float lifespan;
    //bullet's speed
    public float speed;
    //bullet's speed
    public float ySpeed;
    //bullet's power
    public float power;
    //bullet's rigid body
    public Rigidbody2D rb;
    //bullet's explosion GameObject
    public GameObject explosion;
    public AudioClip bulletClip;

    void Update()
    {
        if(lifespan>0) lifespan-= Time.deltaTime;
        else Destroy(gameObject);
    }
    public abstract void Move();
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(CheckCollision(other.gameObject))
        {
            GameObject newExplosion = GameObject.Instantiate(explosion);
            newExplosion.transform.position = transform.position;
            Destroy(gameObject);
        }
    }

    public abstract bool CheckCollision(GameObject target);
}
