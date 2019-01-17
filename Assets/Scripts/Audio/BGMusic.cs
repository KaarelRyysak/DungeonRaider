using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMusic : MonoBehaviour {

    [Range(0, 2)]
    public float volume = 1f;
    [Range(0, 2)]
    public float pitch = 1f;
    

    public AudioClip[] audioClips;
    public int[] clipStartLevelIndexes;

    public static BGMusic instance;

    private int playingClip = -1;

    private AudioSource source;

    //Always keep this gameObject there
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        source = this.gameObject.GetComponent<AudioSource>();

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
        

        //The clip that is looked at
        int clipIndex = -1;
        //The clip that will be played
        int newSongIndex = 0;
        //For every clip (index is its starting level)
        foreach (int index in clipStartLevelIndexes)
        {
            clipIndex += 1;

            //If we are past its starting point
            if (scene.buildIndex >= index)
            {
                //Play that clip
                newSongIndex = clipIndex;
            }
        }

        //if we aren't already playing the clip and we're not in the menu
        if (playingClip != newSongIndex && scene.buildIndex != 0)
        {
            //Play the clip
            playingClip = newSongIndex;

            Debug.Log(source);

            source.clip = audioClips[newSongIndex];
            source.loop = true;
            source.volume = volume;
            source.pitch = pitch;

            source.Play();
        }
    }
    



}
