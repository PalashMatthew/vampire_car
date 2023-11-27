using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    AudioSource aud;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        aud.PlayOneShot(clip, 0.7f);
    }
}
