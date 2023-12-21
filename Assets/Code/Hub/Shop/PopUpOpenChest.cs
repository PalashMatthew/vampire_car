using AssetKits.ParticleImage;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PopUpOpenChest : MonoBehaviour
{
    PopUpController _popUpController;
    public GameObject hubController;

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

    public GameObject chest1Obj, chest2obj;

    public int chestType;

    public GameObject butOk;

    public ParticleImage vfxConfetti;
    public GameObject vfxCircles;

    public GameObject shopCanvas;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    public void Open()
    {
        StartCoroutine(OffShopCanvas());
        StartCoroutine(Animation());

        _popUpController.OpenPopUp();

        switch (rarity)
        {
            case "common":
                imgCard.sprite = sprCardCommon;

                tRarity.text = "<color=#808B96>Обычная</color>";
                break;

            case "rare":
                imgCard.sprite = sprCardRare;

                tRarity.text = "<color=#3498DB>Редкая</color>";
                break;

            case "epic":
                imgCard.sprite = sprCardEpic;

                tRarity.text = "<color=#CE33FF>Эпическая</color>";
                break;

            case "legendary":
                imgCard.sprite = sprCardLegendary;

                tRarity.text = "<color=yellow>Легендарная</color>";
                break;
        }

        tName.text = "<wave>" + card.itemName;

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

    IEnumerator OffShopCanvas()
    {
        yield return new WaitForSeconds(0.3f);
        shopCanvas.SetActive(false);
    }

    public void ButContinue()
    {
        shopCanvas.SetActive(true);
        hubController.GetComponent<ShopController>().Initialize();

        _popUpController.ClosedPopUp();
    }

    IEnumerator Animation()
    {
        if (chestType == 1)
        {
            chest1Obj.SetActive(true);
            chest2obj.SetActive(false);
        }

        if (chestType == 2)
        {
            chest1Obj.SetActive(false);
            chest2obj.SetActive(true);
        }

        vfxCircles.transform.DOScale(0, 0);
        tName.transform.DOScale(0, 0);
        tRarity.transform.DOScale(0, 0);
        imgCard.transform.DOScale(0, 0);
        butOk.transform.DOScale(0, 0);

        vfxCircles.gameObject.SetActive(false);
        tName.gameObject.SetActive(false);
        tRarity.gameObject.SetActive(false);
        imgCard.gameObject.SetActive(false);
        butOk.SetActive(false);

        yield return new WaitForSeconds(1.9f);

        vfxConfetti.Play();

        vfxCircles.gameObject.SetActive(true);
        tName.gameObject.SetActive(true);
        tRarity.gameObject.SetActive(true);
        imgCard.gameObject.SetActive(true);

        vfxCircles.transform.DOScale(1, 0.3f);
        tName.transform.DOScale(1, 0.3f);
        tRarity.transform.DOScale(1, 0.3f);
        imgCard.transform.DOScale(1, 0.3f);

        imgCard.GetComponent<RectTransform>().DOAnchorPosY(-1256, 0);
        imgCard.GetComponent<RectTransform>().DOAnchorPosY(-670, 0.3f);

        yield return new WaitForSeconds(2f);

        butOk.gameObject.SetActive(true);

        butOk.transform.DOScale(1, 0.3f);
    }
}
