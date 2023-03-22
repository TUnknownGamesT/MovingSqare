using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbo : MonoBehaviour, IPowerUp
{
    [ContextMenu("Add speed")]
    public void Effect()
    {
        Movement playerMovement = GameManager.instance.Player.GetComponent<Movement>();
        playerMovement.speed += 1;
    }
}
