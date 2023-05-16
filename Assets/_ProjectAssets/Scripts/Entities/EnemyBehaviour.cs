using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float speed;
    public GameObject deadEffect;
    public Stages stage;
    public List<GameObject> particleSystem;
    
    private Rigidbody2D _rb;

    public virtual void Start()
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

    private  void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Dead Zone")
            Destroy(gameObject);

        if (col.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.PlaySoundEffect(Constants.Sounds.DestroyEnemy);
            Destroy(gameObject);
            Instantiate(deadEffect, new Vector3(col.contacts[0].point.x,
                col.contacts[0].point.y,-8) ,Quaternion.identity);
        }
    }
}
