using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed;

    private bool isMoving;
    private Vector2 currentInputValue;
    private Vector2 smoothInputValue;
    private Touch touch;
    private bool alive = true;


    private void OnEnable()
    {
        PlayerLife.onPlayerDie += SetAliveFalse;
        AdsManager.onAdFinish += SetAliveTrue;
    }

    private void OnDisable()
    {
        PlayerLife.onPlayerDie -= SetAliveFalse;
        AdsManager.onAdFinish -= SetAliveTrue;
    }

    public void Update()
    {
        if (Input.touchCount > 0 && alive)
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

    private void SetAliveTrue()
    {
        alive = true;
    }

    private void SetAliveFalse()
    {
        alive = false;
    }
}