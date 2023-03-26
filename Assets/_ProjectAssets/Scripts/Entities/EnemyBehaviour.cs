using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public enum Stages
{
    first,
    second,
}

public class EnemyBehaviour : MonoBehaviour
{
    public float speed;
    public GameObject deadEffect;
    public Stages stage;
    
    private Rigidbody2D _rb;

    private void Start()
    {
        transform.right = GameManager.instance.PlayerPosition - (Vector2)transform.position;
    }

    private void Awake()
    {  
        _rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        _rb.velocity = transform.right * speed;
    }
    
    

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Dead Zone")
            Destroy(gameObject);

        if (col.gameObject.CompareTag("Enemy"))
        {
            SoundManager.instance.EnemyCollisionSound();
            Destroy(gameObject);
            Instantiate(deadEffect, new Vector3(col.contacts[0].point.x,
                col.contacts[0].point.y,-8) ,Quaternion.identity);
        }
    }
}
