using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpDead : MonoBehaviour
{
    public GameObject imgFade;
    public GameObject objPopUp;

    public float animSpeed;

    PopUpController controller;

    private void Start()
    {
        controller = new PopUpController();
        controller.imgFade = imgFade;
        controller.objPopUp = objPopUp;
        controller.animTime = animSpeed;

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
        
    }
}
