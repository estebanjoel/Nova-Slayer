using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour
{
    public static UICanvas instance;
    public UIFade uIFade;
    public GamePanels gamePanels;
    public LifeCounter lifeCounter;
    public PlasmaBombCounter plasmaBombCounter;
    public SecondaryWeaponsUI secondaryWeaponsUI;
    public BossHealthBars bossHealthBars;
    public PauseMenu pauseMenu;
    public Text godModeText;
    public Text difficultyText;
    public Animator getReadyAnimator;
    public Animator gameOverAnimator;
    public Animator victoryPanelAnimator;
    public GameObject victoryText;
    public UIAudio uIAudio;
    // Start is called before the first frame update
    void Start()
    {
        #region Singleton
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        #endregion
    }

    public void SetUIElements()
    {
        gamePanels.HideAllPanels();
        gamePanels.ShowInitialPanels();
        uIFade.FadeFromBlack();
        getReadyAnimator.Play("GetReady", 0, 0.25f);
        string currentPlayerLives = NovaSlayer.instance.body.lives.ToString();
        plasmaBombCounter.SetCounter(NovaSlayer.instance.body.plasmaBombAmmo);
        lifeCounter.ChangeLifeCounterText(currentPlayerLives);
        for(int i = 0; i < secondaryWeaponsUI.secondaryWeaponAmmo.Length; i++)
        {
            secondaryWeaponsUI.SetAmmoText(i, NovaSlayer.instance.body.secondaryBulletAmmo[i]);
        }
        bossHealthBars.HideHealthBars();
        victoryText.SetActive(false);
        SetGodModeUI();
    }

    public void SetGodModeUI()
    {
        if(GameManager.instance.isGodModeActive) godModeText.text = "God Mode: Activated";
        else godModeText.text = "God Mode: Deactivated";
    }

    public void SetDifficultyTextUI(int difficulty)
    {
        switch(difficulty)
        {
            case 0:
                difficultyText.text = "Difficulty: Easy";
                break;
            case 1:
                difficultyText.text = "Difficulty: Medium";
                break;
            case 2:
                difficultyText.text = "Difficulty: Hard";
                break;
        }
    }

    public IEnumerator victoryCo()
    {
        victoryText.SetActive(true);
        yield return new WaitForSeconds(3f);
        victoryText.SetActive(false);
        AudioManager.instance.bgmSource.Stop();
        AudioManager.instance.ambienceSource.Stop();
        gamePanels.ShowPanel(gamePanels.VictoryPanel);
        victoryPanelAnimator.GetComponent<VictoryPanelController>().SetVictoryPanelElements();
        victoryPanelAnimator.Play("VictoryPanel", 0, 0.25f);
    }

    public IEnumerator gameOverCo()
    {
        yield return new WaitForSeconds(0.5f);
        gamePanels.HideAllPanels();
        gamePanels.ShowPanel(gamePanels.GameOverPanel);
        gameOverAnimator.Play("GameOver", 0, 0.025f);
    }

    public void NextLevelButton()
    {
        GameManager.instance.levelManager.LoadNextLevel();
    }

    public void LoadLevel(int i)
    {
        GameManager.instance.levelManager.LoadLevel(i);
    }
}
