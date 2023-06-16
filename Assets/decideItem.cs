using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decideItem : MonoBehaviour
{
    [HideInInspector] public List<GameObject> possibleItems = new List<GameObject>();
    public GameObject myItem;
    public string type;
    public float dieSpeed;
    public GameObject deathParticle;
    public Vector3 originalSize;
    private void Start()
    {
        myItem = possibleItems[Random.Range(0, possibleItems.Count)];
        if(myItem.GetComponent<weapon>() != null)
            type = "weapon";
        else
            type = "ability";
        this.gameObject.GetComponent<SpriteRenderer>().sprite = myItem.GetComponent<SpriteRenderer>().sprite;
        this.transform.localScale = myItem.transform.localScale*1.5f;
        originalSize = transform.localScale;
        this.gameObject.AddComponent<BoxCollider2D>();
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime/dieSpeed);
        float percentCurrent = (transform.localScale.x/originalSize.x)*100;
        if(percentCurrent <= 45)
        {
            Destroy(this.gameObject);
            Instantiate(deathParticle, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
            this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
