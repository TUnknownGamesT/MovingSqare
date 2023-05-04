using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenLine : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Activate();
    }

    void Activate()
    {
        SpriteRenderer sprite = transform.GetComponent<SpriteRenderer>();
        Color color = new Vector4(1, 1, 1, 0);

        //here we make it get visible 
        LeanTween.value(0f, 0.7f, 1f).setOnUpdate(value =>
        {
            color.a = value;
            sprite.color = color;
        }
        ).setOnComplete(() =>

            //here it becomes invisible again
            LeanTween.value(0.6f, 0f, 1f).setOnUpdate(value =>
            {
                color.a = value;
                sprite.color = color;
            }
            ).setOnComplete(() =>

                //here it becomes visible and the collider is activated
                 LeanTween.value(0f, 1f, 0.2f).setOnUpdate(value =>
                 {
                     color.a = value;
                     sprite.color = color;
                 }
                 ).setOnComplete(() =>
                    transform.GetComponent<BoxCollider2D>().enabled = true
                 )
            )
        );
    }
}
