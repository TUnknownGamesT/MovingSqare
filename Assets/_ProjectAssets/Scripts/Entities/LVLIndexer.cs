using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVLIndexer : MonoBehaviour
{
    public static int currentLvlIndex;
    public static bool lvlCompleted;
    public LvlSettings[] levles;


    private void OnEnable()
    {
        SceneLoader.onSceneNewSceneLoad += ResetLvlCompleted;
        
        Timer.onCounterEnd += () =>
        {
            lvlCompleted = true;
        };
    }

    private void OnDisable()
    {
        SceneLoader.onSceneNewSceneLoad -= ResetLvlCompleted;
        Timer.onCounterEnd -= () =>
        {
            lvlCompleted = true;
        };
    }
    
    public void ResetLvlCompleted()
    {
        lvlCompleted = false;
    }

    public void LvlCompleted()
    {
        if (!lvlCompleted)
            return;
        int maxLvl =  PlayerPrefs.GetInt("MaxLvlReached", 0);
        if (maxLvl <= currentLvlIndex)
           PlayerPrefs.SetInt("MaxLvlReached", currentLvlIndex+1);
    }

    public void NextLevel()
    {
        currentLvlIndex++;
    }

    public LvlSettings GetCurrentLvlSettings()
    {
        return levles[currentLvlIndex];
    }
}
