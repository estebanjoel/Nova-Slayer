using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryPanelController : MonoBehaviour
{
    public Text enemiesKilledPointsText, timePassedText, lostLivesPointsText, totalPointsText;
    public Image secondaryWeaponIcon;
    public Sprite[] secondaryWeaponSprites;
    public Text secondaryWeaponText;
    public string[] secondaryWeaponNames;

    public void SetVictoryPanelElements()
    {
        enemiesKilledPointsText.text = GameManager.instance.scoreController.enemiesKilledPoints.ToString();
        int minutes = Mathf.FloorToInt(GameManager.instance.scoreController.timer / 60);
        int seconds = Mathf.FloorToInt(GameManager.instance.scoreController.timer - minutes);
        timePassedText.text = minutes + ":" + seconds;
        int lostLives = GameManager.instance.scoreController.livesLostPoints - 5000;
        if(lostLives < 0) lostLives /= -1000;
        lostLivesPointsText.text = (lostLives).ToString();
        totalPointsText.text = GameManager.instance.scoreController.totalPoints.ToString();
        SetSecondaryWeapon();
    }

    public void SetSecondaryWeapon()
    {
        string[] levels = GameManager.instance.levelManager.scenesToLoad;
        for(int i = 0; i < levels.Length; i++)
        {
            if(GameManager.instance.levelManager.currentScene == levels[i])
            {
                if(i < secondaryWeaponSprites.Length) secondaryWeaponIcon.sprite = secondaryWeaponSprites[i];
                if(i < secondaryWeaponSprites.Length) secondaryWeaponText.text = secondaryWeaponNames[i];
                break;
            }
        }
    }
}
