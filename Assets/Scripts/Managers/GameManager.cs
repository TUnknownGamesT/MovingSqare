using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        instance = FindObjectOfType<GameManager>();

        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion
    
    public UIManager uiManager;
    public SpawnManager spawnManager;
    public Vector2 PlayerPosition => player.position;
    private  Transform player;
    private bool alreadyOver;
    

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        StartCoroutine(InitGame());
    }

    IEnumerator InitGame()
    {
        yield return new WaitForSeconds(2f);
        spawnManager.enabled = true;
    }
    

    public void GameOver()
    {
        if (!alreadyOver)
        {
            uiManager.LoseState();
            alreadyOver = !alreadyOver;
        }
    }


    public static void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
    
    public void Resume()
    {
        Time.timeScale = 1;
    }

}
