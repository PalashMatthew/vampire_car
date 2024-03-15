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

            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_PopUpPush("Open", GameObject.Find("HubController").GetComponent<ChooseLocationController>().currentLocNum);
        }            
    }

    public void ButClosed()
    {
        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_PopUpPush("Closed", GameObject.Find("HubController").GetComponent<ChooseLocationController>().currentLocNum);

        _popUpController.ClosedPopUp();
    }

    public void ButYes()
    {
        PlayerPrefs.SetInt("pushSendAccess", 1);

        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_PopUpPush("Access_True", GameObject.Find("HubController").GetComponent<ChooseLocationController>().currentLocNum);

        ButClosed();
    }

    public void ButNo()
    {
        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_PopUpPush("Access_False", GameObject.Find("HubController").GetComponent<ChooseLocationController>().currentLocNum);

        PlayerPrefs.SetInt("dontSendPush", 1);
        ButClosed();
    }
}
