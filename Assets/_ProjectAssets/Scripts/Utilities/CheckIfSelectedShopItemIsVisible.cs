using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckIfSelectedShopItemIsVisible : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y>3.65f)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

       
    }
}
