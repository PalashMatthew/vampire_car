using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ItemCell;

public class PopUpDetail : MonoBehaviour
{
    PopUpController _popUpController;
    public GarageController garageController;

    public TMP_Text tName;
    public TMP_Text tRarity;
    public Image imgBackIcon;
    public Image imgIcon;
    public TMP_Text tDescription;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    void Initialize()
    {
        tName.text = garageController.activeItem.itemName;

        switch (garageController.activeItem.itemRarity)
        {
            case "common":
                imgBackIcon.sprite = garageController.activeItem.sprCommonCard;
                tRarity.text = "<color=#808B96>Обычная</color>";
                break;

            case "rare":
                imgBackIcon.sprite = garageController.activeItem.sprRareCard;
                tRarity.text = "<color=#3498DB>Редкая</color>";
                break;

            case "epic":
                imgBackIcon.sprite = garageController.activeItem.sprEpicCard;
                tRarity.text = "<color=#CE33FF>Эпическая</color>";
                break;

            case "legendary":
                imgBackIcon.sprite = garageController.activeItem.sprLegendaryCard;
                tRarity.text = "<color=yellow>Легендарная</color>";
                break;
        }

        imgIcon.sprite = garageController.activeItem.sprIcon;
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

    public void ButSelect()
    {
        garageController.ItemSelect();
        ButClosed();
    }
}
