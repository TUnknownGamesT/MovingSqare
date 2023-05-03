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

        if (_isTouching)
        {
             scrollPos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scrollPos < pos[i] + (distance / 1.7) && scrollPos > pos[i] - (distance / 1.7))
                {
                    scrollbar.GetComponent<Scrollbar>().value =
                        Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.7f);
                }
            }
        }
        
        
        for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (distance / 1.7) && scrollPos > pos[i] - (distance / 1.7))
            {
                transform.GetChild(i).localScale =
                    Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.7f);
                ShopManager.instance.SetDetails(i);

                for(int j = 0;j<pos.Length;j++)
                {
                    if(i!=j)
                        transform.GetChild(j).localScale = 
                            Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                }
                
            }
        }
    }
    
    
    
}
