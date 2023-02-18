using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject movingZone;

    private void Start()
    {
        var sr = movingZone.GetComponent<SpriteRenderer>();
        if (sr == null) return;

        movingZone.transform.localScale = new Vector3(1, 1, 1);

        var width = sr.sprite.bounds.size.x;
        var height = sr.sprite.bounds.size.y;

        var worldScreenHeight = Camera.main.orthographicSize * 2f;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        movingZone.transform.localScale = new Vector2((float)worldScreenWidth / width,
            (float)worldScreenHeight / height*0.75f);

    }


    [ContextMenu("Test Pause Button")]
    public void Pause()
    {
        Time.timeScale = 0;
    }

    [ContextMenu("Test Resume Button")]
    public void Resume()
    {
        Time.timeScale = 1;
    }

}
