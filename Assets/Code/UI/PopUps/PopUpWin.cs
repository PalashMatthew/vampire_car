using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWin : MonoBehaviour
{
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

    public void ButRestart()
    {
        GameplayController.isPause = false;
        Time.timeScale = 1;
        Application.LoadLevel(Application.loadedLevel);
    }
}
