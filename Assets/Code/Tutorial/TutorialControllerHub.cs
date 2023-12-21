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
    public GameObject Message6;
    public GameObject Message7;
    public GameObject Message8;

    public GameObject popUpNewLevel;

    private void Start()
    {
        if (PlayerPrefs.GetString("tutorialHubComplite") != "true")
        {
            PlayerPrefs.SetString("tutorialHubComplite", "false");
            popUpNewLevel.SetActive(false);
            Message1.GetComponent<PopUpController>().OpenPopUp();
        }
    }

    public void StartMessage2()
    {
        Message1.GetComponent<PopUpController>().ClosedPopUp();
        Message2.GetComponent<PopUpController>().OpenPopUp();
    }

    public void OffMessage2()
    {
        Message2.GetComponent<PopUpController>().ClosedPopUp();
    }

    public void StartMessage3()
    {
        if (PlayerPrefs.GetString("tutorialHubComplite") != "true")
        {            
            Message3.GetComponent<PopUpController>().OpenPopUp();
        }            
    }

    public void StartMessage4()
    {
        Message3.GetComponent<PopUpController>().ClosedPopUp();
        Message4.GetComponent<PopUpController>().OpenPopUp();
    }

    public void OffMessage4()
    {
        Message4.GetComponent<PopUpController>().ClosedPopUp();
    }

    public void StartMessage5()
    {
        if (PlayerPrefs.GetString("tutorialHubComplite") != "true")
            Message5.GetComponent<PopUpController>().OpenPopUp();
    }

    public void StartMessage6()
    {
        Message5.GetComponent<PopUpController>().ClosedPopUp();
        Message6.GetComponent<PopUpController>().OpenPopUp();
    }

    public void StartMessage7()
    {
        Message6.GetComponent<PopUpController>().ClosedPopUp();
        Message7.GetComponent<PopUpController>().OpenPopUp();
    }

    public void StartMessage8()
    {
        Message7.GetComponent<PopUpController>().ClosedPopUp();
        Message8.GetComponent<PopUpController>().OpenPopUp();
    }

    public void EndTutorial()
    {
        PlayerPrefs.SetString("tutorialHubComplite", "true");
        popUpNewLevel.SetActive(true);
        Message8.GetComponent<PopUpController>().ClosedPopUp();
    }
}
