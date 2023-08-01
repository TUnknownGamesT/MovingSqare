using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DificultyManager : MonoBehaviour
{
    [SerializeField, Range(0, 30)]
    private int currentLvl=1;
    private float lvlUpTimeDelay=15;
    public float lineTimeDelay=100, enemyTimeDelay=100, obstacleTimeDelay=100;
    public SpawnManager spawnManager;
    
    #region Singleton

    public static DificultyManager instance;

    private void Awake()
    {
        enemyTimeDelay=3;
        instance = FindObjectOfType<DificultyManager>();

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
        if (currentLvl>30){
            UpdateObstacleDelay();
        }
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
    void UpdateObstacleDelay(){
        if(obstacleTimeDelay==100){
            obstacleTimeDelay = 30;
            StartCoroutine(spawnManager.SpawnObstacle());
        }

    }
}
