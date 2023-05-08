using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{

    public float speed;

    private bool activateSpawner;
    
    private void Update()
    {
        transform.Translate(new Vector3(0, -speed, 0) * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Dead Zone" && !activateSpawner)
        {
            activateSpawner = !activateSpawner;
            GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>().StartSpawning();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {

        if (col.gameObject.name == "Dead Zone")
        {
            Destroy(gameObject);
        }
    }
}
