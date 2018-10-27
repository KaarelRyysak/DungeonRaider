using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public float maxSpeed = 100;

    private bool facingRight = true;
    private Rigidbody2D rb2D;

    Animator anim;
    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.02f;
    public LayerMask whatIsGround;

    //private TimeController timeController;

    public KeyCode jump;
    public KeyCode jumpAlt;

    public float JumpForce = 700f;

    private float timeSinceLastJump;
    private float timeBetweenJumps = 0.1f;


	// Use this for initialization
	void Start () {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        timeSinceLastJump = Time.time;
        //timeController = gameObject.GetComponent<TimeController>();

    }
	
	// DeltaTime not needed in FixedUpdate
	void FixedUpdate () {


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


        if(move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }
	}

    private void Update()
    {
        if (Time.time > timeBetweenJumps + timeSinceLastJump &&
             grounded &&
             (Input.GetKeyDown(jump)||Input.GetKeyDown(jumpAlt)))
        {
            anim.SetBool("IsJumping", true);
            rb2D.AddForce(new Vector2(0, JumpForce));
        }
        
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
