using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float explosionDuration = 2.0f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player (1)" || collision.gameObject.name == "Player (2)")
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, explosionDuration);
            Destroy(gameObject);

        }
    }
}


