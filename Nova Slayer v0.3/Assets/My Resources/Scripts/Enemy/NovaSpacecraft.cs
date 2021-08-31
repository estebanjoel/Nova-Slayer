using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaSpacecraft : EnemyBody
{
    [SerializeField] GameObject[] enemiesToSpawn;
    [SerializeField] Spawner additionalFireSpawner;
    public AudioClip spawnClip;
    // Start is called before the first frame update
    void Start()
    {
        fireRate = Random.Range(6, 6.5f);
        enemiesToSpawn = GameObject.FindObjectOfType<EnemiesForSpacecraftToSpawn>().enemies;
        SetEnemyStats();
    }

    public override void FireBullet()
    {
        if(GameObject.FindGameObjectsWithTag("Spaceship").Length < 20)
        {
            if(!AudioManager.instance.sfxSources[6].isPlaying)
            {
                AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.sfxSources[6], spawnClip);
                AudioManager.instance.PlaySource(AudioManager.instance.sfxSources[6]);
            }
            fireSpawner.prefabToSpawn = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];
            fireSpawner.prefabToSpawn.GetComponent<EnemyBody>().xPosition = transform.position.x - 2;
            additionalFireSpawner.prefabToSpawn = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];
            additionalFireSpawner.prefabToSpawn.GetComponent<EnemyBody>().xPosition = transform.position.x - 2;
            fireSpawner.Create();
            additionalFireSpawner.Create();
        }
    }
}
