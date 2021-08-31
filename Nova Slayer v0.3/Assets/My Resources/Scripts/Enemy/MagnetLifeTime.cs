using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetLifeTime : MonoBehaviour
{
    public float lifeTime;

    void Update()
    {
        if(lifeTime <= 0) Destroy(gameObject);
        else lifeTime -= Time.deltaTime;
    }
}
