using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenLine : MonoBehaviour
{
    [SerializeField]
    private float minTimeToLive, maxTimeToLive;
    private SpriteRenderer sprite;
    private Color color;
    // Start is called before the first frame update
    void Awake()
    {
        Activate();
    }

    void Activate()
    {
        sprite = transform.GetComponent<SpriteRenderer>();
        color = new Vector4(1, 1, 1, 0);

        //here we make it get visible 
        LeanTween.value(0f, 0.7f, 1f).setOnUpdate(value =>
        {
            color.a = value;
            sprite.color = color;
        }
        ).setDelay(2f).setOnComplete(() =>

            //here it becomes invisible again
            LeanTween.value(0.6f, 0.3f, 1f).setOnUpdate(value =>
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
                 ).setOnComplete(() => {
                     transform.GetComponent<BoxCollider2D>().enabled = true;
                     StartCoroutine(Die());

                     }
                 )
            )
        );
    }
    public IEnumerator Die()
    {
        yield return new WaitForSeconds(Random.RandomRange(minTimeToLive,maxTimeToLive-minTimeToLive));
        LeanTween.value(1f, 0f, 0.5f).setOnUpdate(value =>
        {
            color.a = value;
            sprite.color = color;
        }
        ).setOnComplete(() =>   Destroy(gameObject));
    }
}
