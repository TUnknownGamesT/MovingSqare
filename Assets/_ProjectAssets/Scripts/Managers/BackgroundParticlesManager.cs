using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParticlesManager : MonoBehaviour
{
    public Color defColor, actionColor;
    public ParticleSystem[] children;

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
    
    
    
    public void InitAnimation()
    {
        StartCoroutine(ChangeColor());
    }
    public IEnumerator ChangeColor()
    {
        foreach (var child in children)
        {
            child.GetComponent<Renderer>().material.color=actionColor;
        }

        yield return new WaitForSeconds(1);
        foreach (var child in children)
        {
            child.GetComponent<Renderer>().material.color=defColor;
        }
    }
}
