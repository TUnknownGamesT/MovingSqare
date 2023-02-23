using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{
    private Skin skin;
    private Sprite skinSprite;
    private RawImage skinImageShow;
    private RawImage buttonSprite;
    private TextMeshProUGUI text;

    public ShopElement Initialize(Skin skin,bool status)
    {
        this.skin = skin;
        skinSprite = skin.sprite;
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
            buttonSprite.texture = ShopManager.instance.unselectedTexture;
            text.text = "Select";
            skinImageShow.texture = skinSprite.texture;
            text.color = new Color32(0, 0, 0, 255);
        }
        else
        {
            skinImageShow.texture = ShopManager.instance.unBoughtImage;
            text.color = new Color32(255, 255, 255, 255);
            text.text = skin.price.ToString();
        }
    }


    private void Unlock()
    {
        if (Int32.Parse(ShopManager.instance.money.text)>= skin.price)
        {
            Debug.Log("UNLOCKKKKKKKKKKKK");
            skinImageShow.texture = skinSprite.texture;
            text.text = "Select";
            text.color = new Color32(255, 255, 255, 255);
            PlayerPrefs.SetInt("skin" + skin.id, 1);
            ClearListener();
            AddListener(true);
        }
    }

    public void Unselect(Texture2D unselectedTexture)
    {
        buttonSprite.texture = unselectedTexture;
        text.text = "Select";
        
    }
    
    public void Select()
    {
        Debug.Log("SELECTTTTTTTTT");
        ShopManager.instance.selectedSkin = skin.id;
        buttonSprite.texture = ShopManager.instance.selectTexture;
        text.text = "Selected";
        text.color = new Color32(0, 0, 0, 255);
        PlayerPrefs.SetInt("currentSkin", skin.id);
    }


    private void AddListener(bool status)
    {
        
        if (status)
        {
            buttonSprite.gameObject.GetComponent<Button>().onClick.AddListener(()=>
            { 
                ShopManager.instance.ChangeSelectedSkin(skin.id);
                Select();
            });
            
        }
        else
        {
           buttonSprite.gameObject.GetComponent<Button>().onClick.AddListener(Unlock);   
        }

    }


    private void ClearListener()
    {
        buttonSprite.GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
