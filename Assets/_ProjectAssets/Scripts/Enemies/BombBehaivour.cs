using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaivour : MonoBehaviour
{
    public GameObject sprite;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        sprite.transform.localPosition = new Vector3(0, sprite.transform.localPosition.y + 0.2f, 0);
        if (sprite.transform.localPosition.y > -0.1)
        {
            Destroy(GameObject.Find("Boss1"));
            Destroy(this.gameObject);
        }
    }
}
