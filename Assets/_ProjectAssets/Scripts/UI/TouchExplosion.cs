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

    void Start()
    {
        effectsOn = true;
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += SpawnEffect;
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
