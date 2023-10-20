using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{

    public static Action onPlayerDie;
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
            uiManagerGameRoom.IncreaseLife(life);
        }
        else
        {
            life += 1;
            uiManagerGameRoom.IncreaseLife(life);
        }
    }

    public void Damage(int damage)
    {
        if (life - damage < 0)
            return;
        BackgroundParticlesManager.instance.TakeDamageAnimation();
        Handheld.Vibrate();
        life -= damage;
        EffectManager.DamageEffect();
        uiManagerGameRoom.DecreaseLife();
        if (life <= 0)
        {

            onPlayerDie?.Invoke();
            GameManager.instance.GameOver();
        }
            
    }

}
