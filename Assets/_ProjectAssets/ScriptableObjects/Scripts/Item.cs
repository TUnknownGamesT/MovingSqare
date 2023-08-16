using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ElementType
{ 
    Skin=0,
    PowerUp
}

[System.Serializable]
public class Effects
{
    public string name;
    public int price;
    public string effect;
    public string shopDescription;
}


[CreateAssetMenu(fileName = "Shop", menuName = "Custom/ShopItem")]
public class Item : ScriptableObject
{
    public int id;
    public Sprite sprite;
    public bool unlocked;
    public ElementType type;
    public Effects[] effects;

    public float GetEffect(string itemName)
    {
        int index = PlayerPrefs.GetInt(itemName);
        
        if (index == 3)
            index--;
        
        return float.Parse(effects[index].effect); 
    }
    
    
}
