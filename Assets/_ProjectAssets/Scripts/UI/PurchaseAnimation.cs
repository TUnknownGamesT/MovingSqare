using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseAnimation : MonoBehaviour
{
    [SerializeField]
    private float animationTime;

    [SerializeField] private RawImage skin;
    private void OnEnable()
    {
        StartCoroutine(StopAnimation());
    }

    public IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(animationTime);
        gameObject.SetActive(false);
    }

    public void SetSkin(Sprite newSprite)
    {
        skin.texture = newSprite.texture;
    }
}
