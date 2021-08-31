using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    public AudioClip moveButtonAudio;
    public AudioClip selectButtonAudio;
    public AudioClip cancelAudio;
    public AudioClip secondaryWeaponSelect;
    public AudioClip forbidden;
    public AudioClip returnAudio;
    public AudioClip godModeAudio;
    public AudioSource UISFXSource;
    void Start()
    {
        UISFXSource = AudioManager.instance.sfxSources[9];
    }

    public void PlayMoveButtonAudio()
    {
        if(!UISFXSource.isPlaying)
        {
            AudioManager.instance.ChangeAudioClipFormSource(UISFXSource, moveButtonAudio);
            AudioManager.instance.PlaySource(UISFXSource);
        }
    }
    
    public void PlaySelectButtonAudio()
    {
        if(!UISFXSource.isPlaying)
        {
            AudioManager.instance.ChangeAudioClipFormSource(UISFXSource, selectButtonAudio);
            AudioManager.instance.PlaySource(UISFXSource);
        }
    }

    public void PlayCancelAudio()
    {
        if(!UISFXSource.isPlaying)
        {
            AudioManager.instance.ChangeAudioClipFormSource(UISFXSource, cancelAudio);
            AudioManager.instance.PlaySource(UISFXSource);
        }
    }

    public void PlaySecondaryWeaponSelectAudio()
    {
        if(!UISFXSource.isPlaying)
        {
            AudioManager.instance.ChangeAudioClipFormSource(UISFXSource, secondaryWeaponSelect);
            AudioManager.instance.PlaySource(UISFXSource);
        }
        
    }

    public void PlayForbiddenAudio()
    {
        if(!UISFXSource.isPlaying)
        {
            AudioManager.instance.ChangeAudioClipFormSource(UISFXSource, forbidden);
            AudioManager.instance.PlaySource(UISFXSource);
        }

    }

    public void ReturnAudio()
    {
        if(!UISFXSource.isPlaying)
        {
            AudioManager.instance.ChangeAudioClipFormSource(UISFXSource, returnAudio);
            AudioManager.instance.PlaySource(UISFXSource);
        }
    }
    public void PlayGodModeAudio()
    {
        if(!UISFXSource.isPlaying)
        {
            AudioManager.instance.ChangeAudioClipFormSource(UISFXSource, godModeAudio);
            AudioManager.instance.PlaySource(UISFXSource);
        }
    }
}
