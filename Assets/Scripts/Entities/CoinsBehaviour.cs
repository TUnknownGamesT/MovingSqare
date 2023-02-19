using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsBehaviour : MonoBehaviour
{
    public int amount;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.PickCoinSound();
            UIManager.instance.UpdateMoney(amount);
            Destroy(gameObject);
        }
    }
}
