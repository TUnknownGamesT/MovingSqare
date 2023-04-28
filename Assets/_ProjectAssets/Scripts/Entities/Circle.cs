using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Circle : EnemyBehaviour,IEnemyStages
{

    public GameObject aoe;
    public override void Start()
    {
        base.Start();
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
                Stage2Circle();
                break;
            }
        }
    }

    private async UniTask Stage2Circle()
    {
        aoe.SetActive(true);
        
        LeanTween.scale(aoe, Vector2.one, 1f).setEaseInCubic().setOnComplete(() =>
        {
            aoe.transform.localScale = Vector2.zero;
            aoe.SetActive(false);
        });

        await UniTask.Delay(TimeSpan.FromSeconds(1));

        await Stage2Circle();
    }
}
