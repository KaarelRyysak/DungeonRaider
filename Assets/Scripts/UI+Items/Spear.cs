﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour {
    Rigidbody2D rgbd2D;
    bool stuck;
    private Vector3 Zero = Vector3.zero;
    public int ForwardForce = 50;

	void Awake () {
        rgbd2D = GetComponent<Rigidbody2D>();
        rgbd2D.interpolation = RigidbodyInterpolation2D.Interpolate;
        //Vector3 UpVector = this.transform.up;
        //Vector3 ForwardVector = Quaternion.AngleAxis(45, Vector3.up) * UpVector;
        //rgbd2D.AddForce(ForwardVector * ForwardForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if (!stuck) {
            this.transform.up = Vector3.SmoothDamp(this.transform.up, rgbd2D.velocity, ref Zero, 1f);
         }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PolygonCollider2D[] PolygonColliders = this.GetComponents<PolygonCollider2D>();
        PolygonCollider2D Collider = (PolygonCollider2D) collision.otherCollider; //Võib lisada try catch ploki
        int PointCount = Collider.points.Length;

        if (collision.gameObject.tag == "Enemy") //et ei takistaks vastaste liikumist
        {
            if (!stuck)
            {
                //TODO: murderize enemy
            }

            return;
        }
        if (PointCount == 3) // TABATI ODA TIPPU
        {
            replaceSpear();
        }
        else // TABATI ODA TÜVE VÕI PEA LAIEMAT OSA (ST KÜLJE POOLT)
        {
            Invoke("replaceSpear", 3f); // TODO: hetkel vahetatakse füüsiline oda UI oma vastu välja 3 sekundit hiljem, aga see peaks toimuma siis kui oda seisma jäänud.
        }
    }

    //Annab tõuke targetPos suunas.
    public void throwSpear(Vector3 mousePosition)
    {
        Vector3 diff = mousePosition  - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        Vector3 UpVector = this.transform.up;
        Vector3 ForwardVector = Quaternion.AngleAxis(45, Vector3.up) * UpVector;
        rgbd2D.AddForce(ForwardVector * ForwardForce, ForceMode2D.Impulse);
    }

    private void spawnUISpear()
    {
        GameObject.Instantiate(Resources.Load<UsableSpear>("Prefabs/UsableSpear"), this.transform.position, this.transform.rotation);
    }

    private void replaceSpear()
    {
        this.gameObject.SetActive(false);
        spawnUISpear();
        GameObject.Destroy(this);
    }


}
