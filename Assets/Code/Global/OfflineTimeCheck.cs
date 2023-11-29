using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OfflineTimeCheck : MonoBehaviour
{
    private void Awake()
    {
        CheckOffline();
    }

    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
    }

    public void CheckOffline()
    {
        TimeSpan ts;

        if (PlayerPrefs.HasKey("LastSession"))
        {
            ts = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastSession"));

            PlayerPrefs.SetString("OfflineTimeLast", ts.ToString());
            //Debug.Log(string.Format("�� ������������� - {0} ����, {1} �����, {2} �����, {3} ������", ts.Days, ts.Hours, ts.Minutes, ts.Seconds));
        }            

        PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
    }
}
