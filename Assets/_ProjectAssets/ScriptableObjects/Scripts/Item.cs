using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ElementType
{ 
    Skin,
   PowerUp
}

[System.Serializable]
public class Effects
{
    public string name;
    public int price;
    public string effect;
}


[CreateAssetMenu(fileName = "Shop", menuName = "Custom/ShopItem")]
public class Item : ScriptableObject
{
    public int id;
    public Sprite sprite;
    public bool unlocked;
    public ElementType type;
    public Effects[] effects;
}
