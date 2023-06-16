using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixPlayer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("attackable"))
        {
            Player1 script = collision.gameObject.GetComponent<Player1>();
            script.enabled = true;
            script.canDoubleJump = true;
        }
            
    }
}
