using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaking : MonoBehaviour
{
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
