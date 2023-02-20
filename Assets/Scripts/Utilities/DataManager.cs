using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public TextMeshProUGUI money;

    public void SaveMoney()
    {
        int amount = PlayerPrefs.GetInt("Money");
        amount += Int32.Parse(money.text);
        PlayerPrefs.SetInt("Money", amount);
        PlayerPrefs.Save();
    }

}
