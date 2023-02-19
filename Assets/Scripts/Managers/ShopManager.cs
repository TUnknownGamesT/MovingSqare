using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public SkinPool skinPool;
    public GameObject contentFile;
    public Transform container;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<skinPool.skins.Length; i++)
        {
            Instantiate(contentFile, container).GetComponent<ShopElement>().Initialize(skinPool.skins[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
