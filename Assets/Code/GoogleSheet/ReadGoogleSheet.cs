using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using Unity.VisualScripting;
using System;

public class ReadGoogleSheet : MonoBehaviour
{
    public bool isDownloadData;

    public string urlCar;
    public string urlGunUpgrade;
    public string urlPassiveUpgrade;
    public string urlGunBaseSettings;
    public string urlTalents;
    public string urlDropSystem;
    public string urlLocalization;


    private void Start()
    {
        if (isDownloadData)
        {
            StartCoroutine(ObtainSheetData(urlCar, "car"));

            StartCoroutine(ObtainSheetData(urlGunUpgrade, "gunUpgrade"));

            StartCoroutine(ObtainSheetData(urlPassiveUpgrade, "passiveUpgrade"));

            StartCoroutine(ObtainSheetData(urlGunBaseSettings, "gunBaseSettings"));

            StartCoroutine(ObtainSheetData(urlTalents, "talents"));

            StartCoroutine(ObtainSheetData(urlDropSystem, "dropSystem"));

            StartCoroutine(ObtainSheetData(urlLocalization, "localization"));

            GameObject.Find("InitController").GetComponent<InitScene>().Play();
        }
        else
        {
            ReadDataFile();
        }        
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

            if (_bdName == "dropSystem")
                GetComponent<BDLoaderController>().LoadDropSystemInfo(json);

            if (_bdName == "localization")
                GetComponent<BDLoaderController>().LoadLocalization(json);
        }
    }

    void ReadDataFile()
    {
        TextAsset csv;
        string json;

        //CarInfo
        csv = Resources.Load<TextAsset>("DataAssets/CarSettings");

        json = csv.text;
        GetComponent<BDLoaderController>().LoadCarInfo(json);

        //GunUpgradeInfo
        csv = Resources.Load<TextAsset>("DataAssets/GunUpgrade");

        json = csv.text;
        GetComponent<BDLoaderController>().LoadGunUpgradeInfo(json);

        //PassiveUpgradeInfo
        csv = Resources.Load<TextAsset>("DataAssets/PassiveUpgrade");

        json = csv.text;
        GetComponent<BDLoaderController>().LoadPassiveUpgradeInfo(json);

        //GunBaseSettingsInfo
        csv = Resources.Load<TextAsset>("DataAssets/GunBaseSettings");

        json = csv.text;
        GetComponent<BDLoaderController>().LoadGunBaseSettingsInfo(json);

        //TalentsInfo
        csv = Resources.Load<TextAsset>("DataAssets/Talents");

        json = csv.text;
        GetComponent<BDLoaderController>().LoadTalentsInfo(json);

        //DropSystemInfo
        csv = Resources.Load<TextAsset>("DataAssets/DropSystem");

        json = csv.text;
        GetComponent<BDLoaderController>().LoadDropSystemInfo(json);

        //Localization
        csv = Resources.Load<TextAsset>("DataAssets/Localization");

        json = csv.text;
        GetComponent<BDLoaderController>().LoadLocalization(json);

        GameObject.Find("InitController").GetComponent<InitScene>().Play();
    }
}
