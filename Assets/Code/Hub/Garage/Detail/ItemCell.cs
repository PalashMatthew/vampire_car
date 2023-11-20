using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCell : MonoBehaviour
{
    public GarageController garageController;
    PopUpDetail _popUpDetail;
    PopUpMerge _popUpMerge;
    PopUpRepair _popUpRepair;

    public DetailCard itemObj;    

    public enum CellType
    {
        Inventory,
        Merge,
        Repair
    }
    public CellType cellType;

    public int itemID;
    public int slotNum;
    public string itemType;
    public string itemName;
    public int itemNumInInventory;

    [Header("Icon")]
    public Image imgIcon;
    public Sprite sprIcon;
    public Image imgIconTypeBack;
    public Image imgIconType;
    public Sprite sprIconGun;
    public Sprite sprIconEngine;
    public Sprite sprIconBrakes;
    public Sprite sprIconFuelSystem;
    public Sprite sprIconSuspension;
    public Sprite sprIconTransmission;

    [Header("Image Card")]
    public Image imgCard;
    public Sprite sprCommonCard;
    public Sprite sprRareCard;
    public Sprite sprEpicCard;
    public Sprite sprLegendaryCard;

    [Header("Merge")]
    public GameObject mergeSelect;
    public GameObject mergeDeactivate;
    public bool isMergeBlock;

    [Header("Level")]
    public int currentLevel;
    public TMP_Text tLevel;

    public string itemRarity;


    private void Awake()
    {
        _popUpDetail = GameObject.Find("PopUp Detail Upgrade").GetComponent<PopUpDetail>();

        _popUpMerge = GameObject.Find("PopUp Merge").GetComponent<PopUpMerge>();
        _popUpRepair = GameObject.Find("PopUp Merge").GetComponent<PopUpRepair>();
    }

    public void Initialize()
    {
        itemType = itemObj.itemType.ToString();

        switch (itemType)
        {
            case "Gun":
                slotNum = 1;
                imgIconType.sprite = sprIconGun;
                break;

            case "Engine":
                slotNum = 2;
                imgIconType.sprite = sprIconEngine;
                break;

            case "Brakes":
                slotNum = 3;
                imgIconType.sprite = sprIconBrakes;
                break;

            case "FuelSystem":
                slotNum = 4;
                imgIconType.sprite = sprIconFuelSystem;
                break;

            case "Suspension":
                slotNum = 5;
                imgIconType.sprite = sprIconSuspension;
                break;

            case "Transmission":
                slotNum = 6;
                imgIconType.sprite = sprIconTransmission;
                break;
        }

        switch (itemRarity)
        {
            case "common":
                imgCard.sprite = sprCommonCard;
                imgIconTypeBack.color = new Color(0.509804f, 0.509804f, 0.509804f, 1);
                break;

            case "rare":
                imgCard.sprite = sprRareCard;
                imgIconTypeBack.color = new Color(0f, 0.5764706f, 0.7686275f, 1);
                break;

            case "epic":
                imgCard.sprite = sprEpicCard;
                imgIconTypeBack.color = new Color(0.654902f, 0f, 0.9137256f, 1);
                break;

            case "legendary":
                imgCard.sprite = sprLegendaryCard;
                imgIconTypeBack.color = new Color(0.7215686f, 0.6313726f, 0f, 1);
                break;
        }

        //string itemSpriteName = "item_" + itemID;
        //sprIcon = Resources.Load<Sprite>("ItemSprite/" + itemSpriteName);

        imgIcon.sprite = itemObj.sprItem;
        sprIcon = itemObj.sprItem;

        itemName = itemObj.itemName;

        //tLevel.text = "Lv. " + 
    }

    private void Update()
    {
        tLevel.text = "Lv. " + currentLevel;
    }

    public void ButOpen()
    {
        if (cellType == CellType.Inventory)
        {
            GameObject.Find("Garage").GetComponent<GarageController>().activeItem = gameObject.GetComponent<ItemCell>();

            _popUpDetail.sprRarity = imgCard.sprite;
            _popUpDetail.itemIcon = sprIcon;
            _popUpDetail.itemName = itemName;
            _popUpDetail.itemRarity = itemRarity;
            _popUpDetail.isSlotItem = false;

            _popUpDetail.ButOpen();
        }

        if (cellType == CellType.Repair)
        {
            _popUpRepair.ButChooseItem(gameObject.GetComponent<ItemCell>());
        }

        if (cellType == CellType.Merge && !isMergeBlock)
        {
            _popUpMerge.ButChooseItem(gameObject.GetComponent<ItemCell>());
        }
    }

    public void MergeSelect()
    {
        mergeSelect.SetActive(true);
        isMergeBlock = true;
    }

    public void MergeDeactivate()
    {
        mergeDeactivate.SetActive(true);
        isMergeBlock = true;
    }

    public void MergeDefault()
    {
        mergeSelect.SetActive(false);
        mergeDeactivate.SetActive(false);
        isMergeBlock = false;
    }
}
