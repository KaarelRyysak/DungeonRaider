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
        Destroy(gameObject);
    }

    //This is what happens when the user starts holding down the button
    public override void Aim()
    {
        Debug.Log("Started aiming");
    }


}
