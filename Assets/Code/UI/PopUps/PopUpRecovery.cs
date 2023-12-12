using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpRecovery : MonoBehaviour
{
    public int timerValue;
    public TMP_Text tTimer;

    private PopUpController _popUpController;

    public bool isRecovery;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();

        isRecovery = false;
    }

    public void ButOpen()
    {
        GameplayController.isPause = true;
        Time.timeScale = 0;
        _popUpController.OpenPopUp();

        timerValue = 5;

        tTimer.text = timerValue.ToString();

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_PlayerDie(GameObject.Find("GameplayController").GetComponent<WaveController>().currentWave -1, Application.loadedLevelName, (int)GameObject.Find("GameplayController").GetComponent<WaveController>().secondsPass, 1);

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        Sequence anim = DOTween.Sequence();
        anim.Append(tTimer.rectTransform.DOScale(1.2f, 0.2f).SetUpdate(true)).SetUpdate(true);
        anim.Append(tTimer.rectTransform.DOScale(1f, 0.2f).SetUpdate(true)).SetUpdate(true);

        yield return new WaitForSecondsRealtime(1f);

        timerValue--;

        tTimer.text = timerValue.ToString();

        if (timerValue > 0)
        {
            StartCoroutine(Timer());
        } 
        else
        {
            EndGame();          
        }        
    }

    public void EndGame()
    {
        StopAllCoroutines();

        GameplayController.isPause = false;
        Time.timeScale = 1;

        GameObject.Find("GameplayController").GetComponent<WaveController>().StopGame();

        GameObject.Find("PopUp Win").GetComponent<PopUpWin>().ButOpen();

        _popUpController.ClosedPopUp();
    }

    public void ButClosed()
    {
        GameplayController.isPause = false;
        Time.timeScale = 1;

        _popUpController.ClosedPopUp();
    }

    public void ButContinueAds()
    {
        StopAllCoroutines();

        isRecovery = true;

        GameObject.Find("Player").GetComponent<PlayerController>().isDead = false;
        GameObject.Find("Player").GetComponent<PlayerStats>().currentHp = GameObject.Find("Player").GetComponent<PlayerStats>().maxHp;

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Recovery(GameObject.Find("GameplayController").GetComponent<WaveController>().currentWave - 1, Application.loadedLevelName);

        ButClosed();
    }

    public void ButContinueHard()
    {
        if (PlayerPrefs.GetInt("playerHard") >= 30)
        {
            StopAllCoroutines();

            isRecovery = true;

            GameObject.Find("Player").GetComponent<PlayerController>().isDead = false;
            GameObject.Find("Player").GetComponent<PlayerStats>().currentHp = GameObject.Find("Player").GetComponent<PlayerStats>().maxHp;

            PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") - 30);

            ButClosed();
        }        
    }
}
