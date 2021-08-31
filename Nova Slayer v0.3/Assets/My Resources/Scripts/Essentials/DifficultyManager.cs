using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager instance;
    public bool easyMode, mediumMode, hardMode;
    // Start is called before the first frame update
    void Start()
    {
        #region Singleton
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        #endregion
        SetEasyMode();
    }

    public void SetEasyMode()
    {
        easyMode = true;
        mediumMode = false;
        hardMode = false;
    }

    public void SetMediumMode()
    {
        easyMode = false;
        mediumMode = true;
        hardMode = false;
    }

    public void SetHardMode()
    {
        easyMode = false;
        mediumMode = false;
        hardMode = true;
    }

    public int GetCurrentDifficultyMode()
    {
        if(easyMode) return 0;
        else if(mediumMode) return 1;
        else if(hardMode) return 2;
        else return -1;
    }
    
}
