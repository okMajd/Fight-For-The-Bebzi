using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setDir : MonoBehaviour
{
    public float dim;
    private void Start()
    {
        dim = transform.localScale.x;
    }
    private void Update()
    {
        transform.localScale = new Vector3(dim, dim, dim);
    } 
}
