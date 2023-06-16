using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float bombForce = 10f;

    private Rigidbody2D playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            Vector2 bombDirection = (Vector2)transform.position - collision.contacts[0].point;
            bombDirection.Normalize();

            playerRigidbody.AddForce(bombDirection * bombForce, ForceMode2D.Impulse);
        }
    }
}

