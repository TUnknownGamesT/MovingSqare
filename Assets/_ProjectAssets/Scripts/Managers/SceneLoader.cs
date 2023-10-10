using System;
using System.Collections;
using EasyTransition;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SceneLoader : MonoBehaviour
{
    
    #region Singleton
    
    public static SceneLoader instance;
    
    private void Awake()
    {
        instance = FindObjectOfType<SceneLoader>();
        if (instance == null)
        {
            instance = this;
        }
    }
    
    #endregion
    
    public TransitionSettings[] TransitionSettings;
    private bool isLoading;
    
    
    public void ReloadGameScene()
    {
        if (!isLoading)
        {
            isLoading = !isLoading;
            EasyTransition.TransitionManager.Instance()
                .Transition(SceneManager.GetActiveScene().buildIndex
                    ,TransitionSettings[Random.Range(0,TransitionSettings.Length-1)],0);
        }
    }

    //Change With Load LvlMenu
    public void LoadBossScene()
    {
        if (!isLoading)
        {
            isLoading = !isLoading;
            EasyTransition.TransitionManager.Instance()
                .Transition(2,TransitionSettings[Random.Range(0,TransitionSettings.Length-1)]
                    ,0);
        }
    }


    public  void LoadSurviveScene()
    {
        if (!isLoading)
        {
            isLoading = !isLoading;
            EasyTransition.TransitionManager.Instance()
                .Transition(1,TransitionSettings[Random.Range(0,TransitionSettings.Length-1)]
                    ,0);
        }
    }

    public  void LoadMainMenu()
    {
        if (!isLoading)
        {
            GameManager.instance.ResetAlreadyOver();
            GameManager.instance.ResetAd();
            isLoading = !isLoading;
            EasyTransition.TransitionManager.Instance()
                .Transition(0,TransitionSettings[Random.Range(0,TransitionSettings.Length-1)]
                    ,0);
        }
        
    }

    public void LoadLvlScene()
    {
        if (!isLoading)
        {
            isLoading = !isLoading;
            EasyTransition.TransitionManager.Instance()
                .Transition(3,TransitionSettings[Random.Range(0,TransitionSettings.Length-1)]
                    ,0);
        }
    }

    public void RandomTransition()
    {
        EasyTransition.TransitionManager.Instance()
            .Transition(TransitionSettings[Random.Range(0,TransitionSettings.Length-1)]
                ,0);
    }
}
