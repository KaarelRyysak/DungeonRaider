using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearIgnore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Spear" || collision.gameObject.tag == "UISpear")
        {
            Physics2D.IgnoreCollision(collision.otherCollider, collision.collider);
            return;
        }
    }
}
