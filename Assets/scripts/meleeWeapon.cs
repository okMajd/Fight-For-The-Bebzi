using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeWeapon : weapon
{
    public bool attacking = false;
    public string lastAttackType;
    public float latestPower;
    public float hitForceWeak, hitForceMedium, hitForceStrong;
    public float upForceWeak, upForceMedium, upForceStrong;
    public float weakPower, mediumPower, strongPower;
    float currentHitForce;
    float currentUpForce;

    public Collider2D col;
    public AudioSource hit;


    private void Update()
    {
        if(attacking)
            col.enabled = true;
        else
            col.enabled = false;
    }

    public override void attack(string type)
    {
        if(!attacking)
        {
            switch (type)
            {
                case "weak":
                    anim.Play("weak");
                    currentHitForce = hitForceWeak;
                    currentUpForce = upForceWeak;
                    latestPower = weakPower;
                    break;
                case "medium":
                    anim.Play("medium");
                    currentHitForce = hitForceMedium;
                    currentUpForce = upForceMedium;
                    latestPower = mediumPower;
                    break;
                case "strong":
                    currentHitForce = hitForceStrong;
                    currentUpForce = upForceStrong;
                    latestPower = strongPower;
                    anim.Play("strong");
                    break;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(attacking && collider.CompareTag("attackable") && collider.gameObject != myPlayer)
        {
            Player1 colPl = collider.GetComponent<Player1>();
            if(!colPl.canBeHit)
                return;
            Vector2 colPoint = collider.ClosestPoint(transform.position);
            float currentDir = myPlayer.transform.localScale.x;
            Debug.Log(currentDir);
            float finalForce = (currentHitForce*currentDir)*colPl.knockbackMultipler;
            Vector2 force = new Vector2(finalForce, currentUpForce);
            colPl.beingHit = true;
            colPl.canAttack = false;
            colPl.assignedWeapon.GetComponent<Animator>().Play("Empty");
            collider.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            StartCoroutine(allowMovement(collider.GetComponent<Player1>()));
            hit.Play();
            colPl.knockback += latestPower;
            colPl.healthAnim.Play("flash");
    
        }
    }
}
