using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpContinue : MonoBehaviour
{
    private PopUpController _popUpController;

    public ASyncLoader loader;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();

        if (PlayerPrefs.GetInt("locationContinue") == 1)
        {
            if (PlayerPrefs.GetString("tutorialComplite") == "true" &&
                PlayerPrefs.GetString("tutorialHubComplite") == "true" &&
                PlayerPrefs.GetString("tutorialLoc1Complite") == "true" &&
                PlayerPrefs.GetString("tutorialCards") == "true")
            {
                ButOpen();
            }
            else
            {
                PlayerPrefs.SetInt("locationContinue", 0);
                PlayerPrefs.SetInt("saveTryPassiveCount", 0);
            }            
        }
    }

    public void ButOpen()
    {
        _popUpController.OpenPopUp();

        if (GameObject.Find("Firebase") != null)
            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_PopUpGameContinueOpen();
    }

    public void ButContinue()
    {
        if (PlayerPrefs.GetString("locationNameSave") != "")
        {
            if (GameObject.Find("Firebase") != null)
                GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_PopUpGameContinueStatus("Yes");

            loader.LoadLevel(PlayerPrefs.GetString("locationNameSave"));
        }
    }

    public void ButClosed()
    {
        if (GameObject.Find("Firebase") != null)
            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_PopUpGameContinueStatus("Not");

        PlayerPrefs.SetInt("locationContinue", 0);
        PlayerPrefs.SetInt("saveTryPassiveCount", 0);
        _popUpController.ClosedPopUp();
    }
}
