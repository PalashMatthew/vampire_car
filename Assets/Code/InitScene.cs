using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScene : MonoBehaviour
{
    public ASyncLoader loader;


    public void Play()
    {
        loader.LoadLevel("Hub");
    }
}
