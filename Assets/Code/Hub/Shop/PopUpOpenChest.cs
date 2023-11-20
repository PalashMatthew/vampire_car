using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpOpenChest : MonoBehaviour
{
    PopUpController _popUpController;

    public string rarity;
    public DetailCard card;

    public TMP_Text tName;
    public TMP_Text tRarity;

    public Image imgIcon;

    public Image imgCard;
    public Sprite sprCardCommon;
    public Sprite sprCardRare;
    public Sprite sprCardEpic;
    public Sprite sprCardLegendary;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    public void Open()
    {
        _popUpController.OpenPopUp();

        switch (rarity)
        {
            case "common":
                imgCard.sprite = sprCardCommon;

                tRarity.text = "<color=#808B96>�������</color>";
                break;

            case "rare":
                imgCard.sprite = sprCardRare;

                tRarity.text = "<color=#3498DB>������</color>";
                break;

            case "epic":
                imgCard.sprite = sprCardEpic;

                tRarity.text = "<color=#CE33FF>���������</color>";
                break;

            case "legendary":
                imgCard.sprite = sprCardLegendary;

                tRarity.text = "<color=yellow>�����������</color>";
                break;
        }

        tName.text = "<rainb>" + card.itemName;

        imgIcon.sprite = card.sprItem;

        #region AddNewItem
        string _itemType;

        _itemType = card.itemType.ToString();

        PlayerPrefs.SetInt("itemCount" + _itemType, PlayerPrefs.GetInt("itemCount" + _itemType) + 1);

        int _cellCount = PlayerPrefs.GetInt("itemCount" + _itemType) - 1;

        if (rarity == "common")
        {
            float _value1 = card.baseItemCharactersCommon1Value;
            float _value2 = card.baseItemCharactersCommon2Value;

            PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterCommon1Value" + _cellCount, _value1);
            PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterCommon2Value" + _cellCount, _value2);
        }

        if (rarity == "rare")
        {
            float _value1 = card.baseItemCharactersRare1Value;
            float _value2 = card.baseItemCharactersRare2Value;

            PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterRare1Value" + _cellCount, _value1);
            PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterRare2Value" + _cellCount, _value2);
        }

        if (rarity == "epic")
        {
            float _value1 = card.baseItemCharactersEpic1Value;
            float _value2 = card.baseItemCharactersEpic2Value;

            PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterEpic1Value" + _cellCount, _value1);
            PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterEpic2Value" + _cellCount, _value2);
        }

        if (rarity == "legendary")
        {
            float _value1 = card.baseItemCharactersLegendary1Value;
            float _value2 = card.baseItemCharactersLegendary2Value;

            PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterLegendary1Value" + _cellCount, _value1);
            PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterLegendary2Value" + _cellCount, _value2);
        }

        PlayerPrefs.SetInt("item" + _itemType + "ID" + _cellCount, card.itemID);
        PlayerPrefs.SetInt("item" + _itemType + "Level" + _cellCount, 1);
        PlayerPrefs.SetString("item" + _itemType + "Rarity" + _cellCount, rarity);
        PlayerPrefs.SetString("item" + _itemType + "Type" + _cellCount, card.itemType.ToString());
        #endregion
    }

    public void ButContinue()
    {
        _popUpController.ClosedPopUp();
    }
}
