using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float speed;

    public Vector2 Direction
    {
        set => direction = value;
    }
    
    public Vector2 direction;
    private Rigidbody2D _rb;


    private void Awake()
    {  
        _rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + direction*speed *Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Dead Zone")
            Destroy(gameObject);
    }
}
