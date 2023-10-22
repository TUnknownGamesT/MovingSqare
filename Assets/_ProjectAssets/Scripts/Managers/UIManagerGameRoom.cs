using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerGameRoom : MonoBehaviour
{

    #region Singleton

    public static UIManagerGameRoom instance;

    private void Awake()
    {
        instance = FindObjectOfType<UIManagerGameRoom>();
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion
    
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI moneyCollected;
    public CanvasGroup mainUI;
    public GameObject movingZone;
    public TextMeshProUGUI money;
    public GameObject moneyParent;
    public GameObject revive;
    public GameObject next;
    public GameObject doubleCoin;
    public List<GameObject> playerLives;
    public GameObject livesParent;
    public GameObject pauseMenu;
    public GameObject maineMenu;
    public TextMeshProUGUI highScorePauseMenu;
    public TextMeshProUGUI moneyCollectedPauseMenu;

    private bool _gameOver;
    private int _index = 0;
    private int _timeToIncreaseMoneyValue;
    private bool _candWatchDoubleCoinAD=true;
    private bool _canWatchReviveAD=true;

    private void OnEnable()
    {
        GameManager.onGameOver += SetGameOver;
        AdsManager.onReviveADFinish += ReviveAdFinish;
        AdsManager.onDoubleMoneyADFinish += RemoveDoubleCoinButton;
        Timer.onCounterEnd += FinishLvlState;
    }

    private void OnDisable()
    {
        GameManager.onGameOver -= SetGameOver;
        AdsManager.onReviveADFinish -= ReviveAdFinish;
        AdsManager.onDoubleMoneyADFinish -= RemoveDoubleCoinButton;
        Timer.onCounterEnd -= FinishLvlState;
    }
    

    private void Start()
    {
        money.text = "0";
        SetBkSize();
    }

    private void  SetBkSize()
    {
        var sr = movingZone.GetComponent<SpriteRenderer>();
        if (sr == null) return;

        movingZone.transform.localScale = new Vector3(1, 1, 1);

        var width = sr.sprite.bounds.size.x;
        var height = sr.sprite.bounds.size.y;

        var worldScreenHeight = Camera.main.orthographicSize * 2f;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        movingZone.transform.localScale = new Vector2((float)worldScreenWidth / width,
            (float)worldScreenHeight / height);

    }

    public void UpdateMoney(int amount)
    {
        int amountOfMoney = Int32.Parse(money.text);
        amountOfMoney += amount;
        money.text = amountOfMoney.ToString();

        LeanTween.value(1, 0.3f, 0.7f).setOnUpdate(value =>
        {
            moneyParent.transform.localScale = Vector3.one * value;
        }).setEasePunch();

    }
    
    public void LoseState()
    {
        maineMenu.SetActive(true);
        UpdateScoreUI();
        
        mainUI.gameObject.SetActive(true);
        revive.SetActive(_canWatchReviveAD);
        next.SetActive(false);
        doubleCoin.SetActive(_candWatchDoubleCoinAD);

        FadeInEffect();
    }

    public void FinishLvlState()
    {
        maineMenu.SetActive(true);
        UpdateScoreUI();
        
        mainUI.gameObject.SetActive(true);
        revive.SetActive(false);
        next.SetActive(true);
        doubleCoin.SetActive(_candWatchDoubleCoinAD);
        
        FadeInEffect();
    }

    public void AdState()
    {
        maineMenu.SetActive(true);
        UpdateScoreUI();
        AdListener();

        revive.SetActive(_canWatchReviveAD);
        doubleCoin.SetActive(_candWatchDoubleCoinAD);
        mainUI.gameObject.SetActive(true);
       
        FadeInEffect();
    }

    
    private void RemoveDoubleCoinButton()
    {
        _candWatchDoubleCoinAD = false;
        doubleCoin.SetActive(false);
        DoubleTheMoney();
        UpdateScoreUI();
    }

    private void ReviveAdFinish()
    {
        _canWatchReviveAD = false;
        ResetMainMenu();
    }

    private void FadeInEffect()
    {
        LeanTween.value(0, 1, 1f).setOnUpdate(value =>
        {
            mainUI.alpha = value;
        }).setEaseInQuad().setDelay(0.5f);
    }
    

    private void UpdateScoreUI()
    {
        highScore.text =  $"High Score\n{PlayerPrefs.GetInt("HighScore")}";
        moneyCollected.text =  $"Score\n{money.text}";
    }


   

    private void AdListener()
    {
        revive.GetComponent<Button>().onClick.AddListener(AdsManager.InitReviveAD);
        doubleCoin.GetComponent<Button>().onClick.AddListener(AdsManager.InitDoubleCoinAD);
    }
    
    private void DoubleTheMoney()
    {
        int amountOfMoney = Int32.Parse(money.text);
        amountOfMoney *= 2;
        money.text = amountOfMoney.ToString();
    }

    private void ResetMainMenu()
    {
        revive.GetComponent<Button>().onClick.RemoveAllListeners();
        LeanTween.value(1, 0, 1f).setOnUpdate(value => mainUI.alpha = value).setEaseInQuad()
            .setOnComplete(()=> mainUI.gameObject.SetActive(false));
    }
    
    private void SetGameOver()
    {
        _gameOver = true;
    }
    
    public void IncreaseLife(int life)
    {
        while(_index < life-1){
            _index++;
            playerLives[_index].SetActive(true);
        }
        
        LeanTween.value(1, 0.5f, 0.7f).setOnUpdate(value =>
        {
            livesParent.transform.localScale = Vector3.one * value;
        }).setEasePunch();
        
    }

    public void ShowPauseMenu()
    {
        UpdateScoreUIPauseMenu();
        mainUI.gameObject.SetActive(true);
        mainUI.alpha = 1;
        pauseMenu.SetActive(true);
    }
    
    public void HidePauseMenu()
    {
        mainUI.alpha = 0;
        mainUI.gameObject.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void DecreaseLife()
    {
        playerLives[_index].SetActive(false);
        _index--;
    }

    private void UpdateScoreUIPauseMenu()
    {
        highScorePauseMenu.text =  $"High Score\n{PlayerPrefs.GetInt("HighScore")}";
        moneyCollectedPauseMenu.text =  $"Score\n{money.text}";
    }

}
