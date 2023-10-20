using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class TouchExplosion : MonoBehaviour
{
    public GameObject explosionPrefab;
    public Camera mainCam;
    private bool effectsOn;
    
#if !UNITY_EDITOR


    private void OnEnable()
    {
       
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += SpawnEffect;
    }


    private void OnDisable()
    {
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= SpawnEffect;
    }

    void Start()
    {
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
        effectsOn = true;
    }

    void SpawnEffect(Finger finger)
    {
        Vector2 mousePos = mainCam.ScreenToWorldPoint(finger.screenPosition);
        if (effectsOn) { Instantiate(explosionPrefab, mousePos, Quaternion.identity); }
            
    }
    public void ToogleEffects(bool value)
    {
        effectsOn = value;
    }
#endif
}
