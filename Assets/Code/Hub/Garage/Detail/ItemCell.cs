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
                break;

            case "Engine":
                slotNum = 2;
                break;

            case "Brakes":
                slotNum = 3;
                break;

            case "FuelSystem":
                slotNum = 4;
                break;

            case "Suspension":
                slotNum = 5;
                break;

            case "Transmission":
                slotNum = 6;
                break;
        }

        switch (itemRarity)
        {
            case "common":
                imgCard.sprite = sprCommonCard;
                break;

            case "rare":
                imgCard.sprite = sprRareCard;
                break;

            case "epic":
                imgCard.sprite = sprEpicCard;
                break;

            case "legendary":
                imgCard.sprite = sprLegendaryCard;
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
