using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;

public class Minimize :  PowerUpBehaviour
{

    [ContextMenu("Minimize test")]
    public override void Effect()
    {
        Transform player=  GameManager.instance.Player;
        TrailRenderer trailRenderer = player.GetComponent<TrailRenderer>();


        LeanTween.value(player.localScale.x, player.localScale.x + item.GetEffect(item.effects[0].name), 0.2f)
            .setOnUpdate(value =>
            {
                player.localScale = Vector3.one * value;
            }).setEaseInCubic().setOnComplete(() =>
            {
                float scale = trailRenderer.widthMultiplier - 0.5f*PlayerPrefs.GetInt(item.effects[0].name);
                trailRenderer.widthMultiplier = scale;
            });
        
        StartCoroutine(DestroyMinimiseEffect());

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        
    }
    
    private IEnumerator DestroyMinimiseEffect()
    {
        yield return new WaitForSeconds(effectTime);
        Transform player=  GameManager.instance.Player;
        TrailRenderer trailRenderer = player.GetComponent<TrailRenderer>();
        
        LeanTween.value(player.localScale.x, player.localScale.x - item.GetEffect(item.effects[0].name), 0.2f)
            .setOnUpdate(value =>
            {
                player.localScale = Vector3.one * value;
            }).setEaseInCubic().setOnComplete(() =>
            {
                float scale = trailRenderer.widthMultiplier + 0.5f*PlayerPrefs.GetInt(item.effects[0].name);
                trailRenderer.widthMultiplier = scale;
            });
        
        Destroy(gameObject);
        
    }

    
    
    
}
