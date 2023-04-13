using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Skin :  ShopItem
{
    public int upgradeStage;
    
    private Sprite itemSprite;
    private RawImage skinImageShow;
    private RawImage buttonSprite;
    private TextMeshProUGUI text;
    private int price;
    private int id;
    private ElementType type;
    
    public override ShopItem Initialize(Item shopItem,bool status)
    {
        effects = shopItem.effects;
        type = shopItem.type;
        id = shopItem.id;
        price = effects[0].price;
        itemSprite = shopItem.sprite;
        
        skinImageShow = transform.GetChild(0).GetComponent<RawImage>();
        buttonSprite = transform.GetChild(1).GetComponent<RawImage>();
        text = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        SetStatus(status);
        return this;
    }
    

    public void SetStatus(bool status)
    {
        AddListener(status);
        if (status)
        {
            //Unselect Case
            buttonSprite.texture = ShopManager.instance.unselectedTexture;
            text.text = "Select";
            skinImageShow.texture = itemSprite.texture;
            text.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            //Need to be bought case
            skinImageShow.texture = ShopManager.instance.unBoughtImage;
            text.color = new Color32(255, 255, 255, 255);
            text.text = price.ToString();
        }
    }

    
    public override void Buy()
    {

        int currentMoney = Int32.Parse(ShopManager.instance.money.text);
        if ( currentMoney >= price)
        {
            currentMoney -= price;
            PlayerPrefs.SetInt("Money",currentMoney);
            ShopManager.instance.money.text = currentMoney.ToString();
            skinImageShow.texture = itemSprite.texture;
            text.text = "Select";
            text.color = new Color32(255, 255, 255, 255);
            PlayerPrefs.SetInt("unlockedSkin" + id, 1);
            ClearListener();
            AddListener(true);
        }
    }

    public void Unselect(Texture2D unselectedTexture)
    {
        buttonSprite.texture = unselectedTexture;
        text.color = new Color32(255, 255, 255, 255);
        text.text = "Select";
    }
    
    public override void Select()
    {
        ShopManager.instance.selectedSkin = id;
        buttonSprite.texture = ShopManager.instance.selectTexture;
        text.text = "Selected";
        text.color = new Color32(0, 0, 0, 255);
        PlayerPrefs.SetInt("currentSkin", id);
    }
    
    private void AddListener(bool status)
    {
        if (status)
        {
            buttonSprite.gameObject.GetComponent<Button>().onClick.AddListener(()=>
            { 
                ShopManager.instance.ChangeSelectedSkin(id);
                Select();
            });
        }
        else
        {
           buttonSprite.gameObject.GetComponent<Button>().onClick.AddListener(Buy);   
        }
    }


    private void ClearListener()
    {
        buttonSprite.GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
