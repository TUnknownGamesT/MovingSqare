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
        });
        LeanTween.value(1f, 0f, 1f).setOnUpdate(value => {
            money.color = new Color(1, 1, 1, value);
        });
    }
}
