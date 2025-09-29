using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clips;
    public void Playsound(int idx)
    {
        float pitchoffset = Random.Range(0f, .4f);
        audioSource.pitch = .8f + pitchoffset;
        audioSource.PlayOneShot(clips[idx]);
    }
}
