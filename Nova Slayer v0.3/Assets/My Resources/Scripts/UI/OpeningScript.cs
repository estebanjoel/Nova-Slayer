using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningScript : MonoBehaviour
{
    public UIFade uIFade;
    bool isOnOpening;
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public GameObject[] uiObjects;

    void Start()
    {
        StartCoroutine(PlayLogoSFXCo());
    }

    void Update()
    {
        if(Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel")) if(!isOnOpening) LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        isOnOpening = true;
        foreach(GameObject myObject in uiObjects)
        {
            myObject.SetActive(false);
        }
        StopAllCoroutines();
        StartCoroutine(LoadMainMenuCo());
    }

    public IEnumerator LoadMainMenuCo()
    {
        uIFade.FadeToBlack();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator PlayLogoSFXCo()
    {
        yield return new WaitForSeconds(bgmSource.clip.length-3);
        sfxSource.Play();
        yield return new WaitForSeconds(5f);
        LoadMainMenu();
    }
}
