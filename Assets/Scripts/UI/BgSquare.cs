using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgSquare : MonoBehaviour
{

    public Image myImg;

    public void Initialize(Sprite sprite)
    {
        myImg.sprite = sprite;
    }

}
