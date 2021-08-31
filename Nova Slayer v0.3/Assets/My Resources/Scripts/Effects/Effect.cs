using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    float initialTimer = 0;
    public float lifeSpawn;

    // Update is called once per frame
    void Update()
    {
        if(initialTimer>=lifeSpawn)
        {
            Destroy(this.gameObject);
        }
        else
        {
            initialTimer+=Time.deltaTime;
        }
    }
}
