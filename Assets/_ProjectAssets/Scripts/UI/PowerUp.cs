using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp :  ShopItem
{

    private RawImage skinImageShow;
    private TextMeshProUGUI text;
    private ElementType type;

    public override ShopItem Initialize(Item shopItem, bool status)
    {
        elementType = ElementType.PowerUp;
        effects = shopItem.effects;
        type = shopItem.type;
        
        upgradeStage = PlayerPrefs.HasKey(effects[0].name) ? PlayerPrefs.GetInt(effects[0].name) : 0;
        
        text = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        transform.GetChild(0).GetComponent<RawImage>().texture = shopItem.sprite.texture;
        
        SetStatus();
    
        return this;
    }

    public override ShopItem Initialize(Item shopItem, bool status, ShopText shopText){return null;}
    private void SetStatus()
    {
        if (upgradeStage < 3)
        {
            text.text = effects[upgradeStage].price.ToString();
            AddListener();
        }
        else
        {
            text.text = "MAX";
        }
    }
    
    public override void Select()
    {
        if (upgradeStage >= 3)
        {
            ClearListener();
        }
    }

    public override void Buy()
    {
        int currentMoney = Int32.Parse(ShopManager.instance.money.text);
        if ( currentMoney >= effects[upgradeStage].price)
        {
            currentMoney -= Int32.Parse(text.text);
            PlayerPrefs.SetInt("Money",currentMoney);
            ShopManager.instance.money.text = currentMoney.ToString();

            upgradeStage++;
            
            text.text = upgradeStage == 3 ? "MAX" : effects[upgradeStage].price.ToString();
            
            PlayerPrefs.SetInt(effects[0].name, upgradeStage);
            Select();
        }
    }

    private void AddListener()
    {
        if (upgradeStage < 3)
        {
            transform.GetChild(1).GetComponent<Button>().onClick.AddListener(Buy);
        }
    }
    
    private void ClearListener()
    {
        transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
