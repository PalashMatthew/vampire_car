using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCell : MonoBehaviour
{
    public DetailCard itemObj;

    public GarageController garageController;
    PopUpDetail _popUpDetail;

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

    [Header("Level")]
    public int currentLevel;
    public TMP_Text tLevel;

    public string itemRarity;


    private void Awake()
    {
        _popUpDetail = GameObject.Find("PopUp Detail Upgrade").GetComponent<PopUpDetail>();
        //Initialize();
    }

    public void Initialize()
    {
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

        //tLevel.text = "Lv. " + 
    }

    public void ButOpen()
    {
        GameObject.Find("Garage").GetComponent<GarageController>().activeItem = gameObject.GetComponent<ItemCell>();

        _popUpDetail.sprRarity = imgCard.sprite;
        _popUpDetail.itemIcon = sprIcon;
        _popUpDetail.itemName = itemName;
        _popUpDetail.itemRarity = itemRarity;
        _popUpDetail.isSlotItem = false;

        _popUpDetail.ButOpen();
    }
}
