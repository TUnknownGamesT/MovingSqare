using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class FullScreenLine : MonoBehaviour
{
    [SerializeField]
    private float minTimeToLive, maxTimeToLive;
    private Color color;
    private int id;
    // Start is called before the first frame update
    void Awake()
    {
        Activate();
    }

    void Activate()
    {
        color = new Vector4(1, 1, 1, 0);

            //here we make it get visible 
         id =  LeanTween.value(0f, 0.7f, 0.5f).setOnUpdate(value =>
                {
                    color.a = value;
                    transform.GetComponent<SpriteRenderer>().color = color;
                }
            ).setDelay(0.3f).setOnComplete(() =>
                //here it becomes invisible again
               id=  LeanTween.value(0.6f, 0.3f, 0.5f).setOnUpdate(value =>
                    {
                        color.a = value;
                        transform.GetComponent<SpriteRenderer>().color = color;
                    }
                ).setOnComplete(() =>

                    //here it becomes visible and the collider is activated
                   id =  LeanTween.value(0f, 1f, 0.2f).setOnUpdate(value =>
                        {
                            color.a = value;
                            transform.GetComponent<SpriteRenderer>().color = color;
                        }
                    ).setOnComplete(() => {
                            transform.GetComponent<BoxCollider2D>().enabled = true;
                            StartCoroutine(Die());
                        }
                    ).id
                ).id
            ).id;
    }
    public IEnumerator Die()
    {
        yield return new WaitForSeconds(Random.RandomRange(minTimeToLive,maxTimeToLive-minTimeToLive));
        transform.GetComponent<BoxCollider2D>().enabled = false;
        id =  LeanTween.value(1f, 0f, 0.3f).setOnUpdate(value =>
        {
            color.a = value;
            transform.GetComponent<SpriteRenderer>().color = color;
        }
        ).setOnComplete(() =>Destroy(gameObject)).id;
    }

    private void OnDestroy()
    {
        LeanTween.cancel(id);
    }
}
