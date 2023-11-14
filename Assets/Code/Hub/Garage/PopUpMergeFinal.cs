using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpMergeFinal : MonoBehaviour
{
    PopUpController _popUpController;
    public PopUpDetail popUpDetail;

    [Header("Stats")]
    public TMP_Text tName;
    public TMP_Text tRarity;
    public TMP_Text tLevel;
    public Image imgIcon;
    public Image imgCell;
    public string rarity;
    public string itemName;
    public int level;

    public DetailCard card;

    public Sprite sprItemIcon;
    public Sprite sprItemCell;

    [Header("Background")]
    public Image imgBackground;
    public Sprite sprBackgroundRare;
    public Sprite sprBackgroundEpic;
    public Sprite sprBackgroundLegendary;

    [Header("Panel New Stats")]
    public GameObject objPanel2;

    public Sprite sprHealth, sprDamage;
    public Image imgIconPanel1;
    public Image imgIconPanel2;
    public TMP_Text tNamePanel1;
    public TMP_Text tNamePanel2;

    public TMP_Text tCurrentLevel;
    public TMP_Text tNextLevel;

    public float currentPanelValue1, currentPanelValue2;
    public float nextPanelValue1, nextPanelValue2;

    public TMP_Text tCurrentPanelValue1;
    public TMP_Text tNextPanelValue1;

    public TMP_Text tCurrentPanelValue2;
    public TMP_Text tNextPanelValue2;

    [Header("Return Consumables")]    
    public int returnLevelValue1;
    public int returnLevelValue2;

    public int returnMoneyValue;
    public int returnDrawingValue;

    public GameObject objReturnMoney;
    public GameObject objReturnDrawing;
    public TMP_Text tMoneyCount;
    public TMP_Text tDrawingCount;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    void Initialize()
    {
        switch (rarity)
        {
            case "rare":
                imgBackground.sprite = sprBackgroundRare;
                tRarity.text = "<color=#3498DB>Редкая</color>";

                tCurrentLevel.text = "10";
                tNextLevel.text = "20";

                if (card.baseItemCharactersRare1 == DetailCard.ItemCharacters.HpUp)
                {
                    imgIconPanel1.sprite = sprHealth;

                    tNamePanel1.text = "Здоровье";
                }

                if (card.baseItemCharactersRare1 == DetailCard.ItemCharacters.DamageUp)
                {
                    imgIconPanel1.sprite = sprDamage;

                    tNamePanel1.text = "Урон";
                }

                tCurrentPanelValue1.text = currentPanelValue1 + "";
                tNextPanelValue1.text = nextPanelValue1 + "";

                if (card.baseItemCharactersRare2 != DetailCard.ItemCharacters.none)
                {
                    objPanel2.SetActive(true);

                    if (card.baseItemCharactersRare2 == DetailCard.ItemCharacters.HpUp)
                    {
                        imgIconPanel2.sprite = sprHealth;

                        tNamePanel2.text = "Здоровье";
                    }

                    if (card.baseItemCharactersRare2 == DetailCard.ItemCharacters.DamageUp)
                    {
                        imgIconPanel2.sprite = sprDamage;

                        tNamePanel2.text = "Урон";
                    }

                    tCurrentPanelValue2.text = currentPanelValue2 + "";
                    tNextPanelValue2.text = nextPanelValue2 + "";
                } else
                {
                    objPanel2.SetActive(false);
                }
                break;

            case "epic":
                imgBackground.sprite = sprBackgroundEpic;
                tRarity.text = "<color=#CE33FF>Эпическая</color>";

                tCurrentLevel.text = "20";
                tNextLevel.text = "30";

                if (card.baseItemCharactersEpic1 == DetailCard.ItemCharacters.HpUp)
                {
                    imgIconPanel1.sprite = sprHealth;

                    tNamePanel1.text = "Здоровье";
                }

                if (card.baseItemCharactersEpic1 == DetailCard.ItemCharacters.DamageUp)
                {
                    imgIconPanel1.sprite = sprDamage;

                    tNamePanel1.text = "Урон";
                }

                tCurrentPanelValue1.text = currentPanelValue1 + "";
                tNextPanelValue1.text = nextPanelValue1 + "";

                if (card.baseItemCharactersEpic2 != DetailCard.ItemCharacters.none)
                {
                    objPanel2.SetActive(true);

                    if (card.baseItemCharactersEpic2 == DetailCard.ItemCharacters.HpUp)
                    {
                        imgIconPanel2.sprite = sprHealth;

                        tNamePanel2.text = "Здоровье";
                    }

                    if (card.baseItemCharactersEpic2 == DetailCard.ItemCharacters.DamageUp)
                    {
                        imgIconPanel2.sprite = sprDamage;

                        tNamePanel2.text = "Урон";
                    }

                    tCurrentPanelValue2.text = currentPanelValue2 + "";
                    tNextPanelValue2.text = nextPanelValue2 + "";
                }
                else
                {
                    objPanel2.SetActive(false);
                }
                break;

            case "legendary":
                imgBackground.sprite = sprBackgroundLegendary;
                tRarity.text = "<color=yellow>Легендарная</color>";

                tCurrentLevel.text = "30";
                tNextLevel.text = "40";

                if (card.baseItemCharactersLegendary1 == DetailCard.ItemCharacters.HpUp)
                {
                    imgIconPanel1.sprite = sprHealth;

                    tNamePanel1.text = "Здоровье";
                }

                if (card.baseItemCharactersLegendary1 == DetailCard.ItemCharacters.DamageUp)
                {
                    imgIconPanel1.sprite = sprDamage;

                    tNamePanel1.text = "Урон";
                }

                tCurrentPanelValue1.text = currentPanelValue1 + "";
                tNextPanelValue1.text = nextPanelValue1 + "";

                if (card.baseItemCharactersLegendary2 != DetailCard.ItemCharacters.none)
                {
                    objPanel2.SetActive(true);

                    if (card.baseItemCharactersLegendary2 == DetailCard.ItemCharacters.HpUp)
                    {
                        imgIconPanel2.sprite = sprHealth;

                        tNamePanel2.text = "Здоровье";
                    }

                    if (card.baseItemCharactersLegendary2 == DetailCard.ItemCharacters.DamageUp)
                    {
                        imgIconPanel2.sprite = sprDamage;

                        tNamePanel2.text = "Урон";
                    }

                    tCurrentPanelValue2.text = currentPanelValue2 + "";
                    tNextPanelValue2.text = nextPanelValue2 + "";
                }
                else
                {
                    objPanel2.SetActive(false);
                }
                break;
        }

        tName.text = itemName;
        imgIcon.sprite = sprItemIcon;
        imgCell.sprite = sprItemCell;
        tLevel.text = "Lv " + level;

        //Return Level 1
        returnMoneyValue = 0;
        returnDrawingValue = 0;

        if (returnLevelValue1 > 0)
        {
            for (int i = 1; i < returnLevelValue1; i++)
            {
                returnMoneyValue += popUpDetail.upgradePrice[i];
                returnDrawingValue += popUpDetail.drawingCount[i];
            }
        }

        if (returnLevelValue2 > 0)
        {
            for (int i = 1; i < returnLevelValue2; i++)
            {
                returnMoneyValue += popUpDetail.upgradePrice[i];
                returnDrawingValue += popUpDetail.drawingCount[i];
            }
        }

        if (returnMoneyValue != 0)
        {
            objReturnMoney.SetActive(true);
            objReturnDrawing.SetActive(true);

            tMoneyCount.text = "x" + returnMoneyValue;
            tDrawingCount.text = "x" + returnDrawingValue;

            PlayerPrefs.SetInt("playerMoney", PlayerPrefs.GetInt("playerMoney") + returnMoneyValue);

            switch (card.itemType)
            {
                case DetailCard.ItemType.Gun:
                    PlayerPrefs.SetInt("drawingGunCount", PlayerPrefs.GetInt("drawingGunCount") + returnDrawingValue);
                    break;

                case DetailCard.ItemType.Engine:
                    PlayerPrefs.SetInt("drawingEngineCount", PlayerPrefs.GetInt("drawingEngineCount") + returnDrawingValue);
                    break;

                case DetailCard.ItemType.Brakes:
                    PlayerPrefs.SetInt("drawingBrakesCount", PlayerPrefs.GetInt("drawingBrakesCount") + returnDrawingValue);
                    break;

                case DetailCard.ItemType.FuelSystem:
                    PlayerPrefs.SetInt("drawingFuelSystemCount", PlayerPrefs.GetInt("drawingFuelSystemCount") + returnDrawingValue);
                    break;

                case DetailCard.ItemType.Suspension:
                    PlayerPrefs.SetInt("drawingSuspensionCount", PlayerPrefs.GetInt("drawingSuspensionCount") + returnDrawingValue);
                    break;

                case DetailCard.ItemType.Transmission:
                    PlayerPrefs.SetInt("drawingTransmissionCount", PlayerPrefs.GetInt("drawingTransmissionCount") + returnDrawingValue);
                    break;
            }
        } 
        else
        {
            objReturnMoney.SetActive(false);
            objReturnDrawing.SetActive(false);
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
}
