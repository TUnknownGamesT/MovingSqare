using System.Collections;
using UnityEngine;

public class Turbo : PowerUpBehaviour
{

    public override void  Effect()
    {
        Transform player = GameManager.instance.Player;

        player.GetComponent<Movement>().speed += item.GetEffect(item.effects[0].name);
        player.GetComponent<TrailRenderer>().enabled = true;
        StartCoroutine(DestroyTurboEffect());

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

    }
    
    
    private IEnumerator DestroyTurboEffect()
    {
        yield return new WaitForSeconds(effectTime);
        Movement playerMovement = GameManager.instance.Player.GetComponent<Movement>();
        playerMovement.speed -= item.GetEffect(item.effects[0].name);
        
        Transform player = GameManager.instance.Player;
        player.GetComponent<TrailRenderer>().enabled = false;
        
        Destroy(gameObject);
    }

}
