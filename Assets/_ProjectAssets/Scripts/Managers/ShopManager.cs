using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

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
    
    public SkinPool skinPool;
    public GameObject contentFile;
    public Transform container;
    public TextMeshProUGUI money;
    public int selectedSkin;
    
    [Header("Buttons")]
    public Texture2D unselectedTexture;
    public Texture2D selectTexture;

    [Header("Element Main Image")] 
    public Texture2D unBoughtImage;
    
    private readonly List<ShopElement> shopElements = new ();
    private  List<BgMovement> bkSquares = new();
    private Dictionary<int,bool> skinStatus = new();
    

    // Start is called before the first frame update
    void Start()
    {
        bkSquares = FindObjectsOfType<BgMovement>().ToList();
        InitPlayerPrefs();
        InitShopElements();
    }

    private void InitPlayerPrefs()
    {
        foreach (var skin in skinPool.skins)
        {
            if (PlayerPrefs.HasKey($"skin{skin.id}"))
            {
                skinStatus.Add(skin.id,skin.unlocked);
            }
            else
            {
                skinStatus.Add(skin.id,false);
            }
        }

        if (PlayerPrefs.HasKey("currentSkin"))
        {
            selectedSkin = PlayerPrefs.GetInt("currentSkin");
        }
        else
        {
            selectedSkin = 0;
        }
    }
    
  
    public void ChangeSelectedSkin(int skinID)
    {
        shopElements[selectedSkin].Unselect(unselectedTexture);
        selectedSkin = skinID;
        SetBkSquares();
    }
   
    private void InitShopElements()
    {
        foreach (var t in skinPool.skins)
        {
            shopElements.Add(Instantiate(contentFile, container)
                .GetComponent<ShopElement>().Initialize(t,skinStatus[t.id]));
        }
        
        shopElements[selectedSkin].SetStatus(true);
        shopElements[selectedSkin].Select();
    }
    
    
    private void SetBkSquares()
    {
        foreach (var square in bkSquares)
        {
            square.SetSkin();
        }
    }
    
}
