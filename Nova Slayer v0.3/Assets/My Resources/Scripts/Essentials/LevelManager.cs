using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string[] scenesToLoad;
    public string currentScene;
    
    // Start is called before the first frame update
    void Start()
    {
        SetLevelElements();

    }

    public bool CheckIfSceneIsGameplay()
    {
        for(int i = 0; i<scenesToLoad.Length-1; i++)
        {
            if(scenesToLoad[i] == currentScene) return true;
        }
        return false;
    }

    public void CheckCurrentScene()
    {
        if(currentScene != SceneManager.GetActiveScene().name) currentScene = SceneManager.GetActiveScene().name;
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelCo());
    }

    public IEnumerator LoadNextLevelCo()
    {
        UICanvas.instance.gamePanels.HideAllPanels();
        UICanvas.instance.gamePanels.ShowPanel(UICanvas.instance.gamePanels.FadeScreen);
        UICanvas.instance.uIFade.FadeToBlack();
        NovaSlayer.instance.UpgradeStats();
        yield return new WaitForSeconds(1.5f);
        string nextLevel = "";
        for(int i = 0; i<scenesToLoad.Length; i++)
        {
            if(scenesToLoad[i] == currentScene)
            {
                nextLevel = scenesToLoad[i+1];
                SetNewSecondaryWeaponOnInventory(i+1);
                break;    
            } 
        }
        SceneManager.LoadScene(nextLevel);
        SetLevelElements();
    }

    public void LoadLevel(int i)
    {
        StartCoroutine(LoadLevelCo(i));
    }

    public IEnumerator LoadLevelCo(int i)
    {
        UICanvas.instance.gamePanels.HideAllPanels();
        UICanvas.instance.gamePanels.ShowPanel(UICanvas.instance.gamePanels.FadeScreen);
        UICanvas.instance.uIFade.FadeToBlack();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scenesToLoad[i]);
        SetLevelElements();
    }

    public void SetLevelElements()
    {
        NovaSlayer.instance.brain.isGameActive = true;
        currentScene = SceneManager.GetActiveScene().name;
        AudioManager.instance.ActivateSourceLoop(AudioManager.instance.bgmSource);
        GameManager.instance.SetElements();
        GameManager.instance.scoreController.SetLevelEstimatedTimes();
        GameManager.instance.scoreController.RestartValues();
        NovaSlayer.instance.body.SetInitialElements();
        UICanvas.instance.SetUIElements();
    }

    public void SetNewSecondaryWeaponOnInventory(int weapon)
    {
        if(GameManager.instance.currentDifficulty == 0)
        {
            for(int i = 0; i < NovaSlayer.instance.body.secondaryBulletOnInventory.Length; i++)
            {
                NovaSlayer.instance.body.AddSecondaryBulletToInventory(i);
                UICanvas.instance.secondaryWeaponsUI.ShowButton(i);
            }
        }
        else
        {
            if(weapon < NovaSlayer.instance.body.secondaryBulletOnInventory.Length)
            {
                NovaSlayer.instance.body.AddSecondaryBulletToInventory(weapon);
                UICanvas.instance.secondaryWeaponsUI.ShowButton(weapon);
            } 
        }
    }

    public void RetryLevel()
    {
        AudioManager.instance.bgmSource.Stop();
        AudioManager.instance.ambienceSource.Stop();
        StartCoroutine(RetryLevelCo());
    }

    public IEnumerator RetryLevelCo()
    {
        UICanvas.instance.gamePanels.HideAllPanels();
        UICanvas.instance.gamePanels.ShowPanel(UICanvas.instance.gamePanels.FadeScreen);
        UICanvas.instance.uIFade.FadeToBlack();
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.enemySpawner.canCheckBoss = false;
        GameManager.instance.enemySpawner.actualWave = 0; 
        for(int i = 0; i < GameObject.FindGameObjectsWithTag("Spaceship").Length; i++)
        {
            Destroy(GameObject.FindGameObjectsWithTag("Spaceship")[i].gameObject);
        }
        if(GameObject.FindGameObjectsWithTag("Boss").Length > 0)
        {
            for(int i = 0; i < GameObject.FindGameObjectsWithTag("Boss").Length; i++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("Boss")[i].gameObject);
            }
        }
        if(GameObject.FindGameObjectsWithTag("Asteroid").Length > 0)
        {
            for(int i = 0; i < GameObject.FindGameObjectsWithTag("Boss").Length; i++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("Boss")[i].gameObject);
            }
        }
        if(GameObject.FindGameObjectsWithTag("SpaceRock").Length > 0)
        {
            for(int i = 0; i < GameObject.FindGameObjectsWithTag("SpaceRock").Length; i++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("SpaceRock")[i].gameObject);
            }
        }
        if(GameObject.FindObjectsOfType<Bullet>().Length > 0)
        {
            for(int i = 0; i < GameObject.FindObjectsOfType<Bullet>().Length; i++)
            {
                Destroy(GameObject.FindObjectsOfType<Bullet>()[i].gameObject);
            }
        }
        if(GameObject.FindObjectsOfType<StarExplosion>().Length > 0)
        {
            for(int i = 0; i < GameObject.FindObjectsOfType<StarExplosion>().Length; i++)
            {
                Destroy(GameObject.FindObjectsOfType<StarExplosion>()[i].gameObject);
            }
        }
        if(GameObject.FindObjectsOfType<PlasmaExplosion>().Length > 0)
        {
            for(int i = 0; i < GameObject.FindObjectsOfType<PlasmaExplosion>().Length; i++)
            {
                Destroy(GameObject.FindObjectsOfType<PlasmaExplosion>()[i].gameObject);
            }
        }
        if(GameObject.FindObjectsOfType<GravityField>().Length > 0)
        {
            for(int i = 0; i < GameObject.FindObjectsOfType<GravityField>().Length; i++)
            {
                Destroy(GameObject.FindObjectsOfType<GravityField>()[i].gameObject);
            }
        }
        if(GameObject.FindObjectsOfType<MagneticSpawner>().Length > 0)
        {
            for(int i = 0; i < GameObject.FindObjectsOfType<MagneticSpawner>().Length; i++)
            {
                GameObject.FindObjectsOfType<MagneticSpawner>()[i].remainingTimeToSpawn = GameObject.FindObjectsOfType<MagneticSpawner>()[i].spawnRate;
            }
        }
        if(GameObject.FindObjectsOfType<ObstacleSpawner>().Length > 0)
        {
            for(int i = 0; i < GameObject.FindObjectsOfType<ObstacleSpawner>().Length; i++)
            {
                GameObject.FindObjectsOfType<ObstacleSpawner>()[i].instancesToSpawn = 0;
                GameObject.FindObjectsOfType<ObstacleSpawner>()[i].AssignTimeToRespawn();
            }
        }
        NovaSlayer.instance.body.lives = GameManager.instance.topLevelInfo.GetBeginningPlayerLives();
        NovaSlayer.instance.body.secondaryBulletAmmo = GameManager.instance.topLevelInfo.GetBeginningSecondaryAmmo();
        NovaSlayer.instance.body.plasmaBombAmmo = GameManager.instance.topLevelInfo.GetBeginningPlasmaBombAmmo();
        SetLevelElements();
        yield return new WaitForSeconds(1f);
        NovaSlayer.instance.brain.CheckDeadBools();
    }

    public void ReturnToMainMenu()
    {
        AudioManager.instance.bgmSource.Stop();
        AudioManager.instance.ambienceSource.Stop();
        StartCoroutine(ReturnToMainMenuCo());
    }

    public IEnumerator ReturnToMainMenuCo()
    {
        UICanvas.instance.gamePanels.HideAllPanels();
        UICanvas.instance.gamePanels.ShowPanel(UICanvas.instance.gamePanels.FadeScreen);
        UICanvas.instance.uIFade.FadeToBlack();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        AudioManager.instance.bgmSource.Stop();
        AudioManager.instance.ambienceSource.Stop();
        StartCoroutine(QuitGameCo());
    }

    public IEnumerator QuitGameCo()
    {
        UICanvas.instance.gamePanels.HideAllPanels();
        UICanvas.instance.gamePanels.ShowPanel(UICanvas.instance.gamePanels.FadeScreen);
        UICanvas.instance.uIFade.FadeToBlack();
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
    }
}
