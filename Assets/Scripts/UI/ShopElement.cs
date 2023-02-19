using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{
    public Skin mySkin;
    public Image myImg;
    public GameObject questionMark, select;
    public TextMeshProUGUI price;
    public void Initialize(Skin skin)
    {
        mySkin = skin;
        myImg.sprite = mySkin.texture;
        if(PlayerPrefs.HasKey("skin" + mySkin.id.ToString()))
        {
            mySkin.unlocked = true;
        }
        if (skin.unlocked)
        {
            questionMark.SetActive(false);
        }
        else {
            select.SetActive(false);
            price.text = mySkin.price.ToString();
        }
    }
    public void Unlock()
    {
        questionMark.SetActive(false);
        select.SetActive(true);
        PlayerPrefs.SetInt("skin" + mySkin.id.ToString(), 1);
    }
    public void Select()
    {
        PlayerPrefs.SetInt("currentskin", mySkin.id);
        BgMovement[] temp = GameObject.FindObjectsOfType<BgMovement>();
        temp[0].SetSkin();
    }
}
