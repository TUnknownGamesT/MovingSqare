using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField, Range(0, 30)]
    private int currentLvl=1;
    private float lvlUpTimeDelay=15;
    public float lineTimeDelay=100, enemyTimeDelay=100, obstacleTimeDelay=100;
    public SpawnManager spawnManager;
    
    #region Singleton

    public static LevelManager instance;

    private void Awake()
    {
        enemyTimeDelay=3;
        instance = FindObjectOfType<LevelManager>();

        if (instance == null)
        {
            instance = this;
        }
        UpdateStats();
        StartCoroutine(LvlUp());
        Debug.Log("IM alive");
    }

    #endregion
    
    IEnumerator LvlUp(){
        yield return new WaitForSeconds(lvlUpTimeDelay); 
        Debug.Log("LvlUp");
        currentLvl++;
        UpdateStats();
        StartCoroutine(LvlUp());
    }

    void LvlReset(){
        currentLvl=0;
    }

    float GetCurrentLvl(){
        return currentLvl;
    }
    void UpdateStats(){
        if(currentLvl<10 || (currentLvl>20 && currentLvl%2==0)){
            UpdateEnemyDelay();
        }else{
            UpdateLineDelay();
        }
        //obstacleTimeDelay = lineTimeDelay-currentLvl;
    }
    void UpdateEnemyDelay(){
        enemyTimeDelay = enemyTimeDelay*0.85f;
    }
    void UpdateLineDelay(){
        if(lineTimeDelay==100){
            lineTimeDelay =5;
            spawnManager.linesSimultaneusly=2;
            StartCoroutine(spawnManager.SpawnLines());
        }
        if(currentLvl%2==0){
            spawnManager.linesSimultaneusly++;
        }
        lineTimeDelay = lineTimeDelay*0.85f;
    }
}
