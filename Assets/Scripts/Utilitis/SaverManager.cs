using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaverManager : MonoBehaviour
{
    public TextMeshProUGUI money;
    
    public void SaveMoney()
    {
        PlayerPrefs.SetInt("Money",Int32.Parse(money.text));
        PlayerPrefs.Save();
    }
}
