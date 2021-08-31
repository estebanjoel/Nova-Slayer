using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGodModeActive;
    public ItemSpawner itemSpawner, ammoSpawner;
    public ObstacleSpawner asteroidSpawner, spaceRockSpawner;
    public LevelManager levelManager;
    public TopLevelInfo topLevelInfo;
    public ScoreController scoreController;
    public Transform playerStartPosition;
    public bool victoryCondition, failCondition;
    public int currentEnemyWave;
    public EnemySpawner enemySpawner;
    public int bossCount;
    public LevelMusic levelMusic;
    bool canChangeMusic;
    public AudioClip victoryClip, gameOverClip;
    public int currentDifficulty;
    public LevelSettings levelSettings;
    
    // Start is called before the first frame update
    void Start()
    {
        #region Singleton
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        #endregion
        levelManager.SetLevelElements();
        currentDifficulty = -1;
    }

    // Update is called once per frame
    void Update()
    {
        levelManager.CheckCurrentScene();
        if(levelManager.CheckIfSceneIsGameplay())
        {
            if(!UICanvas.instance.gamePanels.pauseMenuPanel.activeInHierarchy)
            {
                if(Input.GetButtonDown("Cancel"))
                {
                    UICanvas.instance.pauseMenu.Pause();
                    UICanvas.instance.gamePanels.ShowPanel(UICanvas.instance.gamePanels.pauseMenuPanel);
                }
                if(CheckPlayerLives())
                {
                    if(enemySpawner.canCheckBoss)
                    {
                        if(canChangeMusic)
                        {
                            levelMusic.BossMusic();
                            canChangeMusic = false;
                        }
                        if(CheckIfBossIsDead()) if(!victoryCondition)
                        {
                            StopAllCoroutines();
                            StartCoroutine(PlayVictoryClip());
                        }  
                    }
                }
                else
                {
                    if(!failCondition)
                    {
                        StopAllCoroutines();
                        StartCoroutine(PlayGameOverClip());
                    } 
                }
            }

            else
            {
                if(Input.GetButtonDown("Cancel"))
                {
                    UICanvas.instance.pauseMenu.Resume();
                    UICanvas.instance.gamePanels.HidePanel(UICanvas.instance.gamePanels.pauseMenuPanel);
                }
                if(Input.GetKeyDown(KeyCode.F) && !UICanvas.instance.gamePanels.levelSelectorPanel.activeInHierarchy)
                {
                    UICanvas.instance.gamePanels.ShowPanel(UICanvas.instance.gamePanels.levelSelectorPanel);
                }
                if(Input.GetKeyDown(KeyCode.G))
                {
                    UICanvas.instance.uIAudio.PlayGodModeAudio();
                    if(isGodModeActive) isGodModeActive = false;
                    else isGodModeActive = true;
                    UICanvas.instance.SetGodModeUI();
                }

                if(!CheckCurrentDifficulty())
                {
                   SetCurrentDifficultySettings();
                }
            }
        }
        else
        {
            DestroyAllInstances();
        }
    }

    public void SetElements()
    {
        playerStartPosition = GameObject.Find("startPosition").transform;
        levelMusic = GameObject.FindObjectOfType<LevelMusic>();
        levelMusic.SetLevelMusic();
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        enemySpawner.canCheckBoss = false;
        enemySpawner.actualWave = 0;
        canChangeMusic = true;
        topLevelInfo.SetBeginningPlayerLives(NovaSlayer.instance.body.lives);
        topLevelInfo.SetBeginningSecondaryAmmo(NovaSlayer.instance.body.secondaryBulletAmmo);
        topLevelInfo.SetBeginningPlasmaBombAmmo(NovaSlayer.instance.body.plasmaBombAmmo);
        NovaSlayer.instance.transform.position = playerStartPosition.position;
        levelSettings = GameObject.FindObjectOfType<LevelSettings>();
        SetCurrentDifficultySettings();
        SetInitialSecondaryBulletAmmo();
        switch(currentDifficulty)
        {
            case 0:
                NovaSlayer.instance.body.shockRate = 1f;
                break;
            case 1:
                NovaSlayer.instance.body.shockRate = 1.5f;
                NovaSlayer.instance.body.speed -= 1f;
                NovaSlayer.instance.body.fireRate += 0.1f;
                break;
            case 2:
                NovaSlayer.instance.body.shockRate = 1.75f;
                NovaSlayer.instance.body.speed -= 2f;
                NovaSlayer.instance.body.fireRate += 0.2f;
                break;
        }
        if(currentDifficulty <= 0)
        {
            ActivateSecondaryWeapon(4);
        }
        else
        {
            for(int i = 1 ; i < UICanvas.instance.secondaryWeaponsUI.secondaryWeaponButton.Length; i++)
            {
                UICanvas.instance.secondaryWeaponsUI.HideButton(i);
                NovaSlayer.instance.body.secondaryBulletOnInventory[i] = false;
            }
            switch(levelManager.currentScene)
            {
                case "Lvl1":
                    ActivateSecondaryWeapon(1);
                    break;
                case "Lvl2":
                    ActivateSecondaryWeapon(2);
                    break;
                case "Lvl3":
                    ActivateSecondaryWeapon(3);
                    break;
                case "Lvl4":
                    ActivateSecondaryWeapon(4);
                    break;
                default:
                    ActivateSecondaryWeapon(4);
                    break;
            }
        }
        victoryCondition = false;
        failCondition = false;
    }

    public void SetInitialSecondaryBulletAmmo()
    {
        for(int i = 0; i < NovaSlayer.instance.body.secondaryBulletAmmo.Length; i++)
        {
            switch(currentDifficulty)
            {
                case 0:
                    NovaSlayer.instance.body.secondaryBulletAmmo[i] = 7;
                    break;
                case 1:
                    NovaSlayer.instance.body.secondaryBulletAmmo[i] = 5;
                    break;
                case 2:
                    NovaSlayer.instance.body.secondaryBulletAmmo[i] = 3;
                    break;
            }
            UICanvas.instance.secondaryWeaponsUI.SetAmmoText(i, NovaSlayer.instance.body.secondaryBulletAmmo[i]);
        }
    }

    public void ActivateSecondaryWeapon(int count)
    {
        for(int i = 0; i < count; i++)
        {
            NovaSlayer.instance.body.AddSecondaryBulletToInventory(i);
            UICanvas.instance.secondaryWeaponsUI.ShowButton(i);
        }
    }

    public bool CheckCurrentDifficulty()
    {
        if(currentDifficulty == DifficultyManager.instance.GetCurrentDifficultyMode()) return true;
        else return false;
    }

    public void DestroyAllInstances()
    {
        Destroy(UICanvas.instance.gameObject);
        Destroy(AudioManager.instance.gameObject);
        Destroy(NovaSlayer.instance.gameObject);
        Destroy(gameObject);
    }

    public bool CheckIfBossIsDead()
    {
        if(enemySpawner.CheckForBoss()) return false;
        return true;
    }

    public bool CheckPlayerLives()
    {
        if(NovaSlayer.instance.body.lives > 0) return true;
        else return false;
    }

    public void SetEasySettings()
    {
        levelSettings.SetEasyLevelSettings();
    }

    public void SetMediumSettings()
    {
        levelSettings.SetMediumLevelSettings();
    }

    public void SetHardSettings()
    {
        levelSettings.SetHardLevelSettings();
    }

    public void SetCurrentDifficultySettings()
    {
        currentDifficulty = DifficultyManager.instance.GetCurrentDifficultyMode();
        switch(currentDifficulty)
        {
            case 0:
                SetEasySettings();
                break;
            case 1:
                SetMediumSettings();
                break;
            case 2:
                SetHardSettings();
                break;
            default:
                break;
        }
        UICanvas.instance.SetDifficultyTextUI(currentDifficulty);
    }

    public IEnumerator PlayVictoryClip()
    {
        NovaSlayer.instance.brain.isGameActive = false;
        victoryCondition = true;
        yield return new WaitForSeconds(1f);
        AudioManager.instance.DeactivateSourceLoop(AudioManager.instance.bgmSource);
        AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.bgmSource, victoryClip);
        AudioManager.instance.PlaySource(AudioManager.instance.bgmSource);
        scoreController.SetTotalScore();
        StartCoroutine(UICanvas.instance.victoryCo());
    }
    public IEnumerator PlayGameOverClip()
    {
        failCondition = true;
        yield return new WaitForSeconds(1f);
        AudioManager.instance.ambienceSource.Stop();
        AudioManager.instance.DeactivateSourceLoop(AudioManager.instance.bgmSource);
        AudioManager.instance.ChangeAudioClipFormSource(AudioManager.instance.bgmSource, gameOverClip);
        AudioManager.instance.PlaySource(AudioManager.instance.bgmSource);
        StartCoroutine(UICanvas.instance.gameOverCo());
    }

    
}
