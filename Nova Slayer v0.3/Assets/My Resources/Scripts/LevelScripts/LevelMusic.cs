using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    public AudioClip levelIntroBGM, levelBGM, levelAmbience, bossIntroBGM, bossBGM;

    public void SetLevelMusic()
    {
        if(levelIntroBGM != null) StartCoroutine(PlayBGMIntro(levelIntroBGM, levelBGM));
        else
        {
            AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.bgmSource, levelBGM);
            AudioManager.instance.PlaySource(AudioManager.instance.bgmSource);
        }
        AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.ambienceSource, levelAmbience);
        AudioManager.instance.PlaySource(AudioManager.instance.ambienceSource);
    }

    public void BossMusic()
    {
        if(levelIntroBGM != null) StopCoroutine(PlayBGMIntro(levelIntroBGM, levelBGM));
        if(bossIntroBGM != null) StartCoroutine(PlayBGMIntro(bossIntroBGM, bossBGM));
        else
        {
            AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.bgmSource, bossBGM);
            AudioManager.instance.PlaySource(AudioManager.instance.bgmSource);
        }
    }

    public IEnumerator PlayBGMIntro(AudioClip introClip, AudioClip clip)
    {
        AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.bgmSource, introClip);
        AudioManager.instance.PlaySource(AudioManager.instance.bgmSource);
        yield return new WaitForSeconds(AudioManager.instance.bgmSource.clip.length - 1f);
        Debug.Log("change clip");
        AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.bgmSource, clip);
        AudioManager.instance.PlaySource(AudioManager.instance.bgmSource);
    }
}
