using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D Controller;

    float runSpeed = 40f;
    float hMove = 0f;

    bool jump = false;
    bool crouch = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    //Get variables

	void Update () {

        hMove = Input.GetAxisRaw("Horizontal") * runSpeed; //Get left right movement
        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
        

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
