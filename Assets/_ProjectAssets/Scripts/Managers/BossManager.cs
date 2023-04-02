using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject barrier;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawn());
    }

   IEnumerator spawn()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(barrier, new Vector3(-12f,3f,1f), Quaternion.identity);
    }
}
