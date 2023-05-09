using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ContentVisualBehaviour : MonoBehaviour
{
    public GameObject scrollbar;

    private float[] pos;
    private  bool _isTouching;
    private float scrollPos = 0;
    private PhoneInput _phoneInput;
    
    private void Awake()
    {
        _phoneInput = new PhoneInput();
        
    }

    private void Start()
    {
        _phoneInput.Enable();
        
        
        _phoneInput.Player.Touching.started += _=> _isTouching= true;
        _phoneInput.Player.Touching.performed += _ => _isTouching = false;
    }
    

    private void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);

         for (int i = 0; i < pos.Length; i++)
             pos[i] = distance * i;
        
        
        scrollPos = scrollbar.GetComponent<Scrollbar>().value;

         for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (distance /  2f) && scrollPos > pos[i] - (distance / 2f))
            {
                ShopManager.instance.SetDetails(i);
            }
        }
    }
    
    
    
}
