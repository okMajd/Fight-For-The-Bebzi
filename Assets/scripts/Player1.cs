 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player1 : MonoBehaviour
{

    public float knockback = 1f;
    public float knockbackMultipler;
    public GameObject otherPlayer;

    public float horizontal;
    public float speed;
    public float slowSpeed;
    public float walkSpeed = 3.5f;
    public float runSpeed = 8f;
    public float jumpingPower = 16f;
    bool isFacingRight = true;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    public float boostMultiplierW, boostMultiplierM, boostMultiplierS;
    [Range(1,3)] public int playerNum = 1;
    string horzAxisName = "Horizontal";
    public KeyCode jumpKey, runKey;
    public KeyCode weak, medium, strong, ability, shieldKey, aimKey, pickupKey;

    public GameObject assignedWeapon, assignedAbility;
    public bool beingHit = false, canAttack = true;

    public RawImage healthFast;
    public Animator healthAnim;
    public float healthDecreaseFast;
    public float lives = 3;
    public List<GameObject> lifeIcons = new List<GameObject>();

    public GameObject shield;

    public bool canDoubleJump = true;
    bool speedCanSwitch = false;

    public bool canBeHit = true;

    public keybinds keybinds;
    public TMP_Text username;
    public bool timeSlowed = false;
    KeyCode negativeHorizontal, positiveHorizontal;

    public bool collisionWithItem = false;
    public GameObject itemInCollision;

    public Animator playerAnim;
    public float distanceBeforeLanding;

    public Transform rightArmTop;
    
    public Transform weaponHolder;
    public Transform abilityHolder;

    private void Start()
    {
        keybinds = GameObject.Find("keybindsManager").GetComponent<keybinds>();
        switch (playerNum)
        {
            case 1:
                positiveHorizontal = keybinds.right;
                negativeHorizontal = keybinds.left;
                jumpKey = keybinds.jump;
                runKey = KeyCode.LeftShift;
                aimKey = keybinds.aim;
                username.text = keybinds.username1;
                ability = keybinds.ability;
                pickupKey = keybinds.pickup;
                break;
            case 2:
                positiveHorizontal = keybinds.right2;
                negativeHorizontal = keybinds.left2;
                jumpKey = keybinds.jump2;
                runKey = KeyCode.RightShift;
                aimKey = keybinds.aim2;
                username.text = keybinds.username2;
                ability = keybinds.ability2;
                pickupKey = keybinds.pickup2;
                break;
            case 3:
                username.enabled = false;
                this.enabled = false;
                break;
        }
    }

    public void slowTime()
    {
        if(timeSlowed)
            Time.timeScale = 1;
        if(!timeSlowed)
            Time.timeScale = 0.1f;
        timeSlowed = !timeSlowed;
    }

    public float getHorizontalMovement()
    {
        if(Input.GetKey(negativeHorizontal))
        {
            return -1;
        }else if(Input.GetKey(positiveHorizontal))
        {
            return 1;
        }else { return 0; }
        
    }
  
    private void Update()
    {

        Vector2 dir = rightArmTop.position - assignedWeapon.GetComponent<weapon>().handle.transform.position;
        float rot = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

        rightArmTop.rotation = Quaternion.Euler(0, 0, -rot);
        

        if(Input.GetKeyDown(KeyCode.G))
        {
            slowTime();
        }
        float xVal = Mathf.Lerp(healthFast.transform.localScale.x, knockback/100, healthDecreaseFast*Time.deltaTime);
        healthFast.transform.localScale = new Vector3(xVal, 1, 1);
        knockbackMultipler = (knockback/100)*5;
        if(knockbackMultipler < 0.4f)
            knockbackMultipler = 0.4f;
 
        //get horizontal movement
        horizontal = getHorizontalMovement();
        flip();

        if(Input.GetKeyDown(weak) && canAttack)
            attack("weak");
        else if(Input.GetKeyDown(medium) && canAttack)
            attack("medium");
        else if(Input.GetKeyDown(strong) && canAttack)
            attack("strong");

        if(!canAttack)
            assignedWeapon.GetComponent<Animator>().Play("Empty");

        if(Input.GetKeyDown(ability))
        {
            if(assignedAbility != null)
                assignedAbility.GetComponent<ability>().use();
        }
        if(Input.GetKeyDown(pickupKey) && collisionWithItem)
        {
            if(itemInCollision.GetComponent<decideItem>().type == "weapon")
            {
                Destroy(assignedWeapon);
                assignedWeapon = null;
                GameObject newItem = Instantiate(itemInCollision.GetComponent<decideItem>().myItem);
                Destroy(itemInCollision);
                assignedWeapon = newItem;
                assignedWeapon.transform.parent = weaponHolder;
                assignedWeapon.GetComponent<weapon>().myPlayer = this.gameObject;
                assignedWeapon.transform.localScale = new Vector3(assignedWeapon.transform.localScale.x*transform.localScale.x, assignedWeapon.transform.localScale.y, assignedWeapon.transform.localScale.z);
            }else if(!assignedAbility.GetComponent<ability>().beingUsed){
                Destroy(assignedAbility);
                assignedAbility = null;
                GameObject newItem = Instantiate(itemInCollision.GetComponent<decideItem>().myItem);
                Destroy(itemInCollision);
                assignedAbility = newItem;
                assignedAbility.transform.parent = abilityHolder;
                assignedAbility.GetComponent<ability>().myPlayer = this;
                assignedAbility.transform.localScale = new Vector3(assignedAbility.transform.localScale.x*transform.localScale.x, assignedAbility.transform.localScale.y, assignedAbility.transform.localScale.z);
            }
        }

        if(Input.GetKey(runKey))
            speed = runSpeed;
        else if(!speedCanSwitch){ speed = walkSpeed; }

        if(Input.GetKeyDown(shieldKey))
        {
            speed *= 0.1f;
            canBeHit = false;
            assignedWeapon.SetActive(false);
            shield.SetActive(true);
            shield.GetComponent<Animator>().Play("shieldDeploy");
        }
        if(Input.GetKeyUp(shieldKey))
        {
            canBeHit = true;
            assignedWeapon.SetActive(true);
            shield.GetComponent<Animator>().Play("shieldRetrieve");
        }


        if(Input.GetKeyDown(jumpKey) && !isGrounded() && canDoubleJump)
        {
            playerAnim.SetBool("Land", false);
            playerAnim.SetBool("Jump", true);
            playerAnim.SetBool("isWalking", false);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            canDoubleJump = false;
        }


        //if grounded and space pressed set y velocity to jump power
        if(Input.GetKey(jumpKey) && isGrounded())
        {
            playerAnim.SetBool("Land", false);
            playerAnim.SetBool("Jump", true);
            playerAnim.SetBool("isWalking", false);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        //if player lets go of jump key and velocity is over 0 (meaning midair), slow their velocity
        //this makes it so the longer you press space the higher you jump :D
        if(Input.GetKeyUp(jumpKey) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*0.5f);
        }

        if(Input.GetKeyDown(aimKey))
        {
            if(assignedWeapon.GetComponent<rangedWeapon>() != null)
                assignedWeapon.GetComponent<rangedWeapon>().isAiming = true;
        }
        if(Input.GetKeyUp(aimKey))
        {
            if(assignedWeapon.GetComponent<rangedWeapon>() != null)
                assignedWeapon.GetComponent<rangedWeapon>().isAiming = false;
        }

        controlAnimations();

    }
    public void controlAnimations()
    {
        if(rb.velocity.x > 0.1f || rb.velocity.x < -0.1f && isGrounded())
            playerAnim.SetBool("isWalking", true);
        else
            playerAnim.SetBool("isWalking", false);

        if(playerAnim.GetCurrentAnimatorStateInfo(0).IsName("GoDown") && isGrounded())
            playerAnim.SetBool("Land", true);
        if(playerAnim.GetCurrentAnimatorStateInfo(0).IsName("idle") && rb.velocity.y < 0.1f)
        {
            playerAnim.SetBool("Land", false);
            playerAnim.SetBool("Jump", false);
        }
        if(rb.velocity.y < 0 && !isGrounded())
        {
            playerAnim.Play("GoDown");
        }
    }

    IEnumerator letSpeed()
    {
        speedCanSwitch = true;
        yield return new WaitForSeconds(0.3f);
        speedCanSwitch = false;
    }

    private void attack(string type)
    {
        switch (type)
        {
            case "weak":
                assignedWeapon.GetComponent<weapon>().attack("weak");
                StartCoroutine("letSpeed");
                speed *= boostMultiplierW;
                break;
            case "medium":
                assignedWeapon.GetComponent<weapon>().attack("medium");
                StartCoroutine("letSpeed");
                speed *= boostMultiplierM;
                break;
            case "strong":
                assignedWeapon.GetComponent<weapon>().attack("strong");
                StartCoroutine("letSpeed");
                speed *= boostMultiplierS;
                break;
        }


    }

    private void FixedUpdate()
    {
        //set velocity to horizontal movement * speed and y velc.
        if(!beingHit)
        {
            rb.velocity = new Vector2(horizontal*speed, rb.velocity.y);
            Debug.Log("movingm");
        }
    }

    //return true or false by checking if the ground is touched
    bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
    }

    //check which direction facing
    void flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            Vector3 userScale = username.transform.localScale;
            userScale.x *= -1f;
            username.transform.localScale = userScale;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.transform.tag == "item")
        {
            collisionWithItem = true;
            itemInCollision = collider.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.transform.tag == "item")
        {
            collisionWithItem = false;
            itemInCollision = null;
        }   
    }




}
