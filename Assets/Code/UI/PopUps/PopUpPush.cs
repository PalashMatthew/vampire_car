using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class PopUpPush : MonoBehaviour
{
    private PopUpController _popUpController;

    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    public void ButOpen()
    {
        if (PlayerPrefs.GetInt("pushSendAccess") != 1 &&
            PlayerPrefs.GetInt("dontSendPush") != 1)
        {
            _popUpController.OpenPopUp();
            PlayerPrefs.SetInt("pushActivate", 0);
        }            
    }

    public void ButClosed()
    {
        _popUpController.ClosedPopUp();
    }

    public void ButYes()
    {
        PlayerPrefs.SetInt("pushSendAccess", 1);

        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }

        ButClosed();
    }

    public void ButNo()
    {
        PlayerPrefs.SetInt("dontSendPush", 1);
        ButClosed();
    }
}
