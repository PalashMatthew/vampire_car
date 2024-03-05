using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpStarterPack : MonoBehaviour
{
    private PopUpController _popUpController;

    public GameObject outline1, outline2, outline3;

    public Sprite sprGun1, sprGun2, sprGun3;
    public Image imgGun;

    int activeGun;

    public TMP_Text tNewPrice, tOldPrice;

    public GameObject butStarterPackObj;

    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();

        if (!PlayerPrefs.HasKey("starterPackShow"))
        {
            PlayerPrefs.SetInt("starterPackShow", 1);
        }

        ShowStarterPack();
    }

    public void ShowStarterPack()
    {
        if (PlayerPrefs.GetInt("starterPackShow") == 1 && PlayerPrefs.GetInt("firstShowStarterPack") == 1
            && PlayerPrefs.GetInt("starterPackPurchased") == 0 && PlayerPrefs.GetString("tutorialHubComplite") == "true")
        {            
            ButOpen();
        }

        PlayerPrefs.SetInt("firstShowStarterPack", 0);
    }

    public void ButOpen()
    {
        _popUpController.OpenPopUp();

        Initialize();
    }

    public void ButClosed()
    {
        _popUpController.ClosedPopUp();
    }

    void Initialize()
    {
        ButChangeGun(2);

        string newPrice = GameObject.Find("HubController").GetComponent<ShopController>().prices[4];
        string oldPrice = GameObject.Find("HubController").GetComponent<ShopController>().prices[5];

        tNewPrice.text = newPrice;
        tOldPrice.text = oldPrice;

        CheckTime();
    }

    public void ButChangeGun(int _id)
    {
        switch (_id)
        {
            case 1:
                outline1.SetActive(true);
                outline2.SetActive(false);
                outline3.SetActive(false);

                imgGun.sprite = sprGun1;

                activeGun = 1;
                break;

            case 2:
                outline1.SetActive(false);
                outline2.SetActive(true);
                outline3.SetActive(false);

                imgGun.sprite = sprGun2;

                activeGun = 2;
                break;

            case 3:
                outline1.SetActive(false);
                outline2.SetActive(false);
                outline3.SetActive(true);

                imgGun.sprite = sprGun3;

                activeGun = 3;
                break;
        }
    }

    public void ButBuy()
    {
        GameObject.Find("HubController").GetComponent<ShopController>().ButBuyStarterPack();
    }

    public void CallBackPurchased()
    {
        PlayerPrefs.SetInt("starterPackPurchased", 1);

        switch (activeGun)
        {
            case 1:
                AddItem(1002);
                break;

            case 2:
                AddItem(1004);
                break;

            case 3:
                AddItem(1009);
                break;
        }

        PlayerPrefs.SetInt("playerMoney", PlayerPrefs.GetInt("playerMoney") + 25000);
        PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + 250);
        PlayerPrefs.SetInt("playerTitan", PlayerPrefs.GetInt("playerTitan") + 25);
        PlayerPrefs.SetInt("drawingGunCount", PlayerPrefs.GetInt("drawingGunCount") + 25);

        butStarterPackObj.SetActive(false);
        ButClosed();
    }

    public void AddItem(int _id)
    {
        string _itemType = "Gun";
        string _itemRarity = "epic";
        
        PlayerPrefs.SetInt("itemCount" + _itemType, PlayerPrefs.GetInt("itemCount" + _itemType) + 1);

        PlayerPrefs.SetInt("item" + _itemType + "ID" + (PlayerPrefs.GetInt("itemCount" + _itemType) - 1), _id);

        PlayerPrefs.SetString("item" + _itemType + "Rarity" + (PlayerPrefs.GetInt("itemCount" + _itemType) - 1), _itemRarity);
    }

    void CheckTime()
    {
        if (PlayerPrefs.HasKey("OfflineTimeLast"))
        {
            int seconds = OfflineTimeCheck.totalSeconds;
            Debug.Log("STARTER PACK SECONDS");

            PlayerPrefs.SetInt("StarterPackSaveTime", PlayerPrefs.GetInt("StarterPackSaveTime") + seconds);

            if (PlayerPrefs.GetInt("AdsChestTimerSaveTime") > 172800)
            {
                PlayerPrefs.SetInt("starterPackShow", 0);
            }
        }
    }
}
