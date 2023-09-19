using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void PauseAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    public void StopAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
