using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreen : MonoBehaviour
{
    private Vector3 mainPosition, startPosition;
    public float time=0.1f;
    private bool vertical;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        if (startPosition.y > 4)
        {
            vertical = true;
            mainPosition = startPosition;
            mainPosition.y = 0;
            this.gameObject.transform.localScale = new Vector3(2, 10, 1);
        }
        else {
            vertical = false;
            mainPosition = startPosition;
            mainPosition.x = 0;
            this.gameObject.transform.localScale = new Vector3(7, 2, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, mainPosition, time);
        StartCoroutine(leave());
    }
    IEnumerator leave()
    {
        yield return new WaitForSeconds(1f);
        if (!vertical)
        {
            mainPosition.x -= startPosition.x;
        }
        else
        {
            mainPosition.y -=  startPosition.y;
        }
        
        Destroy(this.gameObject);
    }
}
