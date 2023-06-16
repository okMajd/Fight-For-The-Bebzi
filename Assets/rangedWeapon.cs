using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangedWeapon : weapon
{
    public GameObject bulletPre;
    public float weakForce, mediumForce, strongForce;
    public float bulletForce;
    float latestPower;
    public float cooldown;
    public bool canAttack = true;
    public Transform shootPoint;
    public AudioSource shotSFX;
    public bool isAiming = false;
    public float rotateSpeed;
    private void Start()
    {
        canAttack = true;
    }

    private void Update()
    {
        if(isAiming)
        {
            Vector2 dir = myPlayer.GetComponent<Player1>().otherPlayer.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            transform.rotation =  Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle*-1), rotateSpeed * Time.deltaTime);
        }
    }

    public override void attack(string type)
    {
        if(canAttack)
        {
            switch (type)
            {
                case "weak":
                    anim.Play("weak");
                    latestPower = weakForce;
                    break;
                case "medium":
                    anim.Play("medium");
                    latestPower = mediumForce;
                    break;
                case "strong":
                    anim.Play("strong");
                    latestPower = strongForce;
                    break;
            }
        }
    }

    public void shoot()
    {
        GameObject bullet = Instantiate(bulletPre, shootPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(shootPoint.up*bulletForce, ForceMode2D.Impulse);
        bullet.GetComponent<bullet>().parentGun = this.gameObject.GetComponent<rangedWeapon>();
        // Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), myPlayer.GetComponent<Collider2D>());
    }

    public IEnumerator shootCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

    public void hit(Collider2D collider)
    {
            Player1 colPl = collider.GetComponent<Player1>();
            if(!colPl.canBeHit)
                return;
            Vector2 colPoint = collider.ClosestPoint(transform.position);
            //float currentDir = myPlayer.transform.localScale.x;
            float currentDir = -(shootPoint.position.x - collider.transform.position.x);
            Debug.Log(currentDir);
            float finalForce = (latestPower*currentDir)*colPl.knockbackMultipler;
            Vector2 force = new Vector2(finalForce, 5f);
            colPl.beingHit = true;
            colPl.canAttack = false;
            colPl.assignedWeapon.GetComponent<Animator>().Play("Empty");
            collider.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            StartCoroutine(allowMovement(collider.GetComponent<Player1>()));
            shotSFX.Play();
            colPl.knockback += latestPower;
            colPl.healthAnim.Play("flash");
    }
}
