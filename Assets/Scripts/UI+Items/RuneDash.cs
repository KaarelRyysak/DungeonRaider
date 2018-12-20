using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RuneDash : Consumable {
    
    [Header("Item settings")]
    [SerializeField]
    [Tooltip("Force of the teleport")]
    public float TeleForce;

    private int charges;

    
    public Sprite damagedSprite;



    private void Awake()
    {
        charges = 2;
    }

    //This is what happens when the item is used (button released). Called by an InventoryButton
    public override void Use()
    {
        Debug.Log("used teleport");


        Vector2 forceVector = new Vector2(mousePointer.transform.position.x - playerRb.transform.position.x,
              mousePointer.transform.position.y - playerRb.transform.position.y);
        forceVector.Normalize();
        Debug.Log(forceVector);

        playerRb.velocity = Vector2.zero;
        playerRb.angularVelocity = 0;
        playerRb.AddForce(forceVector * TeleForce);

        playerRb.gameObject.GetComponent<Player>().teleport();

        AudioPlayer.instance.wooshAudioGroup.Play();

        //Change sprite to be damaged
        storedImage.sprite = damagedSprite;

        //Use charge, destroy if empty
        charges -= 1;
        if(charges <= 0)
        {
            //make sprite on button invisible
            storedImage.color = new Color(storedImage.color.r, storedImage.color.g, storedImage.color.b, 0);
            storedImage.sprite = null;
            Destroy(gameObject);
        }
        
    }

    //This is what happens when the user starts holding down the button
    public override void Aim()
    {
        Debug.Log("Started aiming");
    }

}
