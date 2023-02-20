using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skins")]
public class Skin : ScriptableObject
{
    public int id,price;
    public Sprite sprite;
    public bool unlocked;
}
