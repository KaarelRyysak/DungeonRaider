using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

    Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        rb2D.interpolation = RigidbodyInterpolation2D.Interpolate;
    }
	
	// Update is called once per frame
	void Update () {

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.9f){
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = Mathf.Abs(Input.GetAxis("Horizontal"));
            }
            
        }
        else
        {
            Time.timeScale = 0.2f;
        }

        //if(Time.fixedDeltaTime > 0.02F * Time.timeScale)
        //{
        //    rb2D.velocity = rb2D.velocity;
        //}
        //Time.fixedDeltaTime = 0.02F * Time.timeScale;

    }
}
