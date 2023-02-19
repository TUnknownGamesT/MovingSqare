using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMovement : MonoBehaviour
{
    public GameObject obj1;
    public SkinPool SkinPool;
    public Sprite currentSprite;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("currentskin"))
        {
            currentSprite = SkinPool.skins[PlayerPrefs.GetInt("currentskin")].texture;
        }
        else
        {
            currentSprite = SkinPool.skins[0].texture;
        }

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
        currentSprite = SkinPool.skins[PlayerPrefs.GetInt("currentskin")].texture;
    }
}
