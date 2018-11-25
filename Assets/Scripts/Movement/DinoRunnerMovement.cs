using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoRunnerMovement : MonoBehaviour {

    public Animator animator;

    public float runSpeed;
    public int facingRight; //1 or -1

    private Rigidbody2D rb2D;

    // Use this for initialization
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        rb2D.interpolation = RigidbodyInterpolation2D.Interpolate;

        if (facingRight == -1)
            Flip();

    }

    // Update is called once per frame
    void Update()
    {
       transform.position = new Vector3(
            transform.position.x + runSpeed * Time.deltaTime * facingRight,
            transform.position.y);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Mouse")
        {
            Flip();
        }
        
    }





    private void Flip()
    {
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        facingRight *= -1;
    }
}
