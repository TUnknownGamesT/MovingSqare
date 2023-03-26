using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public RectTransform blackCircle;

    private bool isLoading;

    private void Start()
    {
        LeanTween.value(30, 0, 2f).setOnUpdate(value =>
        {
            blackCircle.localScale = Vector3.one * value;
        }).setEaseInQuad();
    }


    public void ReloadGameScene()
    {
        if (!isLoading)
        {
            isLoading = !isLoading;
            LeanTween.value(0, 30, 2.5f).setOnUpdate(value =>
            {
                blackCircle.localScale = Vector3.one * value;
            }).setEaseInCubic().setOnComplete(() =>
            {
                SceneManager.UnloadSceneAsync(1);
                SceneManager.LoadScene(1,LoadSceneMode.Single);
            });
        }
       
    }

    public  void LoadGameRoom()
    {
        if (!isLoading)
        {
            isLoading = !isLoading;
            GameObject.Find("AnimationManager").GetComponent<MenuAnimation>().LeaveMenu();
            StartCoroutine(StartGame());
        }
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        LeanTween.value(0, 30, 2.5f).setOnUpdate(value =>
        {
            blackCircle.localScale = Vector3.one * value;
        }).setEaseInCubic().setOnComplete(() => SceneManager.LoadScene(1));
    }

    public  void LoadMainMenu()
    {
        
        if (!isLoading)
        {
            GameManager.instance.ResetAlreadyOver();
            isLoading = !isLoading;
            LeanTween.value(0, 30, 2.5f).setOnUpdate(value =>
            {
                blackCircle.localScale = Vector3.one * value;
            }).setEaseInCubic().setOnComplete(() =>SceneManager.LoadScene(0));
        }
        
    }
}
