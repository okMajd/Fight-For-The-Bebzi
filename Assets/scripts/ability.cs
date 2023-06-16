using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ability : MonoBehaviour
{
    public float multiplier;
    public Player1 myPlayer;
    public  string type;
    float defaultVal;
    public bool beingUsed = false;
    public virtual void use()
    {
        if(!beingUsed)
            StartCoroutine("boost");
    }

    IEnumerator boost()
    {
        beingUsed = true;
        switch (type)
        {
            case "speed":
                defaultVal = myPlayer.speed;
                myPlayer.speed *= multiplier;
                yield return new WaitForSeconds(5f);
                myPlayer.speed = defaultVal;
                break;
            case "jump":
                defaultVal = myPlayer.jumpingPower;
                myPlayer.jumpingPower *= multiplier;
                yield return new WaitForSeconds(5f);
                myPlayer.jumpingPower = defaultVal;
                break;
            case "timeSlow":
                defaultVal = 1;
                Time.timeScale *= multiplier;
                yield return new WaitForSeconds(2.5f);
                Time.timeScale = defaultVal;
                break;
        }
        beingUsed = false;
    }
}
