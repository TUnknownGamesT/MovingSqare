using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)]
    private float currentLvl=10;
    private float lvlUpTimeDelay=5;
    public float lineTimeDelay=100, enemyTimeDelay=100, obstacleTimeDelay=100;

    #region Singleton

    public static LevelManager instance;

    private void Awake()
    {
        enemyTimeDelay=7;
        instance = FindObjectOfType<LevelManager>();

        if (instance == null)
        {
            instance = this;
        }
        UpdateStats();
        StartCoroutine(LvlUp());
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LvlUp(){
        yield return new WaitForSeconds(lvlUpTimeDelay); 
        Debug.Log("LvlUp");
        currentLvl++;
        StartCoroutine(LvlUp());
    }

    void LvlReset(){
        currentLvl=0;
    }

    float GetCurrentLvl(){
        return currentLvl;
    }
    void UpdateStats(){
        lineTimeDelay = lineTimeDelay-currentLvl;
        enemyTimeDelay = enemyTimeDelay-currentLvl;
        obstacleTimeDelay = lineTimeDelay-currentLvl;
    }
}
