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
}
