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
    public CanvasGroup loseState;
    public GameObject movingZone;
    public TextMeshProUGUI money;
    public GameObject moneyParent;
    public GameObject revive;
    public RectTransform x2Money;
    public List<GameObject> playerLives;
    public GameObject livesParent;

    private bool gameOver;
    private int index = 0;
    private int timeToIncreaseMoneyValue;
    
    private void OnEnable()
    {
        GameManager.onGameOver += SetGameOver;
        AdsManager.onAdFinish += ResetMainMenu;
    }

    private void OnDisable()
    {
        GameManager.onGameOver -= SetGameOver;
        AdsManager.onAdFinish -= ResetMainMenu;
    }
    

    private void Start()
    {
        money.text = "0";
        SetBkSize();
    }
    
    
    public void SetMoneySign(int amount)
    {
        x2Money.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"+{amount}";
        LeanTween.scale(x2Money, new Vector3(1.5f, 1.5f, 1.5f), 1f).setEaseInBounce()
            .setOnComplete(() =>
            {
                LeanTween.scale(x2Money, Vector3.one, 0.5f).setEaseOutBounce();
            });
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
        UpdateScoreUI();
        
        
        loseState.gameObject.SetActive(true);
        revive.SetActive(false);
        
        LeanTween.value(0, 1, 1f).setOnUpdate(value =>
        {
            loseState.alpha = value;

        }).setEaseInQuad().setDelay(0.5f);
    }
    
    public void AdState()
    {
        UpdateScoreUI();
        AdListener();

        revive.SetActive(true);
        loseState.gameObject.SetActive(true);
        LeanTween.value(0, 1, 1f).setOnUpdate(value =>
        {
            loseState.alpha = value;
        }).setEaseInQuad().setDelay(0.5f);
    }
    

    private void UpdateScoreUI()
    {
        highScore.text =  $"High Score\n{PlayerPrefs.GetInt("HighScore")}";
        moneyCollected.text =  $"Score\n{money.text}";
    }
    

    private void AdListener()
    {
        revive.GetComponent<Button>().onClick.AddListener(AdsManager.ShowAd);
    }
    

    public void ResetMainMenu()
    {
        revive.GetComponent<Button>().onClick.RemoveAllListeners();
        LeanTween.value(1, 0, 1f).setOnUpdate(value =>
        {
            loseState.alpha = value;

        }).setEaseInQuad().setOnComplete( ()=> loseState.gameObject.SetActive(false));
    }
    
    private void SetGameOver()
    {
        gameOver = true;
    }
    
    public void IncreaseLife(int life)
    {
        while(index < life-1){
            index++;
            playerLives[index].SetActive(true);
        }
        
        LeanTween.value(1, 0.5f, 0.7f).setOnUpdate(value =>
        {
            livesParent.transform.localScale = Vector3.one * value;
        }).setEasePunch();
        
    }

    public void DecreaseLife()
    {
        playerLives[index].SetActive(false);
        index--;
    }
    
    
}
