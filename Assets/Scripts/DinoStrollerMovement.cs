using UnityEngine;

public class DinoStrollerMovement : MonoBehaviour {

    public Animator animator;

    public float runSpeed;
    public float pauseTime;
    public bool isWalking;

    public Waypoint rightWaypoint;
    public Waypoint leftWaypoint;
    public Waypoint currentWaypoint;


    private float startTime;
    private float runSpeedWaypoint;

    private float leftLocalx;
    private float rightLocalx;


    // Use this for initialization
    void Start () {
        startTime = Time.time;
        runSpeedWaypoint = runSpeed;
        rightLocalx = rightWaypoint.transform.localPosition.x;
        leftLocalx = leftWaypoint.transform.localPosition.x;

        if (isWalking == true)
            animator.SetBool("IsWalking", true);
        else
            animator.SetBool("IsWalking", false);

        if (currentWaypoint.tag == "LeftPoint")
            Flip();   
	}
	
	// Update is called once per frame
	void Update () {

        float currentTime = Time.time;

        //stop walking
        if (isWalking == true && Vector3.Distance(transform.position, currentWaypoint.transform.position) <= 0.05f)
        {
            isWalking = false;
            animator.SetBool("IsWalking", false);
            startTime = Time.time;
        }
        
        //walking
        else if (isWalking == true)
        {
            //move dino towards point
            transform.position = Vector3.MoveTowards(
                transform.position,
                currentWaypoint.transform.position,
                Time.deltaTime * runSpeed
                
                );
            //keep right point in place
            rightWaypoint.transform.position = Vector3.MoveTowards(
                rightWaypoint.transform.position,
                transform.position,
                Time.deltaTime * runSpeedWaypoint

                );

            //keep left waypoint in place
            leftWaypoint.transform.position = Vector3.MoveTowards(
                leftWaypoint.transform.position,
                transform.position,
                Time.deltaTime * -runSpeedWaypoint

                );
        }

        //continue walking
        else if (isWalking == false && currentTime - startTime >= pauseTime)
        {
            isWalking = true;
            animator.SetBool("IsWalking", true);
            Flip();
            currentWaypoint = currentWaypoint.GetNextWaypoint();
        }
	}

    private void Flip()
    {
        //get local coordinates for waypoints to properly turn around
        rightLocalx = rightWaypoint.transform.localPosition.x;
        leftLocalx = leftWaypoint.transform.localPosition.x;

        runSpeedWaypoint *= -1;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        //turn around points
        leftWaypoint.transform.localPosition = new Vector3(
            leftLocalx * -1,
            leftWaypoint.transform.localPosition.y,
            leftWaypoint.transform.localPosition.z);

        rightWaypoint.transform.localPosition = new Vector3(
            rightLocalx * -1,
            rightWaypoint.transform.localPosition.y,
            rightWaypoint.transform.localPosition.z);
    }
}
