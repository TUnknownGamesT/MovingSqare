using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class PowerUpBehaviour : MonoBehaviour
{
    public float effectTime;
    public Item item;
    
    public abstract void Effect();
}
