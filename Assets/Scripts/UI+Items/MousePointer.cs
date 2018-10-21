using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour {

    public List<InventoryButton> buttons;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {



        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Bounds bounds = OrthographicBounds(Camera.main);


        //worldPoint = new Vector3(
        //    Mathf.Clamp(worldPoint.x, bounds.min.x, bounds.max.x),
        //    Mathf.Clamp(worldPoint.x, bounds.min.x, bounds.max.x),
        //    0
        //    );

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
        if(collision.gameObject.tag == "Consumable")
        {
            collision.gameObject.GetComponent<Consumable>().Highlight();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Consumable")
        {
            collision.gameObject.GetComponent<Consumable>().Unlight();
        }
    }
}
