using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OfflineTimeCheck : MonoBehaviour
{
    public static int totalSeconds;

    private void Awake()
    {
        CheckOffline();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
        }
        else
        {
            CheckOffline();
            Debug.Log("PauseFalse");
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
    }

    public void CheckOffline()
    {
        TimeSpan ts;

        if (PlayerPrefs.HasKey("LastSession"))
        {
            ts = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastSession"));

            totalSeconds = (int)ts.TotalSeconds;

            PlayerPrefs.SetString("OfflineTimeLast", ts.ToString());
            //Debug.Log(string.Format("¬ы отсутствовали - {0} дней, {1} часов, {2} минут, {3} секунд", ts.Days, ts.Hours, ts.Minutes, ts.Seconds));

            GameObject.Find("HubController").GetComponent<HubController>().FuelCheck();
        }            
    }
}
