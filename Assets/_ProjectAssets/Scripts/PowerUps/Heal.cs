using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : PowerUpBehaviour
{
    public override void Effect()
    {
        PlayerLife playerLife = GameManager.instance.Player.GetComponent<PlayerLife>();
        SoundManager.instance.PlaySoundEffect(Constants.Sounds.PickLife);
        playerLife.AddLife(1);
        Destroy(gameObject);
    }
}
