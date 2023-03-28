using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : EnemyBehaviour, IEnemyStages
{

    public GameObject collider;
    
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
                StartCoroutine(ActivateLasers());
                break;
            }
        }
    }

    IEnumerator ActivateLasers()
    {
        yield return new WaitForSeconds(1f);
        
        foreach (var particle in particleSystem)
        {
            particle.SetActive(true);
        }

        yield return new WaitForSeconds(2f);
        
        collider.SetActive(true);
        
        yield return new WaitForSeconds(2f);

        collider.SetActive(false);
    }
}