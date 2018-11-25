using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Spear : MonoBehaviour {
    Rigidbody2D rgbd2D;
    bool stuck;
    private Vector3 Zero = Vector3.zero;
    public int ForwardForce = 50;
    bool touchedTip = false;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TABATI ODA TIPPU

        if (collision.gameObject.tag == "Enemy")
        {
            Quaternion deadRotation = Quaternion.Euler(0, 0, 90);
            Vector3 deadShift = new Vector3(0, 0.3f, 0);

            // To deal with the (number) stuff when cloning prefabs
            Regex dinostroller = new Regex("DinoStroller.*");
            Regex dinorunner = new Regex("DinoRunner.*");
            Regex dinostatue = new Regex("DinoStatue.*");
            Regex dinowalker = new Regex("DinoWalker.*");

            //Tuvastada vastase tüüp
            if (dinostroller.IsMatch(collision.gameObject.name))
            {
                GameObject.Instantiate(Resources.Load("Prefabs/Enemies/DeadEnemies/DeadDinoStroller"), collision.gameObject.transform.position - deadShift, deadRotation);
            }
            else if (dinorunner.IsMatch(collision.gameObject.name))
            {
                GameObject.Instantiate(Resources.Load("Prefabs/Enemies/DeadEnemies/DeadDinoRunner"), collision.gameObject.transform.position - deadShift, deadRotation);
            }
            else if (dinostatue.IsMatch(collision.gameObject.name))
            {
                GameObject.Instantiate(Resources.Load("Prefabs/Enemies/DeadEnemies/DeadDinoStatue"), collision.gameObject.transform.position - deadShift, deadRotation);
            }
            else if (dinowalker.IsMatch(collision.gameObject.name))
            {
                GameObject.Instantiate(Resources.Load("Prefabs/Enemies/DeadEnemies/DeadDinoWalker"), collision.gameObject.transform.position - deadShift, deadRotation);
            }

            GameObject.Destroy(collision.gameObject);
        }

        touchedTip = true;
        //replaceSpear();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TABATI ODA TÜVE VÕI PEA LAIEMAT OSA (ST KÜLJE POOLT)

        if (collision.gameObject.tag == "Enemy") return;

        if (touchedTip) replaceSpear();
        else
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
        AudioPlayer.instance.clinkAudioGroup.Play();

        this.gameObject.SetActive(false);
        spawnUISpear();
        GameObject.Destroy(this);
    }


}
