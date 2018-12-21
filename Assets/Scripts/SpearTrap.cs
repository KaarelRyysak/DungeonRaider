using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTrap : MonoBehaviour {
    
    public UsableSpear EnemySpearInstance;
    public GameObject spearSpawn;
    public GameObject spearTarget;
    public int spearNum;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && spearNum > 0)
        {
            spearNum -= 1;
            UsableSpear currentSpear = Instantiate(EnemySpearInstance, spearSpawn.transform.position, spearSpawn.transform.rotation);
            
            EnemySpear currentEnemy = currentSpear.transform.GetChild(0).gameObject.GetComponent<EnemySpear>();

            //DISABLE ALL THE DAMN COLLISIONS
            foreach (Collider2D collider1 in this.gameObject.GetComponentsInChildren<Collider2D>())
            {
                foreach (Collider2D collider2 in currentSpear.gameObject.GetComponentsInChildren<Collider2D>())
                {
                    Physics2D.IgnoreCollision(collider1, collider2);
                }
            }

            currentEnemy.throwSpear(spearTarget.transform.position);
        }
    }
}
