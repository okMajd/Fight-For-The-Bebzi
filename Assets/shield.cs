using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour
{
    public Player1 myPlayer;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("bullet") && !myPlayer.canBeHit)
        {
            Destroy(collider.gameObject);
        }
    }

    public void disable()
    {
        this.gameObject.SetActive(false);
    }
}
