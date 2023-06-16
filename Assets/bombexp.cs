using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombexp : MonoBehaviour
{
    public GameObject explosionPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("attackable"))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

