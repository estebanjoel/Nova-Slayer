using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    //Nova Slayer effects spawner
    public Spawner effectSpawner;
    //Nova Slayer hit effect game object
    [SerializeField] GameObject hitEffect;
    //Nova Slayer hit effect x & y position
    public float hitXPosition, hitYPosition;
    //Nova Slayer smoke effect game object
    [SerializeField] GameObject smokeEffect;
    //Nova Slayer smoke effect x & y position
    public float smokeXPosition, smokeYPosition;
    //Nova Slayer fire effect game object
    public GameObject fireEffect;
    //Nova Slayer fire effect x & y position
    public float fireXPosition, fireYPosition;

    public void HitEffect()
    {
        effectSpawner.prefabToSpawn = hitEffect;
        effectSpawner.SetParentPosition(hitXPosition,hitYPosition);
        effectSpawner.Create();
    }

    public void SmokeEffect()
    {
        effectSpawner.prefabToSpawn = smokeEffect;
        effectSpawner.SetParentPosition(smokeXPosition,smokeYPosition);
        effectSpawner.Create();
    }

    public void FireEffect()
    {
        effectSpawner.prefabToSpawn = fireEffect;
        effectSpawner.SetParentPosition(fireXPosition,fireYPosition);
        effectSpawner.Create();
    }

    public void DestroyEffect(string effectName)
    {
        GameObject.FindGameObjectsWithTag(effectName);
        for(int i=0; i< GameObject.FindGameObjectsWithTag(effectName).Length; i++)
        {
            if(GameObject.FindGameObjectsWithTag(effectName)[i].transform.IsChildOf(transform))
            {
                Destroy(GameObject.FindGameObjectsWithTag(effectName)[i]);
            }
        }
    }
}
