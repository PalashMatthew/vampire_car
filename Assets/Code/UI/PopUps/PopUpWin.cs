using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpWin : MonoBehaviour
{
    private PopUpController _popUpController;

    public GameObject objNewRecord;
    public TMP_Text tWave;
    public TMP_Text tLeader;
    public TMP_Text tAdsReward;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ButOpen();
        }
    }

    public void ButOpen()
    {
        GameplayController.isPause = true;
        Time.timeScale = 0;
        _popUpController.OpenPopUp();
    }

    public void ButClosed()
    {
        GameplayController.isPause = false;
        Time.timeScale = 1;
        GameObject.Find("LoadingCanvas").GetComponent<ASyncLoader>().LoadLevel("Hub");
        _popUpController.ClosedPopUp();        
    }

    public void ButAds()
    {
        //Удваиваем монеты
    }
}
