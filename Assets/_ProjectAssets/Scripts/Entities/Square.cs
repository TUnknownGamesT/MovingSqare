using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : EnemyBehaviour
{
    public override void UpdateSpeedBasedOnFigure(float speed)
    {
        this.speed = speed+2;
    }
}

