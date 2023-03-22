using System;
using System.Collections;
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

    public TextMeshProUGUI uiScore;
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI score;
    public TextMeshProUGUI moneyCollected;
    public CanvasGroup loseState;
    public RawImage joyStick;
    public GameObject movingZone;
    public TextMeshProUGUI money;
    public GameObject firstButton;
    public GameObject secondButton;
    public SceneLoader sceneLoader;
    public RectTransform x2Money;
    
    private bool gameOver;

    private void OnEnable()
    {
        GameManager.onGameOver += SetGameOver;
    }

    private void OnDisable()
    {
        GameManager.onGameOver -= SetGameOver;
    }

    public void DoubleMoneySign()
    {
        x2Money.gameObject.SetActive(true);
        LeanTween.move(x2Money, new Vector3(596, 484, 0), 1.8f).setEaseInCubic();
        LeanTween.value(0, 1, 0.9f).setOnUpdate(value =>
        {
            Color c = x2Money.GetComponent<TextMeshProUGUI>().color;
            c.a = value;
            x2Money.GetComponent<TextMeshProUGUI>().color = c;
        }).setEaseInCubic().setOnComplete(() =>
        {
            LeanTween.value(1, 0, 0.9f).setOnUpdate(value =>
            {
                Color c = x2Money.GetComponent<TextMeshProUGUI>().color;
                c.a = value;
                x2Money.GetComponent<TextMeshProUGUI>().color = c;
            }).setEaseInCubic().setOnComplete(()=>x2Money.gameObject.SetActive(false));
        });
    }

    private void Start()
    {
       SetBkSize();
       SetDataFromPreviousRound();

       StartCoroutine(IncreaseScore());
    }

    private void SetDataFromPreviousRound()
    {
        money.text = PlayerPrefs.GetInt("MoneyRound").ToString();
        score.text = PlayerPrefs.GetInt("ScoreRound").ToString();
    }

    IEnumerator IncreaseScore()
    {
        yield return new WaitForSeconds(1.8f);
        
        int score = Int32.Parse(this.score.text);
        score += 1;
        this.score.text = score.ToString();

        if (!gameOver)
            StartCoroutine(IncreaseScore());
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
            (float)worldScreenHeight / height*0.75f);

    }

    public void UpdateMoney(int amount)
    {
        int amountOfMoney = Int32.Parse(money.text);
        amountOfMoney += amount;
        money.text = amountOfMoney.ToString();
    }
    
    public void LoseState()
    {
        UpdateScoreUI();
        ButtonsLoseState();
        ResetLvlListener();
        loseState.gameObject.SetActive(true);
        
        LeanTween.value(0, 1, 1f).setOnUpdate(value =>
        {
            loseState.alpha = value;

        }).setEaseInQuad().setOnComplete(()=>joyStick.gameObject.SetActive(false));
    }


    public void AdState()
    {
        UpdateScoreUI();
        ButtonsAdState();
        AdListener();
        loseState.gameObject.SetActive(true);
        LeanTween.value(0, 1, 1f).setOnUpdate(value =>
        {
            loseState.alpha = value;

        }).setEaseInQuad().setOnComplete(()=>joyStick.gameObject.SetActive(false));
    }
    
    public void FadeInFadeOutJoystick()
    {
        LeanTween.value(1, 0, 1f).setOnUpdate(value =>
        {
            Color c =  joyStick.color;
            c.a = value;
            joyStick.color = c;
            
        }).setEaseInQuad().setOnComplete(()=>joyStick.gameObject.SetActive(false));
        
    }

    private void UpdateScoreUI()
    {
        highScore.text =  $"High Score:{PlayerPrefs.GetInt("HighScore")}";
        uiScore.text =  $"Score:{score.text}";
        moneyCollected.text =  $"Score:{money.text}";
    }
    
    private void ButtonsAdState()
    {
        firstButton.GetComponent<Image>().color = Color.green;
        firstButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Revive";
        firstButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.green;
        
        
        secondButton.GetComponent<Image>().color = Color.red;
        secondButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;
    }

    private void ButtonsLoseState()
    {
        firstButton.GetComponent<Image>().color = Color.white;
        firstButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
        
        secondButton.GetComponent<Image>().color = Color.white;
        secondButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
    }

    private void AdListener()
    {
        firstButton.GetComponent<Button>().onClick.AddListener(AdsManager.ShowAd);
    }

    private void ResetLvlListener()
    {
        firstButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            GameManager.instance.ResetLvl();
            GameManager.instance.ResetAlreadyOver();
            sceneLoader.ReloadGameScene();
        });
    }

    private void SetGameOver()
    {
        gameOver = true;
    }
    
}
