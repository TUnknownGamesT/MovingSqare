using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{

    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
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
    public ItemsPool items;
    public int difficultySpeed;
    public float extraMoneyIncrease;
    
    
    private  Transform player;
    private bool alreadyOver;
    private static bool askedAd;
    private int moneyMultiplayer=1;
    private int index;

    public Transform Player => player;


    private void OnEnable()
    {
        BossGameplay.OnBossAppear += StopCoroutine;
        BossGameplay.OnBossDisappear += StartCoroutine;
        AdsManager.onAdFinish += ResetAlreadyOver;
    }
    
    
    private void OnDisable()
    {
        BossGameplay.OnBossAppear -= StopCoroutine;
        BossGameplay.OnBossDisappear -= StartCoroutine;
        AdsManager.onAdFinish -= ResetAlreadyOver;
    }

    private void Start()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        
        player.GetComponent<PlayerManager>().InitPlayer(items.items[PlayerPrefs.GetInt("currentSkin")]);
        
        StartCoroutine(InitGame());

    }
    
    IEnumerator InitGame()
    {
        //Init Money
        CoinsBehaviour.amount = moneyMultiplayer;
        uiManager.SetMoneySign(moneyMultiplayer);

        yield return new WaitForSeconds(2f);
        spawnManager.StartSpawning();
        
        StartCoroutine(IncreaseDifficulty());
        StartCoroutine(IncreaseMoneyValue());
    }
    

    public void GameOver()
    {
        if (!alreadyOver)
        {
            onGameOver?.Invoke();
            if (!askedAd)
            {
                uiManager.AdState();
                askedAd = !askedAd;
            }
            else
            {
                askedAd = !askedAd;
                uiManager.LoseState();
            }
            alreadyOver = !alreadyOver;
        }
    }
    
    public void ResetAlreadyOver()
    {
        alreadyOver = !alreadyOver;
    }

    private void StopCoroutine()
    {
        StopAllCoroutines();
    }

    private void StartCoroutine()
    {
        StartCoroutine(IncreaseDifficulty());
        StartCoroutine(IncreaseMoneyValue());
    }
    
    private IEnumerator IncreaseMoneyValue()
    {
        yield return new WaitForSeconds(extraMoneyIncrease);
        moneyMultiplayer *= 2;
        CoinsBehaviour.amount = moneyMultiplayer;
        uiManager.SetMoneySign(moneyMultiplayer);

        StartCoroutine(IncreaseMoneyValue());
    }
    
    private IEnumerator IncreaseDifficulty()
    {
        yield return new WaitForSeconds(difficultySpeed);

        int enemy = Random.Range(1, 4);

        switch (enemy)
        {
            case 1:
            {
                spawnManager.squareStage2 += Random.value;
                break;
            }

            case 2:
            {
                spawnManager.circleStage2 += Random.value;
                break;
            }

            case 3:
            {
                spawnManager.hexagonStage2 += Random.value;
                break;
            }
        }

        StartCoroutine(IncreaseDifficulty());

    }

}
