using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using Unity.VisualScripting;
using static UnityEditor.Progress;
using System;

public class ReadGoogleSheet : MonoBehaviour
{
    public string url;
    public string urlCar;
    public string urlGunUpgrade;
    public string urlPassiveUpgrade;
    public string urlGunBaseSettings;
    public string urlTalents;


    private void Start()
    {
        if (!PlayerPrefs.HasKey("loadCar"))
        {
            StartCoroutine(ObtainSheetData(urlCar, "car"));
            PlayerPrefs.SetInt("loadCar", 1);
        }

        StartCoroutine(ObtainSheetData(urlGunUpgrade, "gunUpgrade"));

        StartCoroutine(ObtainSheetData(urlPassiveUpgrade, "passiveUpgrade"));
        
        StartCoroutine(ObtainSheetData(urlGunBaseSettings, "gunBaseSettings"));

        StartCoroutine(ObtainSheetData(urlTalents, "talents"));
    }

    IEnumerator ObtainSheetData(string _url, string _bdName)
    {
        UnityWebRequest www = UnityWebRequest.Get(_url);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("ERROR: " + www.error);
        }
        else
        {
            string json = www.downloadHandler.text;

            if (_bdName == "car")
                GetComponent<BDLoaderController>().LoadCarInfo(json);

            if (_bdName == "gunUpgrade")
                GetComponent<BDLoaderController>().LoadGunUpgradeInfo(json);

            if (_bdName == "passiveUpgrade")
                GetComponent<BDLoaderController>().LoadPassiveUpgradeInfo(json);

            if (_bdName == "gunBaseSettings")
                GetComponent<BDLoaderController>().LoadGunBaseSettingsInfo(json);

            if (_bdName == "talents")
                GetComponent<BDLoaderController>().LoadTalentsInfo(json);
        }
    }
}
