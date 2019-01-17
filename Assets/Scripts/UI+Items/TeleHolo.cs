using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleHolo : MonoBehaviour {

    

    private PolygonCollider2D polygonCollider;

    private CompositeCollider2D groundCollider;

    public bool validSpot;
    private int overlappers;

	// Use this for initialization
	void Start () {
        polygonCollider = this.gameObject.GetComponent<PolygonCollider2D>();

        groundCollider = GameObject.Find("Ground TileMap").transform.GetChild(0).GetComponent<CompositeCollider2D>();

        validSpot = true;
        overlappers = 0;

    }
	
	

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "WalkableTerrain")
        {
            overlappers += 1;
            validSpot = false;
            //Debug.Log(collision.gameObject.tag + overlappers);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WalkableTerrain")
        {
            overlappers -= 1;
            if (overlappers == 0)
            {
                validSpot = true;
            }
            //Debug.Log(collision.gameObject.tag + overlappers);
        }
    }

}
