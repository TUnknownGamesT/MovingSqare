using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    #region Singleton

    public static UIManager instance;

    private void Awake()
    {
        instance = FindObjectOfType<UIManager>();
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion
    
    public CanvasGroup loseState;
    public RawImage joyStick;
    public GameObject movingZone;
    public CanvasGroup adState;
    public TextMeshProUGUI money;
    
    
    private void Start()
    {
       SetBkSize();
       SetMoney();
       
    }

    private void SetMoney()
    {
        if (PlayerPrefs.HasKey("MoneyRound"))
        {
            money.text = PlayerPrefs.GetInt("MoneyRound").ToString();
        }
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
        loseState.gameObject.SetActive(true);
        LeanTween.value(0, 1, 1f).setOnUpdate(value =>
        {
            loseState.alpha = value;

        }).setEaseInQuad().setOnComplete(()=>joyStick.gameObject.SetActive(false));
    }


    public void AdState()
    {
        adState.gameObject.SetActive(true);
        LeanTween.value(0, 1, 1f).setOnUpdate(value =>
        {
            adState.alpha = value;

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

    public void ResetCanvas()
    {
       PlayerPrefs.DeleteKey("Time");
       PlayerPrefs.DeleteKey("MoneyRound");
        LeanTween.value(1, 0, 1f).setOnUpdate(value =>
        {
            adState.alpha = value;

        }).setEaseInQuad().setOnComplete(() =>
        {
            adState.gameObject.SetActive(false);
            LoseState();
        });
    }
}
