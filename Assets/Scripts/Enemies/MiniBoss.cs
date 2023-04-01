using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    private Vector3 mainPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        mainPosition = this.transform.position;
        mainPosition.y = -10f;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, mainPosition, Time.deltaTime * 4);
    }
}
