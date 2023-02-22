using System;
using System.Security.Cryptography;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static Constants;

public class AttentionSignBehaviour :MonoBehaviour, IMoveAlongAnAxe
{
    public Transform target;
    public Directions enemyDirection;

    private Transform localTransform;
    
    private void Start()
    {
        localTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (target == null)
            return;
        
        MoveAlonAxe();
    }

    public void MoveAlonAxe()
    {
        switch (enemyDirection)
        {
            case Directions.E:
            {
                localTransform.position = Vector2.MoveTowards(transform.position,
                    new Vector2(localTransform.position.x, target.position.y), platformSpeed*Time.deltaTime);
                break;
            }
            case Directions.V:
            {
                localTransform.position = Vector2.MoveTowards(transform.position,
                    new Vector2(localTransform.position.x, target.position.y), platformSpeed*Time.deltaTime);
                break;
            }

            case Directions.W:
            {
                localTransform.position = Vector2.MoveTowards(transform.position,
                    new Vector2(target.position.x,localTransform.position.y), platformSpeed*Time.deltaTime);
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }
}
