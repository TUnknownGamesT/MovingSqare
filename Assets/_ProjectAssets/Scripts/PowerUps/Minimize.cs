using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Minimize :  PowerUpBehaviour
{

    [ContextMenu("Minimize test")]
    public override void Effect()
    {
        Transform player=  GameManager.instance.Player;
        TrailRenderer trailRenderer = player.GetComponent<TrailRenderer>();
        
        int upgradeLvl = PlayerPrefs.GetInt(item.effects[0].name);
        
        
        StartCoroutine(DestroyMinimiseEffect());

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        
    }
    
    private IEnumerator DestroyMinimiseEffect()
    {
        yield return new WaitForSeconds(effectTime);
        Transform player=  GameManager.instance.Player;
        TrailRenderer trailRenderer = player.GetComponent<TrailRenderer>();
        
        player.localScale += Vector3.one * 0.01f;
        float scale = trailRenderer.widthMultiplier + 0.12f;
        trailRenderer.widthMultiplier = scale;
        
        Destroy(gameObject);
        
    }

    
    
    
}
