using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour {

    public List<InventoryButton> buttons;
    private GameObject player;
    public float maxRadius = 0.5f;
    public GameObject cursor;

	// Use this for initialization
	void Start () {
        //Finds the gameobject called "Player" and assigns it
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {


        //See on hiire asukoht maailma koordinaatides
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        /* See on mingi asi, mis Jaggo koodist sai kopeeritud ja mis ei tööta eriti
        
        Bounds bounds = OrthographicBounds(Camera.main);

        Vector3 worldPoint2 = new Vector3(
            Mathf.Clamp(worldPoint.x, bounds.min.x, bounds.max.x),
            Mathf.Clamp(worldPoint.x, bounds.min.x, bounds.max.x),
            0
            );

        worldPoint2 = Vector3.ClampMagnitude(worldPoint2, maxRadius);
        */

        //Liigutame hiire kursorit
        cursor.transform.position = new Vector3(worldPoint.x, worldPoint.y, cursor.transform.position.z);
        
        //Vektor, mis läheb mängijast hiire positsiooni poole
        Vector3 playerToMouse = worldPoint - player.transform.position;

        //Ignoreerime z-koordinaadi muudatust
        playerToMouse = new Vector3(playerToMouse.x, playerToMouse.y, 0f);

        //Piirame vektori suurust, et see mängija lähedal oleks
        playerToMouse = Vector3.ClampMagnitude(playerToMouse, maxRadius);
        
        //Kasutame vektorit, et liigutada selectorit
        transform.position = player.transform.position + playerToMouse;
        
    }

    public Bounds OrthographicBounds(Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(camera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Consumable" || collision.gameObject.tag == "UISpear")
        {
            collision.gameObject.GetComponent<Consumable>().Highlight();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Consumable" || collision.gameObject.tag == "UISpear")
        {
            collision.gameObject.GetComponent<Consumable>().Unlight();
        }
    }
}
