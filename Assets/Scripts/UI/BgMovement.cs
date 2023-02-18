using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMovement : MonoBehaviour
{
    public GameObject obj1, obj2;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in obj1.transform)
        {
            if (null == child)
                continue;
            //child.gameobject contains the current child you can do whatever you want like add it to an array
            StartCoroutine(WaitASec());
            LeanTween.move(child.gameObject, this.transform.GetChild(Random.Range(0, 11)).transform.position, Random.RandomRange(4f, 7f));
            StartCoroutine(ResetDest(child.gameObject));
        }
    }
    IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(2f);
    }
    IEnumerator ResetDest(GameObject obj)
    {
        yield return new WaitForSeconds(6f);
        LeanTween.move(obj, this.transform.GetChild(Random.Range(0, 11)).transform.position, Random.RandomRange(4f, 7f));
        StartCoroutine(ResetDest(obj));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
