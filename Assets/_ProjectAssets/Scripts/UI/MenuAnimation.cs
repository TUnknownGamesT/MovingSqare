using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuAnimation : MonoBehaviour
{
    public Image[] buttonsFill;
    public TextMeshProUGUI[] textFade;
    public TextMeshProUGUI money;
    public GameObject main, shop, topBar, startButton, gameModeMenu;
    public RawImage darkEdgeShop;
    public Image backButonShop;
    private void Start()
    {
        InitPlayerPrefs();
        InitAnimations();
    }


    private void InitPlayerPrefs()
    {
        money.text = PlayerPrefs.HasKey("Money") ? PlayerPrefs.GetInt("Money").ToString() : "0";
        if (!PlayerPrefs.HasKey("currentSkin"))
        {
            PlayerPrefs.SetInt("currentSkin",0);
        }
    }

    private void InitAnimations()
    {
        for(int i=0; i < buttonsFill.Length; i++)
        {
            buttonsFill[i].fillAmount = 0;
            int index = i;
            LeanTween.value(0, 1, 1.5f).setOnUpdate(value => {
                buttonsFill[index].fillAmount = value;
            });
        }
        for(int i=0; i< textFade.Length; i++)
        {
            textFade[i].color = new Color32(255, 255, 255, 1);
            int index = i;
            LeanTween.value(0, 1, 1.5f).setOnUpdate(value =>
            {
                Color c = textFade[index].color;
                c.a = value;
                textFade[index].color = c ;
            });
        }

    }
    
    public void LeaveMenu()
    {
        for (int i = 0; i < buttonsFill.Length; i++)
        {
            buttonsFill[i].fillAmount = 0;
            int index = i;
            LeanTween.value(1, 0, 1.5f).setOnUpdate(value => {
                buttonsFill[index].fillAmount = value;
            });
        }
        for (int i = 0; i < textFade.Length; i++)
        {
            textFade[i].color = new Color32(255, 255, 255, 1);
            int index = i;
            LeanTween.value(1, 0, 1.5f).setOnUpdate(value =>
            {
                Color c = textFade[index].color;
                c.a = value;
                textFade[index].color = c;
            });
        }
        GameObject sign = money.transform.parent.Find("Sign").gameObject;
        LeanTween.value(1f, 0f, 1f).setOnUpdate(value => {
            sign.GetComponent<RawImage>().color = new Color(1, 1, 1, value);
            money.color = new Color(1, 1, 1, value);
        });
    }

    public void GoShop()
    {
        StartCoroutine(AnimateTransition(1, 30,0));
        StartCoroutine(ShowHideShop(0,1,1));
    }
    public void LeaveShop()
    {
        StartCoroutine(AnimateTransition(30, 1,1));
        StartCoroutine(ShowHideShop(1, 0,0));
    }
    IEnumerator AnimateTransition(float from, float to, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (waitTime > 0)
        {
            shop.SetActive(false);
        }
        LeanTween.value(from, to, 1f).setOnUpdate(value => {
            Vector3 temp = new Vector3(value, value, 1);
            main.transform.localScale = temp;
            topBar.transform.localScale = temp;
        });
        
    }
    IEnumerator ShowHideShop(float from, float to,float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
        shop.SetActive(true);
        LeanTween.value(from, to, 1f).setOnUpdate(value =>
        {
            
            darkEdgeShop.uvRect = new Rect(1-value, 0, 1, 1);
            backButonShop.fillAmount = value;
        });
    }

    public void ChoseGameMode()
    {
        LeanTween.rotate(startButton, new Vector3(0, 0, 6120), 1f);
        StartCoroutine(ShowGameModeMenu());

    }
    public IEnumerator ShowGameModeMenu()
    {
        yield return new WaitForSeconds(0.1f);
        gameModeMenu.GetComponent<HorizontalLayoutGroup>().spacing = -650f;
        gameModeMenu.SetActive(true);
        startButton.SetActive(false);
        foreach(Transform child in gameModeMenu.transform)
        {
            Debug.Log(child.name);
            LeanTween.rotate(child.gameObject, new Vector3(0, 0, 6120), 1f);
        }
        LeanTween.value(-650f, 20f, 1f).setOnUpdate(value =>
        {
            gameModeMenu.GetComponent<HorizontalLayoutGroup>().spacing = value;
        });
    }
}
