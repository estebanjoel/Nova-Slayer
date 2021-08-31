using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public UIFade uIFade;
    [Header("Menu Panels")]
    public GameObject PressStartText;
    public GameObject TitlePanel;
    public GameObject FooterPanel;
    public GameObject MenuPanel;
    public GameObject StartPanel;
    public GameObject DifficultyPanel;
    public GameObject OptionsPanel;
    bool hasMainMenuOpened;
    GameObject currentPanel, previousPanel;
    [SerializeField] string sceneToLoad;
    
    // Start is called before the first frame update
    void Start()
    {
        MenuPanel.SetActive(false);
        StartPanel.SetActive(false);
        OptionsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit") && !hasMainMenuOpened) OpenMainMenu();
        if(Input.GetButtonDown("Cancel")) ReturnToPreviousPanel();
    }

    public void OpenMainMenu()
    {
        PressStartText.SetActive(false);
        MenuPanel.SetActive(true);
        hasMainMenuOpened = true;
        currentPanel = MenuPanel;
    }

    public void HideAllPanels()
    {
        PressStartText.SetActive(false);
        TitlePanel.SetActive(false);
        FooterPanel.SetActive(false);
        MenuPanel.SetActive(false);
        StartPanel.SetActive(false);
        DifficultyPanel.SetActive(false);
        OptionsPanel.SetActive(false);
    }

    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
        if(currentPanel!=null)
        {
            previousPanel = panel.transform.parent.gameObject;
        }
        currentPanel = panel;
    }

    public void ReturnToPreviousPanel()
    {
        if(previousPanel!=null && currentPanel != MenuPanel)
        {
            GameObject.FindObjectOfType<MainMenuSFX>().PlaySFX(2);
            currentPanel.SetActive(false);
            previousPanel.SetActive(true);
            currentPanel = previousPanel;
            if(currentPanel.transform.parent != null) previousPanel = currentPanel.transform.parent.gameObject;
            else previousPanel = null;
        }
    }

    public void StopMusic()
    {
        GameObject.Find("MenuMusic").GetComponent<AudioSource>().Stop();
    }

    public void NewGame()
    {
        StartCoroutine(StartGame());
    }

    public void Continue()
    {
        StartCoroutine(StartGame());
    }

    public void EasyMode()
    {
        DifficultyManager.instance.SetEasyMode();
    }
    public void MediumMode()
    {
        DifficultyManager.instance.SetMediumMode();
    }
    public void HardMode()
    {
        DifficultyManager.instance.SetHardMode();
    }

    public IEnumerator StartGame()
    {
        HideAllPanels();
        uIFade.FadeToBlack();
        StopMusic();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
        StartCoroutine(Quit());
    }

    public IEnumerator Quit()
    {
        HideAllPanels();
        uIFade.FadeToBlack();
        StopMusic();
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
    }
}
