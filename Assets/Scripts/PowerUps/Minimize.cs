using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimize : MonoBehaviour, IPowerUp
{
    [ContextMenu("Minimize test")]
    public void Effect()
    {
        Transform player=  GameManager.instance.Player;
        player.localScale -= Vector3.one * 0.01f;
    }
}
