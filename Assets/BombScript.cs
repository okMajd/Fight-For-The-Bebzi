using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour {

    public float knockbackForce = 100f;

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("attackable")) {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null) {
                // Apply knockback force in a random left or right direction
                Vector3 knockbackDirection = new Vector3(Random.Range(-1f, 1f), 0f, 0f).normalized;
                rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            }
        }
    }
}

