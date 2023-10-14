using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVLIndexer : MonoBehaviour
{
    public static int currentLvlIndex=19;
    public LvlSettings[] levles;
    
    
    public void LvlCompleted()
    {
        PlayerPrefs.SetInt("MaxLvlReached", currentLvlIndex+1);
    }
    

    public LvlSettings GetCurrentLvlSettings()
    {
        return levles[currentLvlIndex];
    }
}
