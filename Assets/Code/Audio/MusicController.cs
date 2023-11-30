using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    AudioSource aud;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("musicSettings") == 1)
        {
            aud.volume = 0.1f;
        }
        else
        {
            aud.volume = 0;
        }
    }
}
