using System;
using System.Collections;
using EasyTransition;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SceneLoader : MonoBehaviour
{
    public RawImage blackCircle;
    public GameObject tranzition;
    public TransitionSettings[] TransitionSettings;
    
    private bool isLoading;
    
    
    
    public void ReloadGameScene()
    {
        if (!isLoading)
        {
            isLoading = !isLoading;
            EasyTransition.TransitionManager.Instance()
                .Transition(SceneManager.GetActiveScene().buildIndex
                    ,TransitionSettings[Random.Range(0,TransitionSettings.Length)],0);
        }
    }

    public void LoadBossScene()
    {
        if (!isLoading)
        {
            isLoading = !isLoading;
            EasyTransition.TransitionManager.Instance()
                .Transition(2,TransitionSettings[Random.Range(0,TransitionSettings.Length)]
                    ,0);
        }
    }


    public  void LoadSurviveScene()
    {
        if (!isLoading)
        {
            isLoading = !isLoading;
            EasyTransition.TransitionManager.Instance()
                .Transition(1,TransitionSettings[Random.Range(0,TransitionSettings.Length)]
                    ,0);
        }
    }
    private void StartGame(int sceneIndex)
    {
        tranzition.SetActive(true);
        LeanTween.value(0, 1, 1f).setOnUpdate(value =>
        {
            blackCircle.color = new Vector4(0, 0, 0, value);
        }).setEaseInCubic().setOnComplete(() => SceneManager.LoadScene(sceneIndex)).setDelay(1f);
    }

    public  void LoadMainMenu()
    {
        if (!isLoading)
        {
            GameManager.instance.ResetAlreadyOver();
            GameManager.instance.ResetAd();
            isLoading = !isLoading;
            EasyTransition.TransitionManager.Instance()
                .Transition(0,TransitionSettings[Random.Range(0,TransitionSettings.Length)]
                    ,0);
        }
        
    }

    public void RandomTransition()
    {
        EasyTransition.TransitionManager.Instance()
            .Transition(TransitionSettings[Random.Range(0,TransitionSettings.Length)]
                ,0);
    }
}
