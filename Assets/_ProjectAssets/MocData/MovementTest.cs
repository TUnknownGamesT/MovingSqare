using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementTest : MonoBehaviour
{
    public float speed;
    
    private Rigidbody2D _rb;
    private Touch _touch;
    private Vector3 tempVect;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Moved)
            {
                tempVect = new Vector2(_touch.deltaPosition.x, _touch.deltaPosition.y);
            }
            else
            {
                tempVect=Vector3.zero;
            }
        }
        else
        {
            tempVect=Vector3.zero;
        }

    }


    public void FixedUpdate()
    {
        tempVect *= speed;

        _rb.MovePosition(transform.position + tempVect);
    }
}
