﻿//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public interface Consumable
//{
//    void Use();
//    void Aim();
//    void Highlight();
//    void Unlight();
//    void PickUp();
//    Sprite GetSprite();
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Consumable : MonoBehaviour
{

    SpriteRenderer spriteRenderer;


    [Header("General options")]
    [SerializeField]
    private Color normalColor;

    [SerializeField]
    private Color highlightColor;

    [SerializeField]
    private float interactRange = 1.5f;

    [SerializeField]
    private float itemSize = 0.2f;

    private GameObject player;
    protected MousePointer mousePointer;
    protected Rigidbody2D rb2d;
    protected Rigidbody2D playerRb;

    [HideInInspector]
    public Image storedImage;

    //Set by mousePointer, is it close to mouse?
    private bool highlighted;


    private bool glowing;

    [HideInInspector]
    public bool pickedUp;

    // Use this for initialization
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //Finds the gameobject called "Player" and assigns it
        player = GameObject.Find("Player");

        //Finds the gameobject called "MousePointer" and assigns it
        mousePointer = GameObject.Find("MousePointer").GetComponent<MousePointer>();

        highlighted = false;
        glowing = false;
        pickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        //If it's highlighted, for each button, if the button is pressed, try to take it
        if (highlighted && !pickedUp)
        {
            foreach (InventoryButton button in mousePointer.buttons)
            {
                if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= interactRange)
                {
                    if (!glowing)
                    {
                        spriteRenderer.color = highlightColor;
                        //TODO: implement highlighting here !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        glowing = true;
                    }

                    if (Input.GetKeyUp(button.keyCode))
                    {
                        if (button.storedImage.sprite == null)
                        {
                            //Make invisible and add to button
                            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
                            button.pickUp(this);
                            rb2d.simulated = false;
                        }
                    }
                }
            }
        }
        else if (glowing && !pickedUp)
        {
            spriteRenderer.color = normalColor;
            //TODO: implement highlighting here !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            glowing = false;
        }
        if (pickedUp)
        {
            transform.position = player.transform.position;
        }
    }



    //This is what happens when the item is used (button released). Called by an InventoryButton
    public virtual void Use()
    {

    }

    //This is what happens when the user starts holding down the button
    public virtual void Aim()
    {

    }


    public void PickUp()
    {
        AudioPlayer.instance.pickUpAudioGroup.Play();
        pickedUp = true;
    }

    public Sprite GetSprite()
    {
        return spriteRenderer.sprite;
    }

    public void Highlight()
    {
        highlighted = true;
    }

    public void Unlight()
    {
        highlighted = false;
    }
}
