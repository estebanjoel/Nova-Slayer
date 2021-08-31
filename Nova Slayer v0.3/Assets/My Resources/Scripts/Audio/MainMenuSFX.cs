using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSFX : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioClip[] sfxClips;
    
    // Start is called before the first frame update
    void Start()
    {
        sfxSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(int audio)
    {
        if(!sfxSource.isPlaying)
        {
            sfxSource.clip = sfxClips[audio];
            sfxSource.Play();
        }
    }
}
