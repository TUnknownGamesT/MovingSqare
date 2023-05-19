using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Circle : EnemyBehaviour,IEnemyStages
{
    
    public GameObject aoe;

    private CancellationTokenSource cts;
    public override void Start()
    {
        base.Start();
        cts = new CancellationTokenSource();
        Stage();
    }
    
    public void Stage()
    {
        switch (stage)
        {
            case Stages.first:
            {
                return;
            }
            case Stages.second:
            { 
                Stage2Circle().AttachExternalCancellation(cts.Token);
                break;
            }
        }
    }

    private async UniTask Stage2Circle()
    {
        aoe.SetActive(true);
        
        LeanTween.scale(aoe, Vector2.one, 1f).setEaseInCubic().setOnComplete(() =>
        {
            if (aoe != null)
            {
                aoe.transform.localScale = Vector2.zero;
                aoe.SetActive(false);
            }
        });

        await UniTask.Delay(TimeSpan.FromSeconds(1));

        await Stage2Circle();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Dead Zone")
        {
            Destroy(gameObject);
            cts.Cancel();
        }

        if (col.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.PlaySoundEffect(Constants.Sounds.DestroyEnemy);
            cts.Cancel();
            Destroy(gameObject);
            Instantiate(deadEffect, new Vector3(col.contacts[0].point.x,
                col.contacts[0].point.y,-8) ,Quaternion.identity);
        }
    }
}
