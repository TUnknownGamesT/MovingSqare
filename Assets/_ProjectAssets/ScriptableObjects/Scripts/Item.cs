using UnityEngine;

public enum ElementType
{ 
    Skin=0,
    PowerUp
}

public enum EffectType{
    Speed,
    Size,
    Life,
    None
}

[System.Serializable]
public class Effects
{
    public string name;
    public int price;
    public EffectType effect;
    public float value;
}


[CreateAssetMenu(fileName = "Shop", menuName = "Custom/ShopItem")]
public class Item : ScriptableObject
{
    public int id;
    public Sprite sprite;
    public Texture2D trailTexture;
    public bool unlocked;
    public ElementType type;
    public Effects[] effects;

    public float GetEffect(string itemName)
    {
        int index = PlayerPrefs.GetInt(itemName);
        
        if (index == 3)
            index--;
        
        return effects[index].value; 
    }
    
    
}
