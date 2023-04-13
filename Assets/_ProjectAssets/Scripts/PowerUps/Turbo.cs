using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbo : PowerUpBehaviour
{

    public override void  Effect()
    {
        Transform player = GameManager.instance.Player;
        player.GetComponent<Movement>().speed +=
            Int32.Parse(item.effects[PlayerPrefs.GetInt(item.effects[0].name)].effect);
        
        StartCoroutine(DestroyTurboEffect());

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

    }

    
    
    private IEnumerator DestroyTurboEffect()
    {
        yield return new WaitForSeconds(effectTime);
        Movement playerMovement = GameManager.instance.Player.GetComponent<Movement>();
        playerMovement.speed -= 1;
        
        Destroy(gameObject);
    }

  
    
}
