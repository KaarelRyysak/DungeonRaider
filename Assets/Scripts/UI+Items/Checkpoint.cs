using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private bool state;
    private RespawnController rc;
    public Animator Animator;


	// Use this for initialization
	void Start () {
        rc = GameObject.FindGameObjectWithTag("RC").GetComponent<RespawnController>();
        state = false;
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") {
            Animator.SetBool("current", true);
            state = true;
            rc.lastCheckPoint = transform.position;
        }
    }
}
