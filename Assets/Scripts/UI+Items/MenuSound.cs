﻿using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class MenuSound {
    public AudioClip clip;
    public string name;
    [Range(0f,1f)]
    public float volume;
    [HideInInspector]
    public AudioSource audioSource;
}
