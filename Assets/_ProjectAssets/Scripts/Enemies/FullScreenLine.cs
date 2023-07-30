using System;
using UnityEngine;


public class FullScreenLine : MonoBehaviour
{
    [SerializeField]
    [ColorUsageAttribute(true,true,0f,8f,0.125f,3f)]
    private Color neutralColor;
    [ColorUsageAttribute(true,true,0f,8f,0.125f,3f)]
    [SerializeField] private Color activeColor;


    private Collider2D collider2D;
    private int initIdLeanTween;

    // Start is called before the first frame update
    void Awake()
    {
        collider2D = GetComponent<Collider2D>();
        Init();
    }

    void Init()
    {
        Color c = GetComponent<SpriteRenderer>().color;

       initIdLeanTween = LeanTween.value(0, 1, 1f).setOnUpdate(value =>
        {
            try
            {
                GetComponent<SpriteRenderer>().color = Color.Lerp(c, neutralColor, value);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                LeanTween.cancel(initIdLeanTween);
            }
        }).id;
    }

    [ContextMenu("Test Line")]
    public void Activate()
    {
        Color c = GetComponent<SpriteRenderer>().color;

        LeanTween.value(0, 1, 0.3f).setOnUpdate(value =>
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(c, activeColor, value);
        }).setOnComplete(() => collider2D.enabled = true);
    }

    public void DestroyLaser()
    {
        LeanTween.cancel(gameObject);

        Color c = GetComponent<SpriteRenderer>().color;
        collider2D.enabled = false;

        LeanTween.value(0, 1, 0.3f).setOnUpdate(value =>
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(c, new Vector4(0, 0, 0, 0), value);
        }).setOnComplete(() => Destroy(gameObject));
    }

    private void OnDestroy()
    {
        LeanTween.cancel(gameObject);
    }
}