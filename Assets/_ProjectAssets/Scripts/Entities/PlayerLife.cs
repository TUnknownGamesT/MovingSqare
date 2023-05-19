using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    
    public UIManagerGameRoom uiManagerGameRoom;
    
    private int life=1;

    public int Life
    {
        get => life;
    }
    
    public void AddLife(int amount)
    {
        if (life + amount > 3)
        {
            life = 3;
        }
        else
        {
            life += 1;
            uiManagerGameRoom.IncreaseLife();
        }
    }

    public void Damage(int damage)
    {
        Debug.Log("i took dmg");
        if (life - damage < 0)
            return;
        
        Handheld.Vibrate();
        life -= damage;
        EffectManager.DamageEffect();
        uiManagerGameRoom.DecreaseLife();
        if(life <=0)
            GameManager.instance.GameOver();
    }

}
