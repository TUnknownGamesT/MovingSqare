using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BackgroundParticlesManager : MonoBehaviour
{
    public Color defColor, actionColor, deathColor;
    public ParticleSystem[] children;
    private LTDescr _tween;
    #region Singleton

    public static BackgroundParticlesManager instance;

    private void Awake()
    {
        instance = FindObjectOfType<BackgroundParticlesManager>();

        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion

    public async void TakeCoinAnimation()
    {
        foreach (var child in children)
        {
            if (_tween!=null)
            {
                _tween.reset();
            }
            _tween = LeanTween.value(0, 1, 1f).setOnUpdate((value) =>
            {
                child.GetComponent<Renderer>().material.color =
                    Color.Lerp(child.GetComponent<Renderer>().material.color, actionColor, value);
            }).setOnComplete(ClearTween);
            await Task.Delay(2000);
            _tween = LeanTween.value(0, 1, 1500f).setOnUpdate((value) =>
            {
                child.GetComponent<Renderer>().material.color =
                    Color.Lerp(child.GetComponent<Renderer>().material.color, defColor, value);
            }).setOnComplete(ClearTween);
        }
    }

    public async void TakeDamageAnimation()
    {
        foreach (var child in children)
        {
            if (_tween!=null)
            {
                _tween.reset();
            }
            _tween = LeanTween.value(0, 1, 1f).setOnUpdate((value) =>
            {
                child.GetComponent<Renderer>().material.color =
                    Color.Lerp(child.GetComponent<Renderer>().material.color, deathColor, value);
            }).setOnComplete(ClearTween);
            await Task.Delay(2000);
            _tween = LeanTween.value(0, 1, 1500f).setOnUpdate((value) =>
            {
                child.GetComponent<Renderer>().material.color =
                    Color.Lerp(child.GetComponent<Renderer>().material.color, defColor, value);
            }).setOnComplete(ClearTween);
        }
    }

    public void StopTween()
    {
        _tween.reset();
        _tween = null;
    }

    private void ClearTween()
    {
        _tween = null;
    }

}
