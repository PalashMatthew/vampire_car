using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseCell : MonoBehaviour
{
    public Sprite spr;
    public Image imgIcon;

    public void Initialize()
    {
        imgIcon.sprite = spr;
    }
}
