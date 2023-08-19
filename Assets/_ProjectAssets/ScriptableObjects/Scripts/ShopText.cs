using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct EffectTypeString {
    public EffectType type;
    public string text;
    public float value;
}
[CreateAssetMenu(fileName = "ShopTxt", menuName = "Custom/ShopTxt")]
public class ShopText : ScriptableObject
{
    [SerializeField]
    public EffectTypeString[] effectTypeString;
}
