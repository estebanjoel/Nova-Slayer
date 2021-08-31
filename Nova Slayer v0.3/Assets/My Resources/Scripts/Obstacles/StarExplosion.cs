using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarExplosion : MonoBehaviour
{
    public Spawner explosionSpawner;
    public GameObject explosion;
    public AudioClip explosionClip;
    public float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        explosionSpawner.prefabToSpawn = explosion;
    }

    // Update is called once per frame
    void Update()
    {
        if(lifeTime <= 0)
        {
            if(!AudioManager.instance.sfxSources[7].isPlaying)
            {
                AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.sfxSources[7], explosionClip);
                AudioManager.instance.PlaySource(AudioManager.instance.sfxSources[7]);
            }
            explosionSpawner.prefabToSpawn.transform.position = transform.position;
            explosionSpawner.Create();
            Destroy(gameObject);
        }
        else
        {
            lifeTime -= Time.deltaTime;
        }
    }
}
