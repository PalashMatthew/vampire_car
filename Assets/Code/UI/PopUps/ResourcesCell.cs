using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesCell : MonoBehaviour
{
    public Image imgIcon;
    public Sprite sprIcon;

    public int value;
    public TMP_Text tValue;


    public void Initialize()
    {
        tValue.text = "x" + value;
        imgIcon.sprite = sprIcon;
    }
}
