using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour {

    public List<InventoryButton> buttons;
    private GameObject player;
    public float maxRadius = 0.5f;
    public GameObject cursor;

	void Start () {
        //Finds the gameobject called "Player" and assigns it
        player = GameObject.Find("Player");
	}
	
	void Update ()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Bounds bounds = OrthographicBounds(Camera.main);

        cursor.transform.position = new Vector3(worldPoint.x, worldPoint.y, cursor.transform.position.z);

        /* 
         * !!! These lines put the player and the cursor on different coordinate systems !!!
         * Not sure why they were written - remove them at the end of the project if unused.
         * 
        // C#
        Vector3 v = worldPoint - player.transform.position;
        //v = Vector3.ClampMagnitude(v, maxRadius);
        worldPoint = player.transform.position + v;

        Vector3 playerToMouse = worldPoint - player.transform.position;
        playerToMouse.z = 0f;
        playerToMouse = Vector3.ClampMagnitude(playerToMouse, maxRadius);

        transform.position = player.transform.position + playerToMouse;
        */
        transform.position = worldPoint;
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
