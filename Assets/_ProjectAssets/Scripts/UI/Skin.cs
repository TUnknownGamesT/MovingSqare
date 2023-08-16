using System.Xml.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Skin :  ShopItem
{
    private Sprite itemSprite;
    [SerializeField]
    private RawImage skinImageShow;
    [SerializeField] 
    private RawImage buttonSprite;
    [SerializeField] 
    private TextMeshProUGUI text;
    private int price;
    private int id;
    private ElementType type;
    
    public override ShopItem Initialize(Item shopItem,bool status)
    {
        elementType = ElementType.Skin;
        effects = shopItem.effects;
        type = shopItem.type;
        id = shopItem.id;
        price = type == ElementType.Skin ? effects[0].price : effects[PlayerPrefs.GetInt(effects[0].name)].price;

        itemSprite = shopItem.sprite;
        
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
            StartCoroutine(AnimatePurchase());
            PlayerPrefs.SetInt("unlockedSkin" + id, 1);
            ClearListener();
            AddListener(true);
        }
    }
    IEnumerator AnimatePurchase()
    {
        LeanTween.scale(skinImageShow.gameObject, new Vector3(0,0,0),1f);
        yield return new WaitForSeconds(1f);
        skinImageShow.texture = itemSprite.texture;
        text.text = "Select";
        text.color = new Color32(255, 255, 255, 255);
        LeanTween.scale(skinImageShow.gameObject, new Vector3(1,1,1), 0.5f).setEase(LeanTweenType.easeOutBack);
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
