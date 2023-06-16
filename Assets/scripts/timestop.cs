using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timestop : ability
{
    public bool canTimestop = true;
    public GameObject myPlayer;
    Player1 playerScript;
    public Animator anim;

    private void Start()
    {
        playerScript = myPlayer.GetComponent<Player1>();
    }
    public override void use()
    {
        if(canTimestop)
        {
            canTimestop = false;
            GameObject[] stoppables =  GameObject.FindGameObjectsWithTag("attackable");
            foreach (GameObject stoppable in stoppables)
            {
                if(stoppable != myPlayer)
                {
                    Debug.Log(stoppable);
                    stoppable.GetComponent<Player1>().enabled = false;
                    stoppable.GetComponent<Rigidbody2D>().simulated = false;
                }
            }
            anim.Play("timestop");
            StartCoroutine(resumeTime(stoppables));
        }
    }


    IEnumerator resumeTime(GameObject[] stoppables)
    {
        yield return new WaitForSeconds(10f);
        anim.Play("resume");
        foreach (GameObject stoppable in stoppables)
        {
            stoppable.GetComponent<Player1>().enabled = true;
                stoppable.GetComponent<Rigidbody2D>().simulated = true;
        }
        canTimestop = true;
    }
}
