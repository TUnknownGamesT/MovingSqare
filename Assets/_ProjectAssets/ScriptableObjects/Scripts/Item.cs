using UnityEngine;



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
    public string shopDescription;
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
        
        return float.Parse(effects[index].effect); 
    }
    
    
}
