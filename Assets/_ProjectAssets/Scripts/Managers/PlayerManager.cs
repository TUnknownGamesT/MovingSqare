using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public PlayerLife playerLife;
    public Movement movement;

    public void InitPlayer(Item item)
    {
        GetComponent<SpriteRenderer>().sprite = item.sprite;
        ApplyEffect(item);
    }


    private void ApplyEffect( Item item)
    {
        switch (item.effects[0].name)
        {
            case "nothing":
            {
                return;
            }
            
            case "size":
            {
                Debug.Log(Vector3.one * float.Parse(item.effects[0].effect));
                transform.localScale += Vector3.one * float.Parse(item.effects[0].effect);
                float scale =  GetComponent<TrailRenderer>().widthMultiplier - 0.12f*PlayerPrefs.GetInt(item.effects[0].name);
                GetComponent<TrailRenderer>().widthMultiplier = scale;
                break;
            }

            case "speed":
            {
                movement.speed += float.Parse(item.effects[0].effect);
                break;
            }
            
            case "heal":
            {
                playerLife.AddLife(1);
                break;
            }

            default:
            {
                Debug.LogError("Effect from player not defined in switch");
                return;
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("Enemy") )
        {
            playerLife.Damage(1);
        }

        if (col.gameObject.CompareTag("PowerUp"))
        {
            col.gameObject.GetComponent<PowerUpBehaviour>().Effect();
        }
    }
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        
        if (col.gameObject.CompareTag("Projectile"))
        {
            playerLife.Damage(1);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        playerLife.Damage(1);
    }

}
