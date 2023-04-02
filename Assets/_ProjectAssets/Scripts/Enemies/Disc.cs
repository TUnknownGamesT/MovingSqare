using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : MonoBehaviour
{
    private Vector3 mainPosition;
    private float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetPos(Vector3 finalPoint)
    {
        mainPosition = finalPoint;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 1, Space.World);
        this.transform.position = Vector3.MoveTowards(this.transform.position, mainPosition, Time.deltaTime * speed);
    }
}
