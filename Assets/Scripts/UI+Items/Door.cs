using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Door : MonoBehaviour {
    public String nextLevel;

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.name == "Player")
        {
            if (nextLevel != null)
            {
                SceneManager.LoadSceneAsync(nextLevel);
            }
            else
            {
                Debug.Log("Next scene not found. Check variable 'nextLevel'.");
            }
        }
    }

}
