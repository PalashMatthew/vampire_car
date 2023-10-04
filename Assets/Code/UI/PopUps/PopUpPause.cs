using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpPause : MonoBehaviour
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

    public void ButOpen()
    {
        Time.timeScale = 0;
        controller.OpenPopUp();
    }

    public void ButClosed()
    {
        Time.timeScale = 1;
        controller.ClosedPopUp();
    }

    public void ButSettings()
    {

    }

    public void ButExit()
    {

    }
}
