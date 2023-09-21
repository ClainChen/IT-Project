using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VGController : MonoBehaviour
{
    public static VGController instance;

    [Serializable]
    public struct Voice
    {
        public string name; 
        public AudioClip voice;
    }

    public List<Voice> Voices;

    private AudioSource voiceSource;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        voiceSource = GetComponent<AudioSource>();
        voiceSource.clip = Voices[0].voice;
        
    }

    public void PlaySound(string name)
    {
        foreach (var voice in Voices)
        {
            if (voice.name == name)
            {
                voiceSource.Stop();
                voiceSource.clip = voice.voice;
                voiceSource.Play();
                Debug.Log($"Start Playing {voice.name}");
                return;
            }
        }
    }

    public void StopSound()
    {
        voiceSource.Stop();
    }
}
