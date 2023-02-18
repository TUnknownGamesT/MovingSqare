using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
  public InputManager inputManager;
  public float speed;

  private Rigidbody2D _rb;
  private Vector2 moveDir;
  private bool isMoving;
  private PlayerInput _playerInput;

  private void Awake()
  {
    _playerInput = GetComponent<PlayerInput>();
    _rb = GetComponent<Rigidbody2D>();
  }


  private void Update()
  {
    moveDir = _playerInput.actions["Move"].ReadValue<Vector2>();

    isMoving = Convert.ToBoolean(moveDir.magnitude);

  }


  private void FixedUpdate()
  {
    _rb.MovePosition(_rb.position + moveDir*speed*Time.fixedDeltaTime);
  }
}
