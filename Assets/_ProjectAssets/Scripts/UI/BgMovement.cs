using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
