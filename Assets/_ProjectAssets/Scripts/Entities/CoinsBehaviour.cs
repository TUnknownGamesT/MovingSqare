using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsBehaviour : MonoBehaviour
{

    public static int amount;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            UIManagerGameRoom.instance.UpdateMoney(amount);
            SoundManager.instance.PlaySoundEffect(Constants.Sounds.PickCoin);
            Destroy(gameObject);
        }
    }
}
