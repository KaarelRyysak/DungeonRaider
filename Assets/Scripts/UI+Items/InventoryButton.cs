using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour {

    public Consumable stored;
    public Image storedImage;
    public KeyCode keyCode;

    //This tells us if we're already picking it up this frame, 
    //so we don't pick it up and use it at the same time
    private bool pickingUp;

	// Use this for initialization
	void Start () {
        pickingUp = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!pickingUp)
        {
            //If there is an item on the button
            if (stored != null)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    //displaying item aiming stuff
                    stored.Aim();
                }
                if (Input.GetKeyUp(keyCode))
                {
                    //Using the item
                    stored.Use();
                    //make sprite on button invisible
                    storedImage.color = new Color(storedImage.color.r, storedImage.color.g, storedImage.color.b, 0);
                    storedImage.sprite = null;
                    stored = null;
                }
            }
        }
        //If we've picked it up, we're not picking it up anymore
        else
        {
            pickingUp = false;
        }

    }

    public void pickUp(Consumable pickedUp)
    {
        pickingUp = true;
        //Pick it up if it's not there yet
        stored = pickedUp;
        storedImage.sprite = stored.GetSprite();
        //make sprite visible
        storedImage.color = new Color(storedImage.color.r, storedImage.color.g, storedImage.color.b, 1);
        stored.PickUp();
    }
}
