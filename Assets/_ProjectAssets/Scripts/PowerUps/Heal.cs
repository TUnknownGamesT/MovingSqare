using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour,IPowerUp
{
    public void Effect()
    {
        PlayerLife playerLife = GameManager.instance.Player.GetComponent<PlayerLife>();
        playerLife.AddLife(1);
        Destroy(gameObject);
    }
}
