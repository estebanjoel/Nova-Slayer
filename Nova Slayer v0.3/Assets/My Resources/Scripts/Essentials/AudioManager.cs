    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource bgmSource, ambienceSource;
    public AudioSource[] sfxSources;
    // Start is called before the first frame update
    void Start()
    {
        #region Singleton
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        #endregion
    }

    // Update is called once per frame
    public void StopSource(AudioSource source)
    {
        source.Stop();
    }

    public void PlaySource(AudioSource source)
    {
        source.Play();
    }

    public void ChangeAudioClipFormSource(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
    }

    public void ActivateSourceLoop(AudioSource source)
    {
        source.loop = true;
    }

    public void DeactivateSourceLoop(AudioSource source)
    {
        source.loop = false;
    }
}
