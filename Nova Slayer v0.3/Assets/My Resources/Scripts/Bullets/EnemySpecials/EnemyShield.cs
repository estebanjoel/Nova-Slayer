using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    public Animator anim;
    public float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("lifeTime", lifeTime);
        if(lifeTime <= 0) Destroy(gameObject);
        else lifeTime -= Time.deltaTime;
    }
}
