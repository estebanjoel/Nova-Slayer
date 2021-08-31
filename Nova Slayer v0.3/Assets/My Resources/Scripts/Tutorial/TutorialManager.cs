using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public AudioClip tutorialMusic;
    public TutorialNovaSlayer tutorialNovaSlayer;
    public TutorialEnemySpawner tutorialEnemySpawner;
    public Animator tutorialTextAnimator;
    public bool canBeginFirstPart ,canBeginSecondPart, canBeginThirdPart, canBeginFourthPart, canBeginFifthPart, canBeginLastPart, hasGameStarted, isOnTutorialPlusMenu;
    public UIFade uIFade;
    public TutorialUIManager tutorialUIManager;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.bgmSource, tutorialMusic);
        AudioManager.instance.PlaySource(AudioManager.instance.bgmSource);
        tutorialTextAnimator.gameObject.SetActive(false);
        StartCoroutine(firstPartOfTheTutorial());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel") && !canBeginLastPart && !hasGameStarted && !isOnTutorialPlusMenu)
        {
            tutorialUIManager.ShowPanel(tutorialUIManager.areYouSurePanel);
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
            if(!hasGameStarted)
            {
                if(canBeginFirstPart)
                {
                    tutorialNovaSlayer.canCheckKeys = true;
                    tutorialNovaSlayer.canMove = true;
                    if(tutorialNovaSlayer.hasMoved)
                    {
                        canBeginFirstPart = false;
                        StartCoroutine(secondPartOfTheTutorial());
                    }
                }

                if(canBeginSecondPart)
                {
                    tutorialNovaSlayer.canFireMainBullet = true;
                    if(tutorialNovaSlayer.hasShot)
                    {
                        canBeginSecondPart = false;
                        StartCoroutine(thirdPartOfTheTutorial());
                    }
                }

                if(canBeginThirdPart)
                {
                    tutorialNovaSlayer.canFireMainBullet = true;
                    if(GameObject.FindObjectOfType<TutorialNovaCruiser>() == null)
                    {
                        canBeginThirdPart = false;
                        StartCoroutine(fourthPartOfTheTutorial());
                    }
                }

                if(canBeginFourthPart)
                {
                    tutorialNovaSlayer.canFireSecondaryBullet = true;
                    if(GameObject.FindObjectsOfType<TutorialNovaCruiser>().Length == 0)
                    {
                        canBeginFourthPart = false;
                        StartCoroutine(fifthPartOfTheTutorial());
                    }
                }
                if(canBeginFifthPart)
                {
                    tutorialNovaSlayer.canFireMainBullet = true;
                    tutorialNovaSlayer.canFirePlasmaBomb = true;
                    tutorialNovaSlayer.canFireSecondaryBullet = true;
                    if(GameObject.FindObjectsOfType<TutorialNovaCruiser>().Length == 0)
                    {
                        canBeginFifthPart = false;
                        StartCoroutine(lastPartOfTheTutorial());
                    }
                }

                if(canBeginLastPart)
                {
                    if(!isOnTutorialPlusMenu)
                    {
                        if(Input.GetButtonDown("Cancel"))
                        {
                            canBeginLastPart = false;
                            startGame();                        
                        }

                        if(Input.GetButtonDown("Submit"))
                        {
                            canBeginLastPart = false;
                            isOnTutorialPlusMenu = true;
                            tutorialUIManager.ShowPanel(tutorialUIManager.tutorialPlusPanel);
                        }
                    }
                }
            }
        }
    }

    public IEnumerator firstPartOfTheTutorial()
    {
        uIFade.fadeScreen.gameObject.SetActive(true);
        uIFade.FadeFromBlack();
        yield return new WaitForSeconds(uIFade.fadeSpeed);
        tutorialTextAnimator.gameObject.SetActive(true);
        yield return new WaitForSeconds(tutorialTextAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        canBeginFirstPart = true;        
    }
    public IEnumerator secondPartOfTheTutorial()
    {
        tutorialTextAnimator.SetTrigger("firstTrigger");
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(tutorialTextAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        canBeginSecondPart = true;        
    }
    public IEnumerator thirdPartOfTheTutorial()
    {
        tutorialTextAnimator.SetTrigger("secondTrigger");
        tutorialNovaSlayer.canFireMainBullet = false;
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(tutorialTextAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length/2);
        tutorialEnemySpawner.canSpawnFirstWave = true;
        yield return new WaitForSeconds(tutorialTextAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length/2);
        canBeginThirdPart = true;        
    }

    public IEnumerator fourthPartOfTheTutorial()
    {
        tutorialTextAnimator.SetTrigger("thirdTrigger");
        tutorialNovaSlayer.canFireMainBullet = false;
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(tutorialTextAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length/2);
        tutorialEnemySpawner.canSpawnSecondWave = true;
        yield return new WaitForSeconds(tutorialTextAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length/2);
        canBeginFourthPart = true;
    }

    public IEnumerator fifthPartOfTheTutorial()
    {
        tutorialTextAnimator.SetTrigger("fourthTrigger");
        tutorialNovaSlayer.canFireSecondaryBullet = false;
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(tutorialTextAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length/2);
        tutorialEnemySpawner.canSpawnThirdWave = true;
        yield return new WaitForSeconds(tutorialTextAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length/2);
        canBeginFifthPart = true;
    }

    public IEnumerator lastPartOfTheTutorial()
    {
        tutorialTextAnimator.SetTrigger("lastTrigger");
        tutorialNovaSlayer.canFireMainBullet = false;
        tutorialNovaSlayer.canFireSecondaryBullet = false;
        tutorialNovaSlayer.canFirePlasmaBomb = false;
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(tutorialTextAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        canBeginLastPart = true;
    }

    public void startGame()
    {
        Time.timeScale = 1f;
        hasGameStarted = true;
        StartCoroutine(startGameCo());
    }

    public IEnumerator startGameCo()
    {
        tutorialUIManager.HideAllPanels();
        AudioManager.instance.bgmSource.Stop();
        uIFade.FadeToBlack();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Lvl1");        
    }
}
