using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {

    public AudioClipGroup clinkAudioGroup;
    public AudioClipGroup pickUpAudioGroup;
    public AudioClipGroup stepAudioGroup;
    public AudioClipGroup wooshAudioGroup;
    public AudioClipGroup jumpAudioGroup;
    public AudioClipGroup teleAudioGroup;
    public AudioClipGroup failedTeleAudioGroup;
    public AudioClipGroup clickGroup;
    public AudioClipGroup deathGroup;

    public static AudioPlayer instance;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
