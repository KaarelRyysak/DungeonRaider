using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    //Conditions
    private bool facingRight = true;
    private bool isHit = false;

    //Animations
    Animator anim;

    //Movement
    public float maxSpeed = 100;

    //jump
    public KeyCode jump;
    public KeyCode jumpAlt;
    public float JumpForce = 700f;
    private float timeSinceLastJump;
    private float timeBetweenJumps = 0.1f;

    //Ground comparison
    public LayerMask whatIsGround;
    float groundRadius = 0.01f;
    bool grounded = false;
    public Transform groundCheck;

    //time effect
    private TimeController timeController;
    private PlayerDeathController PlayerDeathController;

    //Other
    private Rigidbody2D rb2D;
    private Vector3 initialTransformPosition;

    // Use this for initialization
    void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        timeSinceLastJump = Time.time;
        timeController = GetComponent<TimeController>();
        PlayerDeathController = GetComponent<PlayerDeathController>();
        initialTransformPosition = this.transform.position;
    }
	
    void resetPlayerPosition()
    {
        rb2D.velocity = Vector3.zero;
        this.transform.position = initialTransformPosition;
    }
	// DeltaTime not needed in FixedUpdate
	void FixedUpdate () {

        if (isHit == true)
        {
            if (facingRight == true)
            { //paremale suunatud
                //deathMove(1);
            }
            else
            { //vasakule suunatud
                //deathMove(-1);
            }
        }
        else {
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
            anim.SetBool("IsJumping", !grounded);

            anim.SetFloat("Speed", rb2D.velocity.y);


            float move = Input.GetAxis("Horizontal");
            rb2D.velocity = new Vector2(move * maxSpeed, rb2D.velocity.y);
            anim.SetFloat("Speed", Mathf.Abs(move));

            ////Change world time according to speed
            //if (Mathf.Abs(move) > 0.4f)
            //{
            //    Time.timeScale = Mathf.Abs(move);
            //}
            //else
            //{
            //    Time.timeScale = 0.4f;
            //}


            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }
        }

	}

    void Update()
    {
        if (isHit == false &&
            Time.time > timeBetweenJumps + timeSinceLastJump &&
            grounded &&
            (Input.GetKeyDown(jump)||Input.GetKeyDown(jumpAlt)))
        {
            //anim.SetBool("IsJumping", true);
            rb2D.AddForce(new Vector2(0, JumpForce));
        }
        
        if (Input.GetMouseButtonDown(0)) //throw spear
        {
            SpawnSpear();
        }
    }

    void Flip()
    {

        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void deathMove(float direction) {
        this.gameObject.transform.position = new Vector3(
            transform.position.x-1f*Time.deltaTime*direction,
            rb2D.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy") {
            anim.SetBool("Hit", true);
            anim.SetBool("IsJumping", false);
            isHit = true;
            SetAllCollidersStatus(false);           
            rb2D.AddForce(new Vector2(-1, 1) * 10, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            anim.SetBool("Hit", false);
            isHit = false;
        }
    }

    private void OnBecameInvisible(){
        anim.SetBool("Hit", false);
        isHit = false;
        SetAllCollidersStatus(true);

        resetPlayerPosition();
    }

    public void SetAllCollidersStatus(bool active){
        foreach (Collider2D c in GetComponents<Collider2D>()){
            c.enabled = active;
        }
    }

    private void SpawnSpear()
    {
        Spear spear = GameObject.Instantiate(Resources.Load<Spear>("Prefabs/Spear"), this.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        spear.throwSpear(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

}
