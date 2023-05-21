using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
  public float speed;
  [HideInInspector] public Vector2 moveDir;

  private Rigidbody2D _rb;
  private bool isMoving;
  private PlayerInput _playerInput;
  private Vector2 currentInputValue;
  private Vector2 smoothInputValue;

  private Touch touch;
  

  private void Awake()
  {
    _playerInput = GetComponent<PlayerInput>();
    
    _rb = GetComponent<Rigidbody2D>();
  }

  
  public void Update()
  {
    if (Input.touchCount > 0)
    {
      touch = Input.GetTouch(0);

      if (touch.phase == TouchPhase.Moved)
      {
        transform.position = new Vector2(
          transform.position.x + touch.deltaPosition.x * speed,
          transform.position.y + touch.deltaPosition.y * speed);
      }
    }

  }
  
  private void FixedUpdate()
  {
     /*moveDir = _playerInput.actions["Move"].ReadValue<Vector2>();

    isMoving = Convert.ToBoolean(moveDir.magnitude);
    
    _rb.MovePosition(_rb.position + moveDir*speed*Time.deltaTime);*/
  }
  
}
