using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuAnimation : MonoBehaviour
{
    public Image[] buttonsFill;
    public TextMeshProUGUI[] textFade;
    private void Start()
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
    public void leaveMenu()
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
    }
}
