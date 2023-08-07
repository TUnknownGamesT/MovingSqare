using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BgMovement : MonoBehaviour
{
    public GameObject obj1;
    public ItemsPool itemsPool;
    public Sprite currentSprite;
    public GameObject prefab;
    // Start is called before the first frame update
    
    void Start()
    {
        currentSprite = PlayerPrefs.HasKey("currentSkin") ? itemsPool.items[PlayerPrefs.GetInt("currentSkin")].sprite : itemsPool.items[0].sprite;
        SetBGAnimation();
    }


    private void SetBGAnimation()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(prefab, obj1.transform).GetComponent<BgSquare>().Initialize(currentSprite);
        }
        foreach (Transform child in obj1.transform)
        {
            if (null == child)
                continue;

            LeanTween.move(child.gameObject, transform.GetChild(Random.Range(0, 11)).transform.position,
                Random.Range(4f, 7f));
            StartCoroutine(ResetDest(child.gameObject));
        }
    }
    public void Dissapear()
    {
        foreach (Transform child in obj1.transform)
        {
            if (null == child)
                continue;
            Debug.Log(child.gameObject.name);
          int id = 0;
          id = LeanTween.value(1f, 0f, 1f).setOnUpdate(value => {
              try
              {
                  child.GetComponent<Image>().color = new Color(1, 1, 1, value);
              }
              catch (Exception e)
              {
                  LeanTween.cancel(id);
              }
                
          }).id;
        }
    }
    private IEnumerator ResetDest(GameObject obj)
    {
        yield return new WaitForSeconds(Random.Range(4f, 7f));
        obj.gameObject.GetComponent<BgSquare>().Initialize(currentSprite);
        LeanTween.move(obj, transform.GetChild(Random.Range(0, 11)).transform.position, 
            Random.Range(4f, 7f));
        StartCoroutine(ResetDest(obj));
    }
   public void SetSkin()
   {
        currentSprite = itemsPool.items[PlayerPrefs.GetInt("currentSkin")].sprite;
    }
}
