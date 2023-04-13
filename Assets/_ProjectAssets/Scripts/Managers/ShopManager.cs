using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    #region Singleton

    public static ShopManager instance;

    private void Awake()
    {
        instance = FindObjectOfType<ShopManager>();
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion
    
    public ItemsPool itemsPool;
    public GameObject skinContainer;
    public GameObject powerUpContainer;
    public Transform contentContainer;
    public TextMeshProUGUI money;
    public int selectedSkin;
    public TextMeshProUGUI _details;
    
    [Header("Buttons")]
    public Texture2D unselectedTexture;
    public Texture2D selectTexture;

    [Header("Item Main Image")] 
    public Texture2D unBoughtImage;
    
    private readonly List<ShopItem> shopElements = new ();
    private  List<BgMovement> bkSquares = new();
    private Dictionary<int,bool> skinStatus = new();
    

    
    void Start()
    {
        bkSquares = FindObjectsOfType<BgMovement>().ToList();
        InitPlayerPrefs();
        InitShopElements();
    }
    

    private void InitPlayerPrefs()
    {
        foreach (var skin in itemsPool.items)
        {
            if (skin.type == ElementType.Skin)
            {
                skinStatus.Add(skin.id, PlayerPrefs.HasKey($"unlockedSkin{skin.id}"));
            }
            
        }

        selectedSkin = PlayerPrefs.HasKey("currentSkin") ? PlayerPrefs.GetInt("currentSkin") : 0;
    }
    
  
    public void ChangeSelectedSkin(int skinID)
    {
        Skin skin = (Skin)shopElements[selectedSkin];
        skin.Unselect(unselectedTexture);
        skin = null;
        selectedSkin = skinID;
        SetBkSquares();
    }
   
    private void InitShopElements()
    {
        foreach (var t in itemsPool.items)
        {
            if (t.type == ElementType.Skin)
            {
                shopElements.Add(Instantiate(skinContainer, contentContainer)
                .GetComponent<ShopItem>().Initialize(t,skinStatus[t.id]));
            }
            else
            {
                shopElements.Add(Instantiate(powerUpContainer, contentContainer)
                    .GetComponent<ShopItem>().Initialize(t,skinStatus[t.id]));
            }
        }
        
        shopElements[selectedSkin].GetComponent<Skin>().SetStatus(true);
        shopElements[selectedSkin].Select();
    }

    public void SetDetails(int index)
    {
        if (shopElements[index].upgradeStage == 3)
        {
            _details.text = $"{shopElements[index].effects[2].name} :" +
                            $" {shopElements[index].effects[2].effect}";
        }
        else
        {
            _details.text = $"{shopElements[index].effects[shopElements[index].upgradeStage].name} :" +
                            $" {shopElements[index].effects[shopElements[index].upgradeStage].effect}";
        }
        
       
    }
    
    private void SetBkSquares()
    {
        foreach (var square in bkSquares)
        {
            square.SetSkin();
        }
    }
    
}
