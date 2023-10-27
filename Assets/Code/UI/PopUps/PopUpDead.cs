using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpDead : MonoBehaviour
{
    private PopUpController _popUpController;

    public GameObject tSkip;
    private float skipTimer = 2;
    private bool isSkipAccess;

    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();

        tSkip.SetActive(false);
        isSkipAccess = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ButOpen();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ButClosed();
        }
    }

    public void ButOpen()
    {
        GameplayController.isPause = true;
        Time.timeScale = 0;
        _popUpController.OpenPopUp();
        StartCoroutine(SkipTimer());
    }

    public void ButClosed()
    {
        GameplayController.isPause = false;
        Time.timeScale = 1;
        _popUpController.ClosedPopUp();
    }

    public void ButContinueHard()
    {
        
    }

    public void ButContinueAds()
    {
        
    }

    public void ButSkip()
    {
        if (isSkipAccess)
        {
            GameplayController.isPause = false;
            Time.timeScale = 1;
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    public IEnumerator SkipTimer()
    {
        yield return new WaitForSecondsRealtime(skipTimer);
        tSkip.SetActive(true);
        tSkip.transform.localScale = Vector3.zero;
        tSkip.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack).SetUpdate(true);
        isSkipAccess = true;
    }
}
