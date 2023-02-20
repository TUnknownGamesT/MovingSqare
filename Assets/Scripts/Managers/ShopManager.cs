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
    
    

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<skinPool.skins.Length; i++)
        {
            Instantiate(contentFile, container).GetComponent<ShopElement>().Initialize(skinPool.skins[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
