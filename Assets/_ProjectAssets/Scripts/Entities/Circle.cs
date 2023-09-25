using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Circle : EnemyBehaviour
{
    
    public GameObject aoe;

    public override void UpdateSpeedBasedOnFigure(float speed)
    {
        this.speed = speed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Dead Zone")
        {
            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.PlaySoundEffect(Constants.Sounds.DestroyEnemy);
            Destroy(gameObject);
            Instantiate(deadEffect, new Vector3(col.contacts[0].point.x,
                col.contacts[0].point.y,-8) ,Quaternion.identity);
        }
    }
}
