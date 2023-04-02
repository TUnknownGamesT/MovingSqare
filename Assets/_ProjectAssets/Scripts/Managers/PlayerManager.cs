using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public PlayerLife playerLife;
    public Movement movement;


    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.name);
        
        if (col.gameObject.CompareTag("Enemy") )
        {
            playerLife.Damage(1);
        }

        if (col.gameObject.CompareTag("PowerUp"))
        {
            col.gameObject.GetComponent<IPowerUp>().Effect();
        }
    }
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        
        if (col.gameObject.CompareTag("Projectile"))
        {
            playerLife.Damage(1);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        playerLife.Damage(1);
    }

}
