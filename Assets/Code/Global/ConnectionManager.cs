using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using System;
using UnityEngine.Networking;

public class ConnectionManager : MonoBehaviour
{
    private PopUpController _popUpController;
    public GameObject imgCircle;
    public float rotateSpeed;

    public GameObject headObj;

    public GameObject butReconnect;


    public enum ConnectionStatus
    {
        NotConnected,
        LimitedAccess,
        Connected
    }

    public void Awake()
    {
        DontDestroyOnLoad(headObj);

        _popUpController = GetComponent<PopUpController>();

        PlayerPrefs.SetInt("internet_access", 0);

        StartCoroutine(checkInternetConnection());
    }

    IEnumerator CheckConnect()
    {
        yield return new WaitForSecondsRealtime(1);

        if (Application.loadedLevelName == "InitScene" || Application.loadedLevelName == "Hub")
        {
            if (CheckInternet() != ConnectionStatus.Connected)
            {
                PlayerPrefs.SetInt("internet_access", 0);
                Debug.LogError("Not Connections");
                OpenPopUp();
            }
            else
            {
                PlayerPrefs.SetInt("internet_access", 1);
            }
        }

        yield return new WaitForSecondsRealtime(4);

        //StartCoroutine(CheckConnect());
    }

    IEnumerator checkInternetConnection()
    {
        yield return new WaitForSecondsRealtime(1);

        UnityWebRequest request = new UnityWebRequest("https://google.com");
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            //action(false);
            PlayerPrefs.SetInt("internet_access", 0);
            Debug.LogError("Not Connections");
            OpenPopUp();
            yield break;
        }
        else
        {
            //action(true);
            PlayerPrefs.SetInt("internet_access", 1);
        }

        yield return new WaitForSecondsRealtime(9);

        StartCoroutine(checkInternetConnection());
    }

    public void ButReconnect()
    {
        //if (CheckInternet() == ConnectionStatus.Connected)
        //{
        //    PlayerPrefs.SetInt("internet_access", 1);
        //    StopAllCoroutines();
        //    ClosePopUp();
        //}

        butReconnect.SetActive(false);
        StartCoroutine(checkInternetConnectionAnother());
    }

    IEnumerator checkInternetConnectionAnother()
    {
        UnityWebRequest request = new UnityWebRequest("https://google.com");
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            PlayerPrefs.SetInt("internet_access", 1);
            StopAllCoroutines();
            ClosePopUp();
        }
        else
        {
            butReconnect.SetActive(true);
        }
    }

    void OpenPopUp()
    {
        StopAllCoroutines();
        Time.timeScale = 0;
        _popUpController.OpenPopUp();
        StartCoroutine(RotateCircle());
        butReconnect.SetActive(true);

        if (GameObject.Find("Firebase") != null)
            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_InternetDisable();
    }

    void ClosePopUp()
    {
        Time.timeScale = 1;
        _popUpController.ClosedPopUp();
        //StartCoroutine(CheckConnect());
        StartCoroutine(checkInternetConnection());
    }

    IEnumerator RotateCircle()
    {
        yield return new WaitForSecondsRealtime(0);
        imgCircle.transform.Rotate(new Vector3(0, 0, rotateSpeed));

        StartCoroutine(RotateCircle());
    }

    public static ConnectionStatus CheckInternet()
    {
        // Проверить подключение к dns.msftncsi.com
        try
        {
            IPHostEntry entry = Dns.GetHostEntry("dns.msftncsi.com");
            if (entry.AddressList.Length == 0)
            {
                return ConnectionStatus.NotConnected;
            }
            else
            {
                if (!entry.AddressList[0].ToString().Equals("131.107.255.255"))
                {
                    return ConnectionStatus.LimitedAccess;
                }
            }
        }
        catch
        {
            return ConnectionStatus.NotConnected;
        }

        // Проверить загрузку документа ncsi.txt
        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://www.msftncsi.com/ncsi.txt");
        try
        {
            HttpWebResponse responce = (HttpWebResponse)request.GetResponse();

            if (responce.StatusCode != HttpStatusCode.OK)
            {
                return ConnectionStatus.LimitedAccess;
            }
            using (StreamReader sr = new StreamReader(responce.GetResponseStream()))
            {
                if (sr.ReadToEnd().Equals("Microsoft NCSI"))
                {
                    return ConnectionStatus.Connected;
                }
                else
                {
                    return ConnectionStatus.LimitedAccess;
                }
            }
        }
        catch
        {
            return ConnectionStatus.NotConnected;
        }
    }
}
