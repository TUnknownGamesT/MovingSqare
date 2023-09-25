using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{

    public static float speed;

    private bool activateSpawner;
    
    private void Update()
    {
        transform.Translate(new Vector3(0, -speed, 0) * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "ObstacleDestroyer")
        {
            GameObject.FindWithTag("SpawnManager").GetComponent<Spawner>().StartSpawning();
            Destroy(gameObject);
        }
    }
}
