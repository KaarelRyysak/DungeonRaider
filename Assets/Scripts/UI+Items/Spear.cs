using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour {
    Rigidbody2D rgbd2D;
    bool stuck;

	void Start () {
        rgbd2D = GetComponent<Rigidbody2D>();
        rgbd2D.AddForce(new Vector2(30,0), ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (!stuck) {
            this.transform.up = Vector3.Lerp(this.transform.up, rgbd2D.velocity, Time.deltaTime * 10); //up vektor praegu resetib stseenis määratud pöörde, süsteem vaja ümber mõelda.
            Debug.Log("transform.up = " + transform.up +
                "rgbd2D.velocity = " + rgbd2D.velocity);
         }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") //et ei takistaks vastaste liikumist
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<PolygonCollider2D>());
            return;
        }
        rgbd2D.Sleep();
        stuck = true;
    }

    private Vector2 ZRotToForceDir(float zCoord) //Oda "otse" suund tuleb korrektse tõuke andmiseks (addforce) tõenäoliselt arvutada oda pöördest z-telje ümber.
    {


        return new Vector2(0, 0);
    }

}
