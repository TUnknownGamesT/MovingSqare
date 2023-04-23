using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class TouchExplosion : MonoBehaviour
{
    public GameObject explosionPrefab;
    public Camera mainCam;
    void Start()
    {
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += SpawnEffect;
    }

    void SpawnEffect(Finger finger)
    {
        Vector2 mousePos = mainCam.ScreenToWorldPoint(finger.screenPosition);
        Instantiate(explosionPrefab, mousePos, Quaternion.identity);
    }
}
