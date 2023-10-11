using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWin : MonoBehaviour
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
        GameplayController.isPause = true;
        Time.timeScale = 0;
        controller.OpenPopUp();
    }

    public void ButClosed()
    {
        GameplayController.isPause = false;
        Time.timeScale = 1;
        controller.ClosedPopUp();
    }

    public void ButRestart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
