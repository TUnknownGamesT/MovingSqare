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
    public DataManager dataManager;
    public SkinPool skins;

    private  Transform player;
    private bool alreadyOver;
    private static bool askedAd;

    private void Start()
    {

        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        
        StartCoroutine(InitGame());
        
        player.GetComponent<SpriteRenderer>().sprite = skins.skins[PlayerPrefs.GetInt("currentSkin")].sprite;
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
            SoundManager.instance.PlayerDeathSound();
            if (!askedAd)
            {
                uiManager.FadeInFadeOutJoystick();
                uiManager.AdState();
                askedAd = !askedAd;
            }
            else
            {
                uiManager.LoseState();
                uiManager.FadeInFadeOutJoystick();
            }
            alreadyOver = !alreadyOver;
        }
    }

    public void ResetAlreadyOver()
    {
        askedAd = false;
    }


    public void ResetLvl()
    {
        PlayerPrefs.DeleteKey("Time");
        PlayerPrefs.DeleteKey("MoneyRound");
        PlayerPrefs.Save();
    }

    public void Retry()
    {
        PlayerPrefs.SetFloat("Time",Time.time);
        PlayerPrefs.SetInt("MoneyRound", Int32.Parse(uiManager.money.text));
        PlayerPrefs.Save();
    }

}
