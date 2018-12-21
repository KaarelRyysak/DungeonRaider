using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoSpear : MonoBehaviour
{

    public UsableSpear EnemySpearInstance;
    public GameObject spearSpawn;
    public GameObject spearTarget;
    public int spearNum;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && spearNum > 0)
        {
            //Reduce spear count in trap
            spearNum -= 1;

            //Make new spear
            UsableSpear currentSpear = Instantiate(EnemySpearInstance, spearSpawn.transform.position, spearSpawn.transform.rotation);
            //Get new spear's child
            EnemySpear currentEnemy = currentSpear.transform.GetChild(0).gameObject.GetComponent<EnemySpear>();

            //DISABLE ALL THE DAMN COLLISIONS (between spear and trap)
            foreach (Collider2D collider1 in this.transform.parent.gameObject.GetComponentsInChildren<Collider2D>())
            {
                foreach (Collider2D collider2 in currentSpear.gameObject.GetComponentsInChildren<Collider2D>())
                {
                    Physics2D.IgnoreCollision(collider1, collider2);
                }
            }

            //Throw the spear
            currentEnemy.throwSpear(collision.gameObject.transform.position);

            //Destroy this object, so spear isn't visible anymore
            if(spearNum <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
