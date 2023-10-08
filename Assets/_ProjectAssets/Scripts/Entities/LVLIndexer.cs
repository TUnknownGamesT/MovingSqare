using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVLIndexer : MonoBehaviour
{
    public static int currentLvlIndex;
    public LvlSettings[] levles;


    public LvlSettings GetCurrentLvlSettings()
    {
        return levles[currentLvlIndex];
    }
}
