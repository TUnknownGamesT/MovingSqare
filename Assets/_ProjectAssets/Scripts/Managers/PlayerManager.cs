using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public PlayerLife playerLife;
    public Movement movement;
    public CameraShaking cameraShaking;


    private void OnEnable()
    {
        AdsManager.onAdFinish += Revive;
    }

    private void OnDisable()
    {
        AdsManager.onAdFinish -= Revive;
    }


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
                Debug.Log("Effect from player not defined in switch");
                return;
            }
        }
    }
    
    
    private void Revive()
    {
        transform.position = Vector3.zero;
        playerLife.AddLife(1);
        GetComponent<BoxCollider2D>().enabled = false;
        LeanTween.value(1, 0.5f, 0.3f).setOnUpdate(value =>
        {
            Color c = GetComponent<SpriteRenderer>().color;
            c.a = value;
            GetComponent<SpriteRenderer>().color = c;
        }).setLoopCount(10).setEaseInOutCubic().setLoopPingPong()
            .setOnComplete(()=> GetComponent<BoxCollider2D>().enabled = true);
    }
    
    
    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("Enemy"))
        {
            playerLife.Damage(1);
            SoundManager.instance.PlaySoundEffect(Constants.Sounds.PlayerGetHit);
            cameraShaking.Shake();
        }

        if (col.gameObject.CompareTag("PowerUp"))
        {
            col.gameObject.GetComponent<PowerUpBehaviour>().Effect();
        }

       
    }
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Projectile"))
        {
            SoundManager.instance.PlaySoundEffect(Constants.Sounds.PlayerGetHit);
            playerLife.Damage(1);
            cameraShaking.Shake();
        }
        
        if (col.gameObject.CompareTag("Obstacle"))
        {
            playerLife.Damage(1);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        SoundManager.instance.PlaySoundEffect(Constants.Sounds.PlayerGetHit);
        playerLife.Damage(1);
        cameraShaking.Shake();
    }

    private void OnBecameInvisible()
    {
        playerLife.Damage(playerLife.Life);
    }
}
