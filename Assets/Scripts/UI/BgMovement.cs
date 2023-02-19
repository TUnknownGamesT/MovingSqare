using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMovement : MonoBehaviour
{
    public GameObject obj1;
    // Start is called before the first frame update
    void Start()
    {
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
        yield return new WaitForSeconds(8f);
        LeanTween.move(obj, transform.GetChild(Random.Range(0, 11)).transform.position, 
            Random.Range(4f, 7f));
        StartCoroutine(ResetDest(obj));
    }
   
}
