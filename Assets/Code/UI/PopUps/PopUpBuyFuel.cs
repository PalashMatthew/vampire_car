using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpBuyFuel : MonoBehaviour
{
    private PopUpController _popUpController;

    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    public void ButBuyHard()
    {
        if (PlayerPrefs.GetInt("playerHard") >= 60)
        {
            PlayerPrefs.SetInt("playerFuelCurrent", PlayerPrefs.GetInt("playerFuelCurrent") + 20);
            PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") - 60);

            GameObject.Find("GameCloud").GetComponent<GameCloud>().SaveData();

            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_BuyFuel("hard");
        }            
    }

    public void ButBuyAds()
    {
        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_BuyFuel("ads");

        GameObject.Find("AdsManager").GetComponent<AdsController>().ShowAds("fuel_5");
    }

    public void ButOpen()
    {
        _popUpController.OpenPopUp();

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_OpenFuelPopUp();
    }

    public void ButClosed()
    {
        _popUpController.ClosedPopUp();
    }
}
