using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : EnemyBehaviour,IEnemyStages
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
                particleSystem[0].gameObject.SetActive(true);
                break;
            }
        }
    }
}
