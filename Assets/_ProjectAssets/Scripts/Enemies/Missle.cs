using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Missle : MonoBehaviour
{
    
    [SerializeField] private GameObject  absorb;
    private Vector2 destination;
    // Start is called before the first frame update
    void Start()
    {
        destination = new Vector2(Random.Range(-2,2), Random.Range(-2, 2));

        StartCoroutine(Charge());
    }
    
    
    IEnumerator Charge()
    {
        yield return new WaitForSeconds(3f);
        absorb.SetActive(false);
        LeanTween.move(this.gameObject, destination, 4f).setEase(LeanTweenType.easeInOutSine);
        StartCoroutine(Explosion());
    }
    
    
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(2.5f);
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
    
}
