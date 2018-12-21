using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UsableSpear : Consumable
{

    //This is what happens when the item is used (button released). Called by an InventoryButton
    public override void Use()
    {
        Spear spear = GameObject.Instantiate(Resources.Load<Spear>("Prefabs/Spear"), this.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        spear.throwSpear(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        //make sprite on button invisible
        storedImage.color = new Color(storedImage.color.r, storedImage.color.g, storedImage.color.b, 0);
        storedImage.sprite = null;
        Destroy(gameObject);
    }

    //This is what happens when the user starts holding down the button
    public override void Aim()
    {
        Debug.Log("Started aiming");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "WallTrap")
        {
            Physics2D.IgnoreCollision(collision.otherCollider, collision.collider);
            return;
        }
    }

}
