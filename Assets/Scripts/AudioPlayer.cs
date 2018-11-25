using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {

    public AudioClipGroup clinkAudioGroup;
    public AudioClipGroup pickUpAudioGroup;
    public AudioClipGroup stepAudioGroup;
    public AudioClipGroup wooshAudioGroup;
    public AudioClipGroup jumpAudioGroup;
    public static AudioPlayer instance;

	// Use this for initialization
	void Awake () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
