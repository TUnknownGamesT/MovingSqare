using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public PlayerLife playerLife;
    public Movement movement;


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy")||
            col.gameObject.CompareTag("Projectile"))
        {
            playerLife.Damage(1);
        }

        if (col.gameObject.CompareTag("PowerUp"))
        {
            col.gameObject.GetComponent<IPowerUp>().Effect();
        }
        
    }
    
    
}
