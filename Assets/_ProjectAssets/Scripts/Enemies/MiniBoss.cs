using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    private Vector3 mainPosition;
    private float speed = 4f;
    private bool wait;
    // Start is called before the first frame update
    public void SetPos(Vector3 finalPoint, bool wait)
    {
        mainPosition = finalPoint;
        this.wait = wait;
        if (wait)
        {
            StartCoroutine(MoveAndWaitRoutine());
        }
    }
    // Update is called once per frame
    void Update()
    {
        // transform.Translate();
        if (!wait)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, mainPosition, Time.deltaTime * speed);
        }
    }
    IEnumerator MoveAndWaitRoutine()
    {
        Vector3 waitpos = mainPosition;
        if (mainPosition.x > 0)
        {
            waitpos.x = -2.1f;
        }
        else
        {
            waitpos.x = 2.1f;
        }
        // Move to target position 1
        while (transform.position != waitpos)
        {
            transform.position = Vector3.MoveTowards(transform.position, waitpos, speed * Time.deltaTime);
            yield return null;
        }

        // Wait for one second
        yield return new WaitForSeconds(1.0f);

        // Move to target position 2
        while (transform.position != mainPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, mainPosition, speed * Time.deltaTime);
            yield return null;
        }
    }
}
