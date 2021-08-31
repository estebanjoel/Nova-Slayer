using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int enemiesKilledPoints;
    public float timer;
    public bool canCheckTimer;
    public int timePassedPoints;
    public float[] estimatedTimes;
    public int[] estimatedTimesPoints;
    public int livesLostPoints;
    public bool perfectlivesScore;
    public int totalPoints;
    public int previousPoints;
    // Start is called before the first frame update
    void Start()
    {
        ActivateTimer();
        livesLostPoints = 5000;
    }

    void Update()
    {
        if(canCheckTimer) timer += Time.deltaTime;    
    }

    public void ActivateTimer()
    {
        canCheckTimer = true;
    }

    public void DeactivateTimer()
    {
        canCheckTimer = false;
    }

    public void RestartValues()
    {
        ActivateTimer();
        enemiesKilledPoints = 0;
        timer = 0;
        livesLostPoints = 5000;
        perfectlivesScore = false;
        if(previousPoints > 0) totalPoints = previousPoints;
    }

    public void SetLevelEstimatedTimes()
    {
        float[] levelTimes = GameObject.FindObjectOfType<LevelEstimatedTimes>().estimatedTimes;
        for(int i = 0; i < estimatedTimes.Length; i++)
        {
            estimatedTimes[i] = levelTimes[i];
        }
    }

    public void CheckEstimatedTime()
    {
        if(timer <= estimatedTimes[0])
        {
            timePassedPoints = estimatedTimesPoints[0];
        }
        else
        {
            for(int i = 1; i < estimatedTimes.Length; i++)
            {
                if(timer > estimatedTimes[i-1] && timer <= estimatedTimes[i])
                {
                    timePassedPoints = estimatedTimesPoints[i];
                    break;
                }
            }
        }
    }

    public void AddKillPoints()
    {
        enemiesKilledPoints++;
    }

    public void SubstractLivesPoints()
    {
        if(livesLostPoints>0) livesLostPoints-=1000;
    }

    public void SetTotalScore()
    {
        DeactivateTimer();
        previousPoints = totalPoints;
        CheckEstimatedTime();
        if(livesLostPoints == 5000)
        {
          livesLostPoints += (livesLostPoints/2);
          perfectlivesScore = true;  
        } 
        totalPoints += enemiesKilledPoints * 100 + timePassedPoints + livesLostPoints;
    }
}
