using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMusic : MonoBehaviour {

    [Range(0, 2)]
    public float Volume = 1f;
    [Range(0, 2)]
    public float Pitch = 1f;

    public static BGMusic instance;

    //Always keep this gameObject there
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        Debug.Log("Instance: " + instance);
        
        //If there already is a music player, destroy this thing
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        instance = this;
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        
        Debug.Log(scene.buildIndex);
    }



}
