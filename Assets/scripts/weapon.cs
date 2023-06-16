using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public string weaponName;
    public string category;
    public Animator anim;
    public GameObject myPlayer;
    public GameObject handle;

    public virtual void attack(string type)
    {

    }
    public IEnumerator allowMovement(Player1 player)
    {
        yield return new WaitForSeconds(0.5f);
        player.beingHit = false;
        player.canAttack = true;
    }

}
