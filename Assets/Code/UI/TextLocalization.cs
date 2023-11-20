using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextLocalization : MonoBehaviour
{
    public string textID;
    private string text;

    private void OnEnable()
    {
        PopUpSettings.onLocalization += Initialize;        

        Initialize();
    }

    private void OnDisable()
    {
        PopUpSettings.onLocalization -= Initialize;
    }

    void Initialize()
    {
        text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + textID);

        GetComponent<TMP_Text>().text = text;
    }
}
