using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameAnimation : MonoBehaviour
{
    public GameObject bottomLine,player;
    public RawImage[] lives;
    public RawImage moneySign, joystick;
    public TextMeshProUGUI money;

    // Start is called before the first frame update
    void Start()
    {
        
        LeanTween.value(0f, 1f, 2f).setOnUpdate(value => {
            bottomLine.transform.localScale = new Vector2(value, 0.01f);
            joystick.color = new Color(0.3867925f, 0.3867925f, 0.3867925f, value);
            var myColor = new Color(1, 1, 1, value);
            moneySign.color = myColor;
            money.color = myColor;
            lives[0].color = myColor;
            player.GetComponent<SpriteRenderer>().color = myColor;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
