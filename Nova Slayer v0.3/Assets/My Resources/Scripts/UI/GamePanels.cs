using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanels : MonoBehaviour
{
    public GameObject FadeScreen, PlayerHealthBar, LifeCounter, EnhancedIcon, PlasmaBombCounter, SecondaryWeaponBars, SecondaryWeaponsButtons, 
    BossHealthBars, VictoryPanel, GameOverPanel;

    [Header("Pause Menu Panels")]
    public GameObject pauseMenuPanel;
    public GameObject difficultyMenuPanel;
    public GameObject easyModePanel, mediumModePanel, hardModePanel;
    public GameObject retryPanel;
    public GameObject mainMenuPanel;
    public GameObject quitPanel;
    public GameObject levelSelectorPanel;
    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    
    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void ShowInitialPanels()
    {
        FadeScreen.SetActive(true);
        PlayerHealthBar.SetActive(true);
        LifeCounter.SetActive(true);
        PlasmaBombCounter.SetActive(true);
        SecondaryWeaponBars.SetActive(true);
        SecondaryWeaponsButtons.SetActive(true);
        BossHealthBars.SetActive(true);
    }

    public void HidePauseMenuPanels()
    {
        pauseMenuPanel.SetActive(false);
        difficultyMenuPanel.SetActive(false);
        retryPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        quitPanel.SetActive(false);
        easyModePanel.SetActive(false);
        mediumModePanel.SetActive(false);
        hardModePanel.SetActive(false);
        levelSelectorPanel.SetActive(false);
    }

    public void HideAllPanels()
    {
        FadeScreen.SetActive(false);
        PlayerHealthBar.SetActive(false);
        LifeCounter.SetActive(false);
        EnhancedIcon.SetActive(false);
        PlasmaBombCounter.SetActive(false);
        SecondaryWeaponBars.SetActive(false);
        SecondaryWeaponsButtons.SetActive(false);
        BossHealthBars.SetActive(false);
        VictoryPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        HidePauseMenuPanels();
    }
}
