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

    public bool isSlotItem;

    public TMP_Text tName;
    public TMP_Text tRarity;
    public Image imgBackIcon;
    public Image imgIcon;
    public TMP_Text tDescription;

    public GameObject butSelect;
    public GameObject butUnselect;

    [Header("Item Info")]
    public Sprite sprRarity;
    public Sprite itemIcon;
    public string itemName;
    public string itemRarity;

    public Sprite sprCommonCard;
    public Sprite sprRareCard;
    public Sprite sprEpicCard;
    public Sprite sprLegendaryCard;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    void Initialize()
    {
        tName.text = itemName;

        switch (itemRarity)
        {
            case "common":
                imgBackIcon.sprite = sprCommonCard;
                tRarity.text = "<color=#808B96>Обычная</color>";
                break;

            case "rare":
                imgBackIcon.sprite = sprRareCard;
                tRarity.text = "<color=#3498DB>Редкая</color>";
                break;

            case "epic":
                imgBackIcon.sprite = sprEpicCard;
                tRarity.text = "<color=#CE33FF>Эпическая</color>";
                break;

            case "legendary":
                imgBackIcon.sprite = sprLegendaryCard;
                tRarity.text = "<color=yellow>Легендарная</color>";
                break;
        }

        imgIcon.sprite = itemIcon;

        if (isSlotItem)
        {
            butSelect.SetActive(false);
            butUnselect.SetActive(true);
        } 
        else
        {
            butSelect.SetActive(true);
            butUnselect.SetActive(false);
        }
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

        switch (garageController.activeItem.slotNum)
        {
            case 1:
                if (garageController.slot1Card != null)
                {
                    garageController.slot1Card.gameObject.SetActive(true);
                }

                garageController.slot1Card = garageController.activeItem;

                PlayerPrefs.SetInt("slot1_itemID", garageController.activeItem.itemID);
                PlayerPrefs.SetInt("slot1_itemNumInInventory", garageController.activeItem.itemNumInInventory);
                break;

            case 2:
                if (garageController.slot2Card != null)
                {
                    garageController.slot2Card.gameObject.SetActive(true);
                }

                garageController.slot2Card = garageController.activeItem;

                PlayerPrefs.SetInt("slot2_itemID", garageController.activeItem.itemID);
                PlayerPrefs.SetInt("slot2_itemNumInInventory", garageController.activeItem.itemNumInInventory);
                break;

            case 3:
                if (garageController.slot3Card != null)
                {
                    garageController.slot3Card.gameObject.SetActive(true);
                }

                garageController.slot3Card = garageController.activeItem;

                PlayerPrefs.SetInt("slot3_itemID", garageController.activeItem.itemID);
                PlayerPrefs.SetInt("slot3_itemNumInInventory", garageController.activeItem.itemNumInInventory);
                break;

            case 4:
                if (garageController.slot4Card != null)
                {
                    garageController.slot4Card.gameObject.SetActive(true);
                }

                garageController.slot4Card = garageController.activeItem;

                PlayerPrefs.SetInt("slot4_itemID", garageController.activeItem.itemID);
                PlayerPrefs.SetInt("slot4_itemNumInInventory", garageController.activeItem.itemNumInInventory);
                break;

            case 5:
                if (garageController.slot5Card != null)
                {
                    garageController.slot5Card.gameObject.SetActive(true);
                }

                garageController.slot5Card = garageController.activeItem;

                PlayerPrefs.SetInt("slot5_itemID", garageController.activeItem.itemID);
                PlayerPrefs.SetInt("slot5_itemNumInInventory", garageController.activeItem.itemNumInInventory);
                break;

            case 6:
                if (garageController.slot6Card != null)
                {
                    garageController.slot6Card.gameObject.SetActive(true);
                }

                garageController.slot6Card = garageController.activeItem;

                PlayerPrefs.SetInt("slot6_itemID", garageController.activeItem.itemID);
                PlayerPrefs.SetInt("slot6_itemNumInInventory", garageController.activeItem.itemNumInInventory);
                break;
        }

        

        garageController.activeItem.gameObject.SetActive(false);

        ButClosed();
    }

    public void ButUnselect()
    {
        garageController.ItemUnselect();
        ButClosed();
    }
}
