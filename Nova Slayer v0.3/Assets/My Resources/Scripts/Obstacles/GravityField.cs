using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    public float lifeTime;
    public Animator anim;
    Collider2D targetCollider;
    public float pullRadius = 5;
    public float pullForce = 10;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        targetCollider = NovaSlayer.instance.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("lifeTime", lifeTime);
        if(lifeTime <= 0)
        {
            if(transform.localScale.x <= 0) Destroy(gameObject);
        } 
        else
        {
            lifeTime -= Time.deltaTime;
        } 
    }

    void FixedUpdate()
    {
        if(Physics2D.OverlapCircle(transform.position, pullRadius).IsTouching(targetCollider))
        {
            // Debug.Log("Nova Slayer is on gravity field");
            Vector3 forceDirection = transform.position + targetCollider.transform.position;
            targetCollider.GetComponent<Rigidbody2D>().AddForce(forceDirection * -pullForce, ForceMode2D.Impulse);
        }
    }
}
