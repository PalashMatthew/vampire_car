using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControllerHub : MonoBehaviour
{
    public GameObject Message1;
    public GameObject Message2;
    public GameObject Message3;
    public GameObject Message4;
    public GameObject Message5;
    public GameObject Message5_1;
    public GameObject Message6;
    public GameObject Message7;
    public GameObject Message8;

    public GameObject popUpNewLevel;

    public void CheckTutorialPlay()
    {
        PlayerPrefs.SetString("tutorialLoc1Complite", "true");

        if (!PlayerPrefs.HasKey("tutorialHubStage"))
        {
            PlayerPrefs.SetInt("tutorialHubStage", 1);
        }

        if (PlayerPrefs.GetString("tutorialHubComplite") != "true")
        {
            if (PlayerPrefs.GetInt("tutorialHubStage") == 1)
            {
                PlayerPrefs.SetString("tutorialHubComplite", "false");
                popUpNewLevel.SetActive(false);
                Message1.GetComponent<PopUpController>().OpenPopUp();

                GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Tutorial("Hub_Stage_1");
            }

            if (PlayerPrefs.GetInt("tutorialHubStage") == 2)
            {
                StartMessage3();
            }

            if (PlayerPrefs.GetInt("tutorialHubStage") == 3)
            {
                StartMessage5_1();
            }
        }
    }

    public void StartMessage2()
    {
        Message1.GetComponent<PopUpController>().ClosedPopUp();
        Message2.GetComponent<PopUpController>().OpenPopUp();

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Tutorial("Hub_Stage_2");
    }

    public void OffMessage2()
    {
        Message2.GetComponent<PopUpController>().ClosedPopUp();
        PlayerPrefs.SetInt("tutorialHubStage", 2);
    }

    public void StartMessage3()
    {
        if (PlayerPrefs.GetString("tutorialHubComplite") != "true")
        {            
            Message3.GetComponent<PopUpController>().OpenPopUp();

            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Tutorial("Hub_Stage_3");
        }            
    }

    public void StartMessage4()
    {
        Message3.GetComponent<PopUpController>().ClosedPopUp();
        Message4.GetComponent<PopUpController>().OpenPopUp();

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Tutorial("Hub_Stage_4");
    }

    public void OffMessage4()
    {
        Message4.GetComponent<PopUpController>().ClosedPopUp();
        PlayerPrefs.SetInt("tutorialHubStage", 3);
    }

    public void StartMessage5()
    {
        if (PlayerPrefs.GetString("tutorialHubComplite") != "true")
        {
            Message5.GetComponent<PopUpController>().OpenPopUp();

            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Tutorial("Hub_Stage_5");
        }
    }

    public void StartMessage5_1()
    {
        if (PlayerPrefs.GetString("tutorialHubComplite") != "true")
        {
            Message5_1.GetComponent<PopUpController>().OpenPopUp();

            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Tutorial("Hub_Stage_5");
        }
    }

    public void StartMessage6()
    {
        Message5.GetComponent<PopUpController>().ClosedPopUp();
        Message5_1.GetComponent<PopUpController>().ClosedPopUp();
        Message6.GetComponent<PopUpController>().OpenPopUp();

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Tutorial("Hub_Stage_6");
    }

    public void StartMessage7()
    {
        Message6.GetComponent<PopUpController>().ClosedPopUp();
        Message7.GetComponent<PopUpController>().OpenPopUp();

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Tutorial("Hub_Stage_7");
    }

    public void StartMessage8()
    {
        PlayerPrefs.SetString("tutorialHubComplite", "true");
        Message7.GetComponent<PopUpController>().ClosedPopUp();
        Message8.GetComponent<PopUpController>().OpenPopUp();
        GameObject.Find("GameCloud").GetComponent<GameCloud>().SaveData();

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Tutorial("Hub_Stage_8");
    }

    public void EndTutorial()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventTutorialComplete);

        PlayerPrefs.SetString("tutorialHubComplite", "true");
        popUpNewLevel.SetActive(true);
        Message8.GetComponent<PopUpController>().ClosedPopUp();
        GameObject.Find("GameCloud").GetComponent<GameCloud>().SaveData();
    }
}
