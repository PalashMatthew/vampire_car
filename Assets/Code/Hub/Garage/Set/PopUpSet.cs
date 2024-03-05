using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class PopUpSet : MonoBehaviour
{
    PopUpController _popUpController;

    public List<SetCard> setCards;
    private SetCard setCard;
    private SetCard setCardActive;

    public string setID;

    private int setDetailCount;

    [Header("UI")]
    public TMP_Text tName;
    public TMP_Text tSetDetailCount;
    public TMP_Text tDescription1;
    public TMP_Text tDescription2;
    public TMP_Text tDescription3;
    public Image imgBonusPanel1;
    public Image imgBonusPanel2;
    public Image imgBonusPanel3;
    public Sprite sprBonusPanelInactive;
    public Sprite sprBonusPanelActive;

    [Header("Cell")]
    public Image imgCell1;
    public Image imgCell2;
    public Image imgCell3;
    public Image imgCell4;
    public Image imgCell5;
    public Image imgIconCell1;
    public Image imgIconCell2;
    public Image imgIconCell3;
    public Image imgIconCell4;
    public Image imgIconCell5;

    public Image imgGarageCell1;
    public Image imgGarageCell2;
    public Image imgGarageCell3;
    public Image imgGarageCell4;
    public Image imgGarageCell5;
    public Sprite sprCellCommon;

    [Header("Detail")]
    public List<DetailCard> engineCards;
    public List<DetailCard> brakesCards;
    public List<DetailCard> fuelSystemCards;
    public List<DetailCard> suspensionCards;
    public List<DetailCard> transmissionCards;

    private Color colorActive;
    private Color colorInactive;

    public GarageController garageController;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    void Initialize()
    {
        colorActive = new Color(1, 1, 1, 1);
        colorInactive = new Color(0.5f, 0.5f, 0.5f, 1);

        setDetailCount = 0;

        tName.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_set" + setID + "Name");


        foreach (SetCard _card in setCards)
        {
            if (_card.setID == setID)
            {
                setCard = _card;
            }
        }

        #region Выставляем иконки
        int engineID = 0;
        int brakesID = 0;
        int fuelSystemID = 0;
        int suspensionID = 0;
        int transmissionID = 0;

        switch (setID)
        {
            case "s01":
                engineID = 2001;
                brakesID = 3001;
                fuelSystemID = 4001;
                suspensionID = 5001;
                transmissionID = 6001;
                break;
            case "s02":
                engineID = 2002;
                brakesID = 3002;
                fuelSystemID = 4002;
                suspensionID = 5002;
                transmissionID = 6002;
                break;
            case "s03":
                engineID = 2003;
                brakesID = 3003;
                fuelSystemID = 4003;
                suspensionID = 5003;
                transmissionID = 6003;
                break;
            case "s04":
                engineID = 2004;
                brakesID = 3004;
                fuelSystemID = 4004;
                suspensionID = 5004;
                transmissionID = 6004;
                break;
            case "s05":
                engineID = 2005;
                brakesID = 3005;
                fuelSystemID = 4005;
                suspensionID = 5005;
                transmissionID = 6005;
                break;
            case "s06":
                engineID = 2006;
                brakesID = 3006;
                fuelSystemID = 4006;
                suspensionID = 5006;
                transmissionID = 6006;
                break;
            case "s07":
                engineID = 2007;
                brakesID = 3007;
                fuelSystemID = 4007;
                suspensionID = 5007;
                transmissionID = 6007;
                break;
            case "s08":
                engineID = 2008;
                brakesID = 3008;
                fuelSystemID = 4008;
                suspensionID = 5008;
                transmissionID = 6008;
                break;
            case "s09":
                engineID = 2009;
                brakesID = 3009;
                fuelSystemID = 4009;
                suspensionID = 5009;
                transmissionID = 6009;
                break;
            case "s10":
                engineID = 2010;
                brakesID = 3010;
                fuelSystemID = 4010;
                suspensionID = 5010;
                transmissionID = 6010;
                break;
        }

        foreach (DetailCard _card in engineCards)
        {
            if (_card.itemID == engineID)
            {
                imgIconCell1.sprite = _card.sprItem;
            }
        }

        foreach (DetailCard _card in brakesCards)
        {
            if (_card.itemID == brakesID)
            {
                imgIconCell2.sprite = _card.sprItem;
            }
        }

        foreach (DetailCard _card in fuelSystemCards)
        {
            if (_card.itemID == fuelSystemID)
            {
                imgIconCell3.sprite = _card.sprItem;
            }
        }

        foreach (DetailCard _card in suspensionCards)
        {
            if (_card.itemID == suspensionID)
            {
                imgIconCell4.sprite = _card.sprItem;
            }
        }

        foreach (DetailCard _card in transmissionCards)
        {
            if (_card.itemID == transmissionID)
            {
                imgIconCell5.sprite = _card.sprItem;
            }
        }
        #endregion

        #region Настраиваем отображение
        if (PlayerPrefs.GetInt("slot2_itemID") == engineID)
        {
            imgCell1.color = colorActive;
            imgIconCell1.color = colorActive;
            setDetailCount++;

            imgCell1.sprite = imgGarageCell1.sprite;
        } 
        else
        {
            imgCell1.color = colorInactive;
            imgIconCell1.color = colorInactive;

            imgCell1.sprite = sprCellCommon;
        }

        if (PlayerPrefs.GetInt("slot3_itemID") == brakesID)
        {
            imgCell2.color = colorActive;
            imgIconCell2.color = colorActive;
            setDetailCount++;

            imgCell2.sprite = imgGarageCell2.sprite;
        }
        else
        {
            imgCell2.color = colorInactive;
            imgIconCell2.color = colorInactive;

            imgCell2.sprite = sprCellCommon;
        }

        if (PlayerPrefs.GetInt("slot4_itemID") == fuelSystemID)
        {
            imgCell3.color = colorActive;
            imgIconCell3.color = colorActive;
            setDetailCount++;

            imgCell3.sprite = imgGarageCell3.sprite;
        }
        else
        {
            imgCell3.color = colorInactive;
            imgIconCell3.color = colorInactive;

            imgCell3.sprite = sprCellCommon;
        }

        if (PlayerPrefs.GetInt("slot5_itemID") == suspensionID)
        {
            imgCell4.color = colorActive;
            imgIconCell4.color = colorActive;
            setDetailCount++;

            imgCell4.sprite = imgGarageCell4.sprite;
        }
        else
        {
            imgCell4.color = colorInactive;
            imgIconCell4.color = colorInactive;

            imgCell4.sprite = sprCellCommon;
        }

        if (PlayerPrefs.GetInt("slot6_itemID") == transmissionID)
        {
            imgCell5.color = colorActive;
            imgIconCell5.color = colorActive;
            setDetailCount++;

            imgCell5.sprite = imgGarageCell5.sprite;
        }
        else
        {
            imgCell5.color = colorInactive;
            imgIconCell5.color = colorInactive;

            imgCell5.sprite = sprCellCommon;
        }
        #endregion

        tSetDetailCount.text = setDetailCount + "/5";        

        string newText = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_" + setCard.setID + "Desk");

        var replacement1 = newText.Replace("{value}", setCard.value1.ToString());

        tDescription1.text = replacement1;

        var replacement2 = newText.Replace("{value}", setCard.value2.ToString());

        tDescription2.text = replacement2;

        var replacement3 = newText.Replace("{value}", setCard.value3.ToString());

        tDescription3.text = replacement3;

        CheckSet();
    }

    bool checkActiveItem()
    {
        if (PlayerPrefs.GetInt("slot2_itemNumInInventory") == garageController.activeItem.itemNumInInventory)
        {
            return true;
        }

        if (PlayerPrefs.GetInt("slot3_itemNumInInventory") == garageController.activeItem.itemNumInInventory)
        {
            return true;
        }

        if (PlayerPrefs.GetInt("slot4_itemNumInInventory") == garageController.activeItem.itemNumInInventory)
        {
            return true;
        }

        if (PlayerPrefs.GetInt("slot5_itemNumInInventory") == garageController.activeItem.itemNumInInventory)
        {
            return true;
        }

        if (PlayerPrefs.GetInt("slot6_itemNumInInventory") == garageController.activeItem.itemNumInInventory)
        {
            return true;
        }

        return false;
    }

    public void OpenPopUp()
    {
        Initialize();
        _popUpController.OpenPopUp();
    }

    public void ClosedPopUp()
    {
        _popUpController.ClosedPopUp();
    }    

    public void CheckSet()
    {
        setDetailCount = 0;

        string setName = "0";

        foreach (SetCard _card in setCards)
        {
            if (_card.setID == setID)
            {
                setCard = _card;
            }
        }

        for (int i = 1; i <= 10; i++)
        {
            setDetailCount = 0;

            int engineID = 0;
            int brakesID = 0;
            int fuelSystemID = 0;
            int suspensionID = 0;
            int transmissionID = 0;

            switch (i)
            {
                case 1:
                    engineID = 2001;
                    brakesID = 3001;
                    fuelSystemID = 4001;
                    suspensionID = 5001;
                    transmissionID = 6001;

                    setName = "s01";
                    break;
                case 2:
                    engineID = 2002;
                    brakesID = 3002;
                    fuelSystemID = 4002;
                    suspensionID = 5002;
                    transmissionID = 6002;

                    setName = "s02";
                    break;
                case 3:
                    engineID = 2003;
                    brakesID = 3003;
                    fuelSystemID = 4003;
                    suspensionID = 5003;
                    transmissionID = 6003;

                    setName = "s03";
                    break;
                case 4:
                    engineID = 2004;
                    brakesID = 3004;
                    fuelSystemID = 4004;
                    suspensionID = 5004;
                    transmissionID = 6004;

                    setName = "s04";
                    break;
                case 5:
                    engineID = 2005;
                    brakesID = 3005;
                    fuelSystemID = 4005;
                    suspensionID = 5005;
                    transmissionID = 6005;

                    setName = "s05";
                    break;
                case 6:
                    engineID = 2006;
                    brakesID = 3006;
                    fuelSystemID = 4006;
                    suspensionID = 5006;
                    transmissionID = 6006;

                    setName = "s06";
                    break;
                case 7:
                    engineID = 2007;
                    brakesID = 3007;
                    fuelSystemID = 4007;
                    suspensionID = 5007;
                    transmissionID = 6007;

                    setName = "s07";
                    break;
                case 8:
                    engineID = 2008;
                    brakesID = 3008;
                    fuelSystemID = 4008;
                    suspensionID = 5008;
                    transmissionID = 6008;

                    setName = "s08";
                    break;
                case 9:
                    engineID = 2009;
                    brakesID = 3009;
                    fuelSystemID = 4009;
                    suspensionID = 5009;
                    transmissionID = 6009;

                    setName = "s09";
                    break;
                case 10:
                    engineID = 2010;
                    brakesID = 3010;
                    fuelSystemID = 4010;
                    suspensionID = 5010;
                    transmissionID = 6010;

                    setName = "s10";
                    break;
            }

            if (PlayerPrefs.GetInt("slot2_itemID") == engineID)
            {
                setDetailCount++;
            }

            if (PlayerPrefs.GetInt("slot3_itemID") == brakesID)
            {
                setDetailCount++;
            }

            if (PlayerPrefs.GetInt("slot4_itemID") == fuelSystemID)
            {
                setDetailCount++;
            }

            if (PlayerPrefs.GetInt("slot5_itemID") == suspensionID)
            {
                setDetailCount++;
            }

            if (PlayerPrefs.GetInt("slot6_itemID") == transmissionID)
            {
                setDetailCount++;
            }

            if (setDetailCount >= 3)
            {
                goto l1;
            }
        }

        l1:

        if (setDetailCount < 3)
        {
            PlayerPrefs.SetInt("setActive", 0);
            PlayerPrefs.SetFloat("setValue", 0);

            PlayerPrefs.SetString("setActiveID", "0");

            imgBonusPanel1.sprite = sprBonusPanelInactive;
            imgBonusPanel2.sprite = sprBonusPanelInactive;
            imgBonusPanel3.sprite = sprBonusPanelInactive;
        } 
        else
        {
            PlayerPrefs.SetInt("setActive", 1);
            PlayerPrefs.SetString("setActiveID", setName);

            if (setCard.setID == setName)
            {
                if (setDetailCount == 3)
                {
                    PlayerPrefs.SetFloat("setValue", setCard.value1);

                    imgBonusPanel1.sprite = sprBonusPanelActive;
                    imgBonusPanel2.sprite = sprBonusPanelInactive;
                    imgBonusPanel3.sprite = sprBonusPanelInactive;
                }

                if (setDetailCount == 4)
                {
                    PlayerPrefs.SetFloat("setValue", setCard.value2);

                    imgBonusPanel1.sprite = sprBonusPanelInactive;
                    imgBonusPanel2.sprite = sprBonusPanelActive;
                    imgBonusPanel3.sprite = sprBonusPanelInactive;
                }

                if (setDetailCount == 5)
                {
                    PlayerPrefs.SetFloat("setValue", setCard.value3);

                    imgBonusPanel1.sprite = sprBonusPanelInactive;
                    imgBonusPanel2.sprite = sprBonusPanelInactive;
                    imgBonusPanel3.sprite = sprBonusPanelActive;
                }
            }
        }        

        Debug.Log("Set Settings = " + PlayerPrefs.GetString("setActiveID"));

        CheckSetVisual();
    }

    void CheckSetVisual()
    {
        setDetailCount = 0;

        foreach (SetCard _card in setCards)
        {
            if (_card.setID == setID)
            {
                setCard = _card;
            }
        }

        setDetailCount = 0;

        int engineID = 0;
        int brakesID = 0;
        int fuelSystemID = 0;
        int suspensionID = 0;
        int transmissionID = 0;

        switch (setID)
        {
            case "s01":
                engineID = 2001;
                brakesID = 3001;
                fuelSystemID = 4001;
                suspensionID = 5001;
                transmissionID = 6001;
                break;
            case "s02":
                engineID = 2002;
                brakesID = 3002;
                fuelSystemID = 4002;
                suspensionID = 5002;
                transmissionID = 6002;
                break;
            case "s03":
                engineID = 2003;
                brakesID = 3003;
                fuelSystemID = 4003;
                suspensionID = 5003;
                transmissionID = 6003;
                break;
            case "s04":
                engineID = 2004;
                brakesID = 3004;
                fuelSystemID = 4004;
                suspensionID = 5004;
                transmissionID = 6004;
                break;
            case "s05":
                engineID = 2005;
                brakesID = 3005;
                fuelSystemID = 4005;
                suspensionID = 5005;
                transmissionID = 6005;
                break;
            case "s06":
                engineID = 2006;
                brakesID = 3006;
                fuelSystemID = 4006;
                suspensionID = 5006;
                transmissionID = 6006;
                break;
            case "s07":
                engineID = 2007;
                brakesID = 3007;
                fuelSystemID = 4007;
                suspensionID = 5007;
                transmissionID = 6007;
                break;
            case "s08":
                engineID = 2008;
                brakesID = 3008;
                fuelSystemID = 4008;
                suspensionID = 5008;
                transmissionID = 6008;
                break;
            case "s09":
                engineID = 2009;
                brakesID = 3009;
                fuelSystemID = 4009;
                suspensionID = 5009;
                transmissionID = 6009;
                break;
            case "s10":
                engineID = 2010;
                brakesID = 3010;
                fuelSystemID = 4010;
                suspensionID = 5010;
                transmissionID = 6010;
                break;
        }

        if (PlayerPrefs.GetInt("slot2_itemID") == engineID)
        {
            setDetailCount++;
        }

        if (PlayerPrefs.GetInt("slot3_itemID") == brakesID)
        {
            setDetailCount++;
        }

        if (PlayerPrefs.GetInt("slot4_itemID") == fuelSystemID)
        {
            setDetailCount++;
        }

        if (PlayerPrefs.GetInt("slot5_itemID") == suspensionID)
        {
            setDetailCount++;
        }

        if (PlayerPrefs.GetInt("slot6_itemID") == transmissionID)
        {
            setDetailCount++;
        }

        if (setDetailCount < 3)
        {
            imgBonusPanel1.sprite = sprBonusPanelInactive;
            imgBonusPanel2.sprite = sprBonusPanelInactive;
            imgBonusPanel3.sprite = sprBonusPanelInactive;
        }
        else
        {
            if (setDetailCount == 3)
            {
                imgBonusPanel1.sprite = sprBonusPanelActive;
                imgBonusPanel2.sprite = sprBonusPanelInactive;
                imgBonusPanel3.sprite = sprBonusPanelInactive;
            }

            if (setDetailCount == 4)
            {
                imgBonusPanel1.sprite = sprBonusPanelInactive;
                imgBonusPanel2.sprite = sprBonusPanelActive;
                imgBonusPanel3.sprite = sprBonusPanelInactive;
            }

            if (setDetailCount == 5)
            {
                imgBonusPanel1.sprite = sprBonusPanelInactive;
                imgBonusPanel2.sprite = sprBonusPanelInactive;
                imgBonusPanel3.sprite = sprBonusPanelActive;
            }
        }
    }
}
