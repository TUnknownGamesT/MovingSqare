using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVLIndexer : MonoBehaviour
{
    public static int currentLvlIndex;
    public LvlSettings[] levles;
    
    
    public void LvlCompleted()
    {
        PlayerPrefs.SetInt("MaxLvlReached", currentLvlIndex+1);
    }

    public void NextLevel()
    {
        LVLIndexer.currentLvlIndex++;
        Debug.Log("starting lvl "+ currentLvlIndex);
        SceneLoader.instance.LoadLvlScene();
    }

    public LvlSettings GetCurrentLvlSettings()
    {
        return levles[currentLvlIndex];
    }
}
