using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UsableSlowSpear : Consumable
{

    //This is what happens when the item is used (button released). Called by an InventoryButton
    public override void Use()
    {
        SlowSpear spear = GameObject.Instantiate(Resources.Load<SlowSpear>("Prefabs/SlowSpear"), this.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
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

}
