using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void RetryLevel()
    {
        GameManager.instance.levelManager.RetryLevel();
    }

    public void MainMenu()
    {
        GameManager.instance.levelManager.ReturnToMainMenu();
    }

    public void QuitGame()
    {
        GameManager.instance.levelManager.QuitGame();
    }

    public void EasyMode()
    {
        DifficultyManager.instance.SetEasyMode();
        Debug.Log(DifficultyManager.instance.GetCurrentDifficultyMode());
    }

    public void MediumMode()
    {
        DifficultyManager.instance.SetMediumMode();
        Debug.Log(DifficultyManager.instance.GetCurrentDifficultyMode());
    }

    public void HardMode()
    {
        DifficultyManager.instance.SetHardMode();
        Debug.Log(DifficultyManager.instance.GetCurrentDifficultyMode());
    }
}
