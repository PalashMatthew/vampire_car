using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    AudioSource aud;

    public AudioClip clipButtonClick;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        aud.PlayOneShot(clip, 0.7f);
    }

    public void PlayButtonSound()
    {
        aud.PlayOneShot(clipButtonClick, 0.7f);
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("soundSettings") == 1)
        {
            aud.volume = 0.15f;
        }
        else
        {
            aud.volume = 0;
        }
    }
}
