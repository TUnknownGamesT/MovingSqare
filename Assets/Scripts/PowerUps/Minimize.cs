using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Minimize : MonoBehaviour, IPowerUp
{
    [ContextMenu("Minimize test")]
    public void Effect()
    {
        Transform player=  GameManager.instance.Player;
        TrailRenderer trailRenderer = player.GetComponent<TrailRenderer>();
        
        player.localScale -= Vector3.one * 0.01f;
        float scale = trailRenderer.widthMultiplier - 0.12f;
        trailRenderer.widthMultiplier = scale;
        Destroy(gameObject);
    }
}
