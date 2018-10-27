using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoWalkerMovement : MonoBehaviour {

    public Animator animator;

    public float runSpeed;
    public float pauseTime;
    public bool isWalking;
    public int facingRight; //1 or -1

    private float startTime;

    private Rigidbody2D rb2D;

    // Use this for initialization
    void Start () {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        rb2D.interpolation = RigidbodyInterpolation2D.Interpolate;

        animator.SetBool("IsTrigger", false);
        startTime = Time.time;

        if (isWalking == true)
            animator.SetBool("IsWalking", true);
        else
            animator.SetBool("IsWalking", false);

        if (facingRight == -1)
            Flip();

    }
	
	// Update is called once per frame
	void Update () {
        float currentTime = Time.time;

        //on contact with a wall, activate stand mode

        //walking
        if (isWalking == true)
        {
            transform.position = new Vector3(
                transform.position.x + runSpeed * Time.deltaTime * facingRight,
                transform.position.y);
        }

        else if (isWalking == false && currentTime - startTime >= pauseTime) {
            Flip();
            isWalking = true;
        }
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Do things to contact item, such as player
        isWalking = false;
        animator.SetBool("IsTrigger", true);
        animator.SetBool("IsWalking", false);

        startTime = Time.time;
        

    }






    private void Flip()
    {
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        facingRight *= -1;
        animator.SetBool("IsTrigger", false);
        animator.SetBool("IsWalking", true);
    }

}
