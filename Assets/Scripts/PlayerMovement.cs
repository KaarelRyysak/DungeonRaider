using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement instance;
    public CharacterController2D Controller;
    public Animator animator;

    float runSpeed = 40f;
    float hMove = 0f;

    bool jump = false;
    //float vMoveOld;
    //float vMoveNew;
    bool crouch = false;

    //Note: Currently player uses "slippery" material,
    //which means he won't stick to walls when you run into it.
    //If you want that, make a new material with some friction
    //and apply it to him (box and circlecolliders).


	// Use this for initialization
	void Start () {
        instance = this;
        //vMoveNew = transform.position.y;
	}
	
	// Update is called once per frame
    //Get variables

	void Update () {
        //Get left right movement
        hMove = Input.GetAxisRaw("Horizontal") * runSpeed; 
        animator.SetFloat("Speed", Mathf.Abs(hMove));

        //Jump is pressed
        if (Input.GetButtonDown("Jump")) {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        //Get vertical movement
        //vMoveOld = vMoveNew;
        //vMoveNew = transform.position.y;
        //animator.SetFloat("HeightSpeed", vMoveNew - vMoveOld);


        //Crouch is pressed
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
        

	}

    public void onLanding() {
        animator.SetBool("IsJumping", false);
    }

    //Apply variables
    private void FixedUpdate()
    {
        //Move character left/right
        Controller.Move(hMove * Time.fixedDeltaTime, crouch, jump);
        //transform.position = new Vector3(transform.position.x + hMove * Time.fixedDeltaTime, transform.position.y, transform.position.z);
        jump = false;
    }
}
