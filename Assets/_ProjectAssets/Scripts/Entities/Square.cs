using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : EnemyBehaviour,IEnemyStages
{
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
                StartCoroutine(SpawnMiniCubes());
                break;
            }
        }
    }

    IEnumerator SpawnMiniCubes()
    {
        yield return new WaitForSeconds(3f);
        particleSystem[0].SetActive(true);
    }
}

