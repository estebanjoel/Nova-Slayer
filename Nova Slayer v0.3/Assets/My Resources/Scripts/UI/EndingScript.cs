using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    public UIFade uIFade;
    public AudioSource endingSource;
    public GameObject endOfCredits;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel") && endOfCredits.activeInHierarchy)
        {
            StartCoroutine(ReturnToMainMenu());
        }
    }

    public IEnumerator ReturnToMainMenu()
    {
        endOfCredits.SetActive(false);
        endingSource.Stop();
        uIFade.FadeToBlack();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainMenu");
    }
}
