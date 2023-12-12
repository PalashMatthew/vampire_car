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
        }            
    }

    public void ButBuyAds()
    {
        //Запустить рекламу
        PlayerPrefs.SetInt("playerFuelCurrent", PlayerPrefs.GetInt("playerFuelCurrent") + 5);
    }

    public void ButOpen()
    {
        _popUpController.OpenPopUp();
    }

    public void ButClosed()
    {
        _popUpController.ClosedPopUp();
    }
}
