using UnityEngine.Audio;
using UnityEngine;
using System;

public class MenuAudioManager : MonoBehaviour {
    public MenuSound[] sounds;
    private void Awake()
    {
        foreach (MenuSound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
        }
    }
    public void Play(string soundName)
    {
        MenuSound menuSound = Array.Find(sounds, sound => sound.name == soundName);
        menuSound.audioSource.Play();
    }
}
