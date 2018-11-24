using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RuneTeleport : Consumable {

    //This is what happens when the item is used (button released). Called by an InventoryButton
    public override void Use()
    {
        Debug.Log("used teleport");


        Destroy(gameObject);
    }

    //This is what happens when the user starts holding down the button
    public override void Aim()
    {
        Debug.Log("Started aiming");
    }

}
