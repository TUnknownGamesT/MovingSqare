using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsBehaviour : MonoBehaviour
{

    public static Action<bool> onCoinDestroy;
    
    public float life;
    public static int amount;
    public GameObject destroyVFX;
    public GameObject vfx;
    public GameObject textEffect;


    private void Start()
    { 
        Invoke(nameof(Destroy),life);
    }

    private void Destroy()
    {
        Instantiate(destroyVFX, transform.position, Quaternion.identity);
        onCoinDestroy?.Invoke(false);
        Destroy(gameObject);
    }
    

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            Instantiate(textEffect, transform.position+Vector3.forward*-9.125f, Quaternion.identity);
            Instantiate(vfx, transform.position, Quaternion.identity);
            UIManagerGameRoom.instance.UpdateMoney(amount);
            BackgroundParticlesManager.instance.TakeCoinAnimation();
            SoundManager.instance.PlaySoundEffect(Constants.Sounds.PickCoin);
            onCoinDestroy?.Invoke(true);
            Destroy(gameObject);
        }
    }
}
