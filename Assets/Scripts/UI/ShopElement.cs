using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{ 
    [HideInInspector]
    public Skin skin;
    [HideInInspector]
    public Sprite sprite;

    public RawImage statusButton;
    public GameObject questionMark, select;
    public TextMeshProUGUI price;
    public Texture2D unselectedTexture;
    public Texture2D selectTexture;

    public ShopElement Initialize(Skin skin)
    {
        this.skin = skin;
        sprite = skin.sprite;
        
        SetStatus();

        return this;
    }
 

    private void SetStatus()
    {
        if(PlayerPrefs.HasKey("skin" + skin.id))
        {
            statusButton.texture = unselectedTexture;
            skin.unlocked = true;
        }
        if (skin.unlocked)
        {
            questionMark.SetActive(false);
        }
        else {
            select.SetActive(false);
            price.text = skin.price.ToString();
        }
    }
    
    
    
    public void Unlock()
    {
        if (Int32.Parse(ShopManager.instance.money.text)>= skin.price &&!skin.unlocked)
        {
            questionMark.SetActive(false);
            select.SetActive(true);
            PlayerPrefs.SetInt("skin" + skin.id, 1);
        }
    }
    public void Select()
    {

        if (skin.unlocked)
        {
            statusButton.texture = selectTexture;
            PlayerPrefs.SetInt("currentSkin", skin.id);
            BgMovement[] temp = FindObjectsOfType<BgMovement>();
            temp[0].SetSkin();
        }
    }
}
