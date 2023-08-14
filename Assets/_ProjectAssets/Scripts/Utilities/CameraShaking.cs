using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaking : MonoBehaviour
{
    private void OnEnable()
    {
        Boomerang.onBoomerangHit += Shake;
    }

    private void OnDisable()
    {
        Boomerang.onBoomerangHit -= Shake;
    }

    public void Shake()
    {
        LeanTween.moveX(gameObject, 0.3f, 0.03f).setEasePunch().setOnComplete(() =>
        {
            LeanTween.moveY(gameObject, 0.3f, 0.03f).setEasePunch().setOnComplete(() =>
            {
                LeanTween.moveX(gameObject, -0.3f, 0.03f).setEasePunch().setOnComplete(() =>
                {
                    LeanTween.moveY(gameObject, -0.3f, 0.03f).setEasePunch();
                });
            });
        });
    }
}
