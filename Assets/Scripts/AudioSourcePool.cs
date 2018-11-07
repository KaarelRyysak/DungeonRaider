using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour {

    private List<AudioSource> audioSources;



    private void Awake()
    {
        audioSources = new List<AudioSource>();
    }

    private IEnumerator WaitAndPrint(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
        //TODO vabasta audio source


    }

    public AudioSource GetSource()
    {
        for(int i=0; i<audioSources.Count; i++)
        {
            if (!audioSources[i].isPlaying)
            {
                return audioSources[i];
            }
        }

        GameObject newObject = new GameObject("AudioSource");
        AudioSource audioSource = newObject.AddComponent<AudioSource>();
        audioSources.Add(audioSource);

        return audioSource;



        return null;
    }
}
