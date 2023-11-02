using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddItemToInventory : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Dropdown itemType;
    public TMP_Dropdown itemRarity;

    public GameObject popUp;


    private void Start()
    {
        popUp.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            OpenPopUp();
        }
    }

    public void OpenPopUp()
    {
        popUp.SetActive(true);
    }

    public void ClosedPopUp()
    {
        popUp.SetActive(false);
    }

    public void AddItem()
    {
        string _itemType = "";
        string _itemRarity = "";

        switch (itemType.value)
        {
            case 0:
                _itemType = "Gun";
                break;

            case 1:
                _itemType = "Engine";
                break;

            case 2:
                _itemType = "Brakes";
                break;

            case 3:
                _itemType = "Transmission";
                break;

            case 4:
                _itemType = "Suspension";
                break;

            case 5:
                _itemType = "FuelSystem";
                break;
        }

        switch (itemRarity.value)
        {
            case 0:
                _itemRarity = "common";
                break;

            case 1:
                _itemRarity = "rare";
                break;

            case 2:
                _itemRarity = "epic";
                break;

            case 3:
                _itemRarity = "legendary";
                break;
        }

        PlayerPrefs.SetInt("itemCount" + _itemType,  PlayerPrefs.GetInt("itemCount" + _itemType) + 1);

        PlayerPrefs.SetInt("item" + _itemType + "ID" + (PlayerPrefs.GetInt("itemCount" + _itemType) - 1), Int32.Parse(inputField.text));

        PlayerPrefs.SetString("item" + _itemType + "Rarity" + (PlayerPrefs.GetInt("itemCount" + _itemType) - 1), _itemRarity);
    }
}
