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

    public static Action onGameOver;

    public UIManagerGameRoom uiManager;
    public SpawnManager spawnManager;
    public Vector2 PlayerPosition => player.position;
    public DataManager dataManager;
    public SkinPool skins;

    private  Transform player;
    private bool alreadyOver;
    private static bool askedAd;
    private int moneyMultiplayer=1;

    public Transform Player => player;

    private void Start()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        
        StartCoroutine(InitGame());
    }

    IEnumerator InitGame()
    {
        //Init Money
        CoinsBehaviour.amount = moneyMultiplayer;
        uiManager.SetMoneySign(moneyMultiplayer);
        
        player.GetComponent<SpriteRenderer>().sprite = skins.skins[PlayerPrefs.GetInt("currentSkin")].sprite;
        
        yield return new WaitForSeconds(2f);
        spawnManager.enabled = true;
    }
    
    public void GameOver()
    {
        if (!alreadyOver)
        {
            onGameOver?.Invoke();
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
        PlayerPrefs.DeleteKey("multiplayer");
        PlayerPrefs.DeleteKey("scoreRound");
        PlayerPrefs.Save();
    }

    public void Retry()
    {
        PlayerPrefs.SetFloat("multiplayer",moneyMultiplayer);
        PlayerPrefs.SetInt("scoreRound",Int32.Parse(uiManager.money.text));
        PlayerPrefs.Save();
    }
    
    public void IncreaseMoneyValue()
    {
        moneyMultiplayer *= 2;
        CoinsBehaviour.amount = moneyMultiplayer;
        uiManager.SetMoneySign(moneyMultiplayer);
    }

}
