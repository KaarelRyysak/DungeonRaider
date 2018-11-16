using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour {
    Rigidbody2D rgbd2D;
    bool stuck;
    private Vector3 Zero = Vector3.zero;
    public int ForwardForce = 50;

	void Start () {
        rgbd2D = GetComponent<Rigidbody2D>();
        Vector3 UpVector = this.transform.up;
        Vector3 ForwardVector = Quaternion.AngleAxis(45, Vector3.up) * UpVector;
        rgbd2D.AddForce(ForwardVector * ForwardForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if (!stuck) {
            this.transform.up = Vector3.SmoothDamp(this.transform.up, rgbd2D.velocity, ref Zero, 1f);
         }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") //et ei takistaks vastaste liikumist
        {
            if (!stuck)
            {
                //TODO: murderize enemy
            }

            return;
        }

        this.gameObject.SetActive(false);
        spawnUISpear();
        GameObject.Destroy(this);


        //TODO: replace this line with destroying gameobject

    }

        //Annab tõuke targetPos suunas.
    public void throwSpear(Vector3 targetPos)
    {
        //TODO: Write the actual code
    }

    private void spawnUISpear()
    {
        GameObject.Instantiate(Resources.Load<UsableSpear>("Prefabs/UsableSpear"), this.transform.position, this.transform.rotation);
    }


}
