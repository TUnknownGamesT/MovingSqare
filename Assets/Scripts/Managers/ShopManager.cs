using System;
using System.Collections;
using System.Collections.Generic;
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
    
    public SkinPool skinPool;
    public GameObject contentFile;
    public Transform container;
    public TextMeshProUGUI money;

    private readonly List<ShopElement> shopElements = new ();


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }


    private void Init()
    {
        foreach (var t in skinPool.skins)
        {
            shopElements.Add(Instantiate(contentFile, container).GetComponent<ShopElement>().Initialize(t));
        }
        
        if (PlayerPrefs.HasKey("currentSkin"))
        {
            int id = PlayerPrefs.GetInt("currentSkin");
            shopElements[id].Select();
        }
    }
}
