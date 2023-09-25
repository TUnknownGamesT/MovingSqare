using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : EnemyBehaviour
{

    public GameObject collider;
    public override void UpdateSpeedBasedOnFigure(float speed)
    {
        this.speed = speed +1;
    }
}