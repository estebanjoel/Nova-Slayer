using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaShield : EnemyBody
{
    public AudioClip shieldClip;
    // Start is called before the first frame update
    void Start()
    {
        fireRate = Random.Range(60f, 65f);
        SetEnemyStats();
        fireSpawner.prefabToSpawn.layer = 10;
        FireBullet();
        if(health <=0)
        {
            if(AudioManager.instance.sfxSources[6].isPlaying) AudioManager.instance.sfxSources[6].Stop();
        }
    }

    public override void FireBullet()
    {
        AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.sfxSources[6], shieldClip);
        AudioManager.instance.PlaySource(AudioManager.instance.sfxSources[6]);
        fireSpawner.Create();
    }
}
