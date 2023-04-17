using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopItem : MonoBehaviour
{
    public Effects[] effects;
    public int upgradeStage= 0;
    
    public abstract ShopItem Initialize(Item shopItem, bool status);
    public abstract void Select();

    public abstract void Buy();
    
    
}