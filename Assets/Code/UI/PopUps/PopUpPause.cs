using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpPause : MonoBehaviour
{
    public GameObject imgFade;
    public GameObject objPopUp;

    public float animSpeed;

    private PopUpController _popUpController;

    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
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
        _popUpController.ClosedPopUp();
    }

    public void ButSettings()
    {

    }

    public void ButExit()
    {
        GameplayController.isPause = false;
        Time.timeScale = 1;
        GameObject.Find("LoadingCanvas").GetComponent<ASyncLoader>().LoadLevel("Hub");
    }
}
