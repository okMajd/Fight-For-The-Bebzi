using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endPlayer : MonoBehaviour
{
    public float xMin, xMax;
    public float y;
    public float speed;
    public AudioSource sfx;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("attackable"))
        {
            Player1 pl = collider.GetComponent<Player1>();
            pl.knockback = 1;
            pl.enabled = false;
            StartCoroutine(respawn(collider.gameObject));
            collider.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
            pl.lives--;
            int index = pl.lifeIcons.Count;
            index--;
            GameObject toDestroy = pl.lifeIcons[index];
            pl.lifeIcons.RemoveAt(index);
            Destroy(toDestroy);
            //sfx.Play();
        }
    }

    IEnumerator respawn(GameObject player)
    {
        yield return new WaitForSeconds(1f);
        Vector2 spawnPoint = new Vector2(Random.Range(xMin, xMax), y);
        player.transform.position = spawnPoint;
    }
}
