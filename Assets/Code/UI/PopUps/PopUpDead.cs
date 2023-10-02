using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpDead : MonoBehaviour
{
    public GameObject imgFade;
    public GameObject objPopUp;

    public float animSpeed;

    PopUpController controller;

    public GameObject tSkip;
    private float skipTimer = 2;
    private bool isSkipAccess;

    private void Start()
    {
        controller = new PopUpController();
        controller.imgFade = imgFade;
        controller.objPopUp = objPopUp;
        controller.animTime = animSpeed;
        tSkip.SetActive(false);
        isSkipAccess = false;

        controller.Initialize();
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
        controller.OpenPopUp();
    }

    public void ButClosed()
    {
        controller.ClosedPopUp();
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
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    public IEnumerator SkipTimer()
    {
        yield return new WaitForSeconds(skipTimer);
        tSkip.SetActive(true);
        isSkipAccess = true;
    }
}
