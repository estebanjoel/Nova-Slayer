using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLifeTime : MonoBehaviour
{
    public float lifeTime;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<Animator>() != null) lifeTime = GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length;
        if(GetComponent<ParticleSystem>() != null) lifeTime = GetComponent<ParticleSystem>().main.duration;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer>=lifeTime) Destroy(gameObject);
        else timer+=Time.deltaTime;
    }
}
