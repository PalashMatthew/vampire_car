using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCell : MonoBehaviour
{
    public int itemID;
    public int slotNum;
    public string itemType;
    public string itemName;

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


    private void Start()
    {
        Initialize();
    }

    void Initialize()
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

        string itemSpriteName = "item_" + itemID;
        sprIcon = Resources.Load<Sprite>("ItemSprite/" + itemSpriteName);

        imgIcon.sprite = sprIcon;

        //tLevel.text = "Lv. " + 
    }

    public void ButOpen()
    {
        GameObject.Find("Garage").GetComponent<GarageController>().activeItem = gameObject.GetComponent<ItemCell>();
        GameObject.Find("PopUp Detail Upgrade").GetComponent<PopUpDetail>().ButOpen();
    }
}
