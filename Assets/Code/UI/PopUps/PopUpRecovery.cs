using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpRecovery : MonoBehaviour
{
    public int timerValue;
    public TMP_Text tTimer;

    public GameObject imgFade;
    public GameObject objPopUp;
    private PopUpController _popUpController;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ButOpen();
        }
    }

    public void ButOpen()
    {
        GameplayController.isPause = true;
        Time.timeScale = 0;
        _popUpController.OpenPopUp();

        timerValue = 5;

        tTimer.text = timerValue.ToString();

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(1f);

        timerValue--;

        tTimer.text = timerValue.ToString();

        if (timerValue > 0)
        {
            StartCoroutine(Timer());
        } 
        else
        {
            ButClosed();
        }        
    }

    public void ButClosed()
    {
        GameplayController.isPause = false;
        Time.timeScale = 1;
        _popUpController.ClosedPopUp();
    }

    public void ButContinueAds()
    {

    }

    public void ButContinueHard()
    {

    }
}
