    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public rangedWeapon parentGun;
    private void Start()
    {
        Destroy(this.gameObject, 10f);        
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("attackable") && collider2D.gameObject != parentGun.myPlayer)
        {
            Debug.Log("bullet hit");
            parentGun.hit(collider2D);
            Destroy(this.gameObject);
        }
    }
}
