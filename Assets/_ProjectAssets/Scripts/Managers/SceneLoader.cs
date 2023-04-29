using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public RawImage blackCircle;
    public GameObject tranzition;

    private bool isLoading;

    private void Start()
    {
        //tranzition.SetActive(false);
    }
    
    public void ReloadGameScene()
    {
        if (!isLoading)
        {
            isLoading = !isLoading;
            tranzition.SetActive(true);
            LeanTween.value(0, 1, 2.5f).setOnUpdate(value =>
            {
                blackCircle.color = new Vector4(0, 0, 0, value);
            }).setEaseInCubic().setOnComplete(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            });
        }
    }

    public void LoadBossScene()
    {
        if (!isLoading)
        {
            isLoading = !isLoading;
            GameObject.Find("AnimationManager").GetComponent<MenuAnimation>().LeaveMenu();
            StartGame(2);
        }
    }


    public  void LoadSurviveScene()
    {
        if (!isLoading)
        {
            isLoading = !isLoading;
            GameObject.Find("AnimationManager").GetComponent<MenuAnimation>().LeaveMenu();
            StartGame(1);
        }
    }
    private void StartGame(int sceneIndex)
    {
        tranzition.SetActive(true);
        LeanTween.value(0, 1, 2.5f).setOnUpdate(value =>
        {
            blackCircle.color = new Vector4(0, 0, 0, value);
        }).setEaseInCubic().setOnComplete(() => SceneManager.LoadScene(sceneIndex)).setDelay(2f);
    }

    public  void LoadMainMenu()
    {
        if (!isLoading)
        {
            GameManager.instance.ResetAlreadyOver();
            GameManager.instance.ResetAD();
            isLoading = !isLoading;
            tranzition.SetActive(true);
            LeanTween.value(0, 1, 2.5f).setOnUpdate(value =>
            {
                blackCircle.color = new Vector4(0, 0, 0, value);
            }).setEaseInCubic().setOnComplete(() =>SceneManager.LoadScene(0));
        }
        
    }
}
