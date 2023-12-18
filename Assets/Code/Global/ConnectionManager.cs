using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;

public class ConnectionManager : MonoBehaviour
{
    private PopUpController _popUpController;
    public GameObject imgCircle;
    public float rotateSpeed;

    public GameObject headObj;


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

        StartCoroutine(CheckConnect());
    }

    IEnumerator CheckConnect()
    {
        yield return new WaitForSecondsRealtime(1);

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

        yield return new WaitForSecondsRealtime(4);

        StartCoroutine(CheckConnect());
    }

    public void ButReconnect()
    {
        if (CheckInternet() == ConnectionStatus.Connected)
        {
            PlayerPrefs.SetInt("internet_access", 1);
            StopAllCoroutines();
            ClosePopUp();
        }
    }

    void OpenPopUp()
    {
        StopAllCoroutines();
        Time.timeScale = 0;
        _popUpController.OpenPopUp();
        StartCoroutine(RotateCircle());
    }

    void ClosePopUp()
    {
        Time.timeScale = 1;
        _popUpController.ClosedPopUp();
        StartCoroutine(CheckConnect());
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
