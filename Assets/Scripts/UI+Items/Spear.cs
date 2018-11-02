using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour, Consumable {
    SpriteRenderer spriteRenderer;
    public Color normalColor;
    public Color highlightColor;
    private GameObject player;
    public float interactRange = 10f;
    public float itemSize = 10f;
    public InventoryButton lmb;
    public InventoryButton rmb;
    private MousePointer mousePointer;

    //Set by mousePointer, is it close to mouse?
    private bool highlighted;


    private bool glowing;
    public bool pickedUp;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //Finds the gameobject called "Player" and assigns it
        player = GameObject.Find("Player");

        //Finds the gameobject called "MousePointer" and assigns it
        mousePointer = GameObject.Find("MousePointer").GetComponent<MousePointer>();

        highlighted = false;
        glowing = false;
        pickedUp = false;
    }

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
    }



    //This is what happens when the item is used (button released). Called by an InventoryButton
    public void Use()
    {
        GameObject prefab = Resources.Load("Prefabs/Spear") as GameObject; 
        GameObject spear = Instantiate(prefab, player.transform.position + new Vector3(0.0f, 2.0f, 0.0f), prefab.transform.rotation); //viimast parameetrit mitte näppida, muidu objekt pööratakse vale nurga alla (originaal sprite viltu)
        //Vector3 difference = mousePointer.transform.position - spear.transform.position;
        //float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //spear.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        //spear.GetComponent<Rigidbody2D>().AddRelativeForce(Vector3.forward * 10);



        Vector3 mousePos = mousePointer.transform.position;
        Vector3 spearPos = spear.transform.position;
        Debug.Log("Mouse position:" + mousePos);
        Debug.Log("Spear position:" + spearPos);
        float x = spearPos.x - mousePos.x;
        float y = spearPos.y - mousePos.y;
        Debug.Log(x);
        Debug.Log(y);
        //spear.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y)*50);
    }

    public void PickUp()
    {
        //GameObject spear = Instantiate(Resources.Load("Prefabs/Spear") as GameObject, player.transform, true); //intended to display the spear above the player when it is primed, but currently the physics will conflict with the implementation
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
