using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

    public static TimeController instance;

    Rigidbody2D rb2D;

    public KeyCode slowMotionShift;

    // Time taken for the transition to slow-mo.
    public float duration = 5.0f;
    public float minNormalTimeHoriz = 0.01f;


    float startTime;



    // Use this for initialization
    void Start () {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        rb2D.interpolation = RigidbodyInterpolation2D.Interpolate;
        startTime = Time.unscaledTime-duration;
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {

        float t = (Time.unscaledTime - startTime) / duration;


        if (Input.GetKey(slowMotionShift) || Mathf.Abs(Input.GetAxis("Horizontal")) < minNormalTimeHoriz && Mathf.Abs(Input.GetAxis("Vertical")) < minNormalTimeHoriz)
        {
            Time.timeScale = Mathf.SmoothStep(1f, 0.2f, t);
            //Time.timeScale = 0.2f;
        }
        else
        {
            //if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.9f){
            //    Time.timeScale = 1f;
            //}
            //else
            //{
            //    Time.timeScale = Mathf.Abs(Input.GetAxis("Horizontal"));
            //}
            Time.timeScale = Mathf.SmoothStep(0.2f, 1f, t);
        }

        //if(Time.fixedDeltaTime > 0.02F * Time.timeScale)
        //{
        //    rb2D.velocity = rb2D.velocity;
        //}
        //Time.fixedDeltaTime = 0.02F * Time.timeScale;

    }
}
