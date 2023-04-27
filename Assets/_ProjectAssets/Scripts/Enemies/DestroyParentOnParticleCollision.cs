using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParentOnParticleCollision : MonoBehaviour
{
    public GameObject explosionPrefab;
    private void OnParticleCollision(GameObject other)
    {
        Destroy(transform.parent.gameObject);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
