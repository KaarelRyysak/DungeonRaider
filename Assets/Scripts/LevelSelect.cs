using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {

    public GameObject levelButtonPrefab;

	// Use this for initialization
	void Start () {
        int i = 1;

        
		while (i < 8)
        {


            GameObject button = Instantiate(levelButtonPrefab);
            button.transform.parent = gameObject.transform;

            int num = i;
            Button buttonScr = button.GetComponent<Button>();
            buttonScr.onClick.AddListener(delegate { click(num); });

            Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
            buttonText.text = "Level " + i;

            i += 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void click(int i)
    {
        SceneManager.LoadScene(i);
    }
}
