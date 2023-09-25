using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class PowerUpBehaviour : MonoBehaviour
{
    public float effectTime;
    public Item item;
    
    
    protected void Start()
    {
        Destroy(gameObject,3);
    }
    
    public abstract void Effect();
}
