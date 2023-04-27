using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public TextMeshProUGUI money;


    private void OnEnable()
    {
        GameManager.onGameOver += SaveMoney;
        GameManager.onGameOver += SaveHighScore;
    }

    private void OnDisable()
    {
        GameManager.onGameOver -= SaveMoney;
        GameManager.onGameOver -= SaveHighScore;
    }


    private void SaveHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");
        int score = Int32.Parse(money.text);
        
        if(score>highScore)
            PlayerPrefs.SetInt("HighScore",score);
    }

    public void SaveMoney()
    {
        int amount = PlayerPrefs.GetInt("Money");
        amount += Int32.Parse(money.text);
        PlayerPrefs.SetInt("Money", amount);
        PlayerPrefs.Save();
    }

}
