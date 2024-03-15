using AssetKits.ParticleImage;
using DG.Tweening;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GUI;

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

    public GameObject paricleImage;

    [Header("Background")]
    public Image imgBackground;
    public Image imgBackground2, imgBackground3;
    public Sprite sprBackgroundRare;
    public Sprite sprBackgroundEpic;
    public Sprite sprBackgroundLegendary;

    [Header("Panel New Stats")]
    public GameObject objPanel0;
    public GameObject objPanel1;
    public GameObject objPanel2;
    private bool objPanel2Open;

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

    public GameObject butOk;

    [Header("Sounds")]
    public AudioClip clipNewItem;

    public Sprite sprDrawingGun, sprDrawingEngine, sprDrawingBrakes, sprDrawingFuelSystem, sprDrawingSuspension, sprDrawingTransmission;
    public Image imgDrawing;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    void Initialize()
    {
        SoundController _soundController = GameObject.Find("SoundsController").GetComponent<SoundController>();

        if (_soundController != null)
        {
            _soundController.PlaySound(clipNewItem);
        }

        switch (rarity)
        {
            case "rare":
                imgBackground.sprite = sprBackgroundRare;
                imgBackground2.sprite = sprBackgroundRare;
                imgBackground3.sprite = sprBackgroundRare;
                tRarity.text = "<color=#3498DB>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rare") + "</color>";

                tCurrentLevel.text = "10";
                tNextLevel.text = "20";

                if (card.baseItemCharactersRare1 == DetailCard.ItemCharacters.HpUp)
                {
                    imgIconPanel1.sprite = sprHealth;

                    tNamePanel1.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameHealth");
                }

                if (card.baseItemCharactersRare1 == DetailCard.ItemCharacters.DamageUp)
                {
                    imgIconPanel1.sprite = sprDamage;

                    tNamePanel1.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameDamage");
                }

                tCurrentPanelValue1.text = currentPanelValue1 + "";
                tNextPanelValue1.text = nextPanelValue1 + "";

                if (card.baseItemCharactersRare2 != DetailCard.ItemCharacters.none)
                {
                    //objPanel2.SetActive(true);
                    objPanel2Open = true;

                    if (card.baseItemCharactersRare2 == DetailCard.ItemCharacters.HpUp)
                    {
                        imgIconPanel2.sprite = sprHealth;

                        tNamePanel2.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameHealth");
                    }

                    if (card.baseItemCharactersRare2 == DetailCard.ItemCharacters.DamageUp)
                    {
                        imgIconPanel2.sprite = sprDamage;

                        tNamePanel2.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameDamage");
                    }

                    tCurrentPanelValue2.text = currentPanelValue2 + "";
                    tNextPanelValue2.text = nextPanelValue2 + "";
                } else
                {
                    //objPanel2.SetActive(false);
                    objPanel2Open = false;
                }
                break;

            case "epic":
                imgBackground.sprite = sprBackgroundEpic;
                imgBackground2.sprite = sprBackgroundEpic;
                imgBackground3.sprite = sprBackgroundEpic;
                tRarity.text = "<color=#CE33FF>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_epic") + "</color>";

                tCurrentLevel.text = "20";
                tNextLevel.text = "30";

                if (card.baseItemCharactersEpic1 == DetailCard.ItemCharacters.HpUp)
                {
                    imgIconPanel1.sprite = sprHealth;

                    tNamePanel1.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameHealth");
                }

                if (card.baseItemCharactersEpic1 == DetailCard.ItemCharacters.DamageUp)
                {
                    imgIconPanel1.sprite = sprDamage;

                    tNamePanel1.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameDamage");
                }

                tCurrentPanelValue1.text = currentPanelValue1 + "";
                tNextPanelValue1.text = nextPanelValue1 + "";

                if (card.baseItemCharactersEpic2 != DetailCard.ItemCharacters.none)
                {
                    //objPanel2.SetActive(true);
                    objPanel2Open = true;

                    if (card.baseItemCharactersEpic2 == DetailCard.ItemCharacters.HpUp)
                    {
                        imgIconPanel2.sprite = sprHealth;

                        tNamePanel2.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameHealth");
                    }

                    if (card.baseItemCharactersEpic2 == DetailCard.ItemCharacters.DamageUp)
                    {
                        imgIconPanel2.sprite = sprDamage;

                        tNamePanel2.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameDamage");
                    }

                    tCurrentPanelValue2.text = currentPanelValue2 + "";
                    tNextPanelValue2.text = nextPanelValue2 + "";
                }
                else
                {
                    //objPanel2.SetActive(false);
                    objPanel2Open = false;
                }
                break;

            case "legendary":
                imgBackground.sprite = sprBackgroundLegendary;
                imgBackground2.sprite = sprBackgroundLegendary;
                imgBackground3.sprite = sprBackgroundLegendary;
                tRarity.text = "<color=yellow>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_legendary") + "</color>";

                tCurrentLevel.text = "30";
                tNextLevel.text = "40";

                if (card.baseItemCharactersLegendary1 == DetailCard.ItemCharacters.HpUp)
                {
                    imgIconPanel1.sprite = sprHealth;

                    tNamePanel1.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameHealth");
                }

                if (card.baseItemCharactersLegendary1 == DetailCard.ItemCharacters.DamageUp)
                {
                    imgIconPanel1.sprite = sprDamage;

                    tNamePanel1.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameDamage");
                }

                tCurrentPanelValue1.text = currentPanelValue1 + "";
                tNextPanelValue1.text = nextPanelValue1 + "";

                if (card.baseItemCharactersLegendary2 != DetailCard.ItemCharacters.none)
                {
                    //objPanel2.SetActive(true);
                    objPanel2Open = true;

                    if (card.baseItemCharactersLegendary2 == DetailCard.ItemCharacters.HpUp)
                    {
                        imgIconPanel2.sprite = sprHealth;

                        tNamePanel2.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameHealth");
                    }

                    if (card.baseItemCharactersLegendary2 == DetailCard.ItemCharacters.DamageUp)
                    {
                        imgIconPanel2.sprite = sprDamage;

                        tNamePanel2.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameDamage");
                    }

                    tCurrentPanelValue2.text = currentPanelValue2 + "";
                    tNextPanelValue2.text = nextPanelValue2 + "";
                }
                else
                {
                    //objPanel2.SetActive(false);
                    objPanel2Open = false;
                }
                break;
        }

        tName.text = "<wave>" + itemName;
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

        if (returnMoneyValue > 0)
        {
            objReturnMoney.SetActive(true);
            objReturnDrawing.SetActive(true);

            tMoneyCount.text = "x" + returnMoneyValue;
            tDrawingCount.text = "x" + returnDrawingValue;

            PlayerPrefs.SetInt("playerMoney", PlayerPrefs.GetInt("playerMoney") + returnMoneyValue);

            switch (card.itemType)
            {
                case DetailCard.ItemType.Gun:
                    imgDrawing.sprite = sprDrawingGun;
                    PlayerPrefs.SetInt("drawingGunCount", PlayerPrefs.GetInt("drawingGunCount") + returnDrawingValue);
                    break;

                case DetailCard.ItemType.Engine:
                    imgDrawing.sprite = sprDrawingEngine;
                    PlayerPrefs.SetInt("drawingEngineCount", PlayerPrefs.GetInt("drawingEngineCount") + returnDrawingValue);
                    break;

                case DetailCard.ItemType.Brakes:
                    imgDrawing.sprite = sprDrawingBrakes;
                    PlayerPrefs.SetInt("drawingBrakesCount", PlayerPrefs.GetInt("drawingBrakesCount") + returnDrawingValue);
                    break;

                case DetailCard.ItemType.FuelSystem:
                    imgDrawing.sprite = sprDrawingFuelSystem;
                    PlayerPrefs.SetInt("drawingFuelSystemCount", PlayerPrefs.GetInt("drawingFuelSystemCount") + returnDrawingValue);
                    break;

                case DetailCard.ItemType.Suspension:
                    imgDrawing.sprite = sprDrawingSuspension;
                    PlayerPrefs.SetInt("drawingSuspensionCount", PlayerPrefs.GetInt("drawingSuspensionCount") + returnDrawingValue);
                    break;

                case DetailCard.ItemType.Transmission:
                    imgDrawing.sprite = sprDrawingTransmission;
                    PlayerPrefs.SetInt("drawingTransmissionCount", PlayerPrefs.GetInt("drawingTransmissionCount") + returnDrawingValue);
                    break;
            }
        } 
        else
        {
            objReturnMoney.SetActive(false);
            objReturnDrawing.SetActive(false);
        }

        GameObject.Find("GameCloud").GetComponent<GameCloud>().SaveData();
    }

    public void ButOpen()
    {
        _popUpController.OpenPopUp();

        Initialize();
        StartCoroutine(Animation());
    }

    public void ButClosed()
    {
        _popUpController.ClosedPopUp();
    }

    IEnumerator Animation()
    {
        tName.transform.DOScale(0, 0);
        imgCell.transform.DOScale(0, 0);
        paricleImage.transform.DOScale(0, 0);
        objPanel0.transform.DOScale(0, 0);
        objPanel1.transform.DOScale(0, 0);
        objPanel2.transform.DOScale(0, 0);
        objReturnMoney.transform.DOScale(0, 0);
        objReturnDrawing.transform.DOScale(0, 0);
        butOk.transform.DOScale(0, 0);

        tName.gameObject.SetActive(false);
        imgCell.gameObject.SetActive(false);
        paricleImage.SetActive(false);
        objPanel0.SetActive(false);
        objPanel1.SetActive(false);
        objPanel2.SetActive(false);
        objReturnMoney.SetActive(false);
        objReturnDrawing.SetActive(false);
        butOk.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        tName.gameObject.SetActive(true);
        tName.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);

        imgCell.gameObject.SetActive(true);
        imgCell.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);

        paricleImage.SetActive(true);
        paricleImage.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);

        yield return new WaitForSeconds(0.5f);

        objPanel0.SetActive(true);
        objPanel0.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);

        yield return new WaitForSeconds(0.5f);

        objPanel1.SetActive(true);
        objPanel1.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);

        yield return new WaitForSeconds(0.5f);

        if (objPanel2Open)
        {
            objPanel2.SetActive(true);
            objPanel2.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);
        } 
        else
        {
            if (returnMoneyValue > 0)
            {
                objReturnMoney.SetActive(true);
                objReturnDrawing.SetActive(true);

                objReturnMoney.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);
                objReturnDrawing.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);
            }
        }

        yield return new WaitForSeconds(0.5f);

        if (objPanel2Open)
        {
            if (returnMoneyValue > 0)
            {
                objReturnMoney.SetActive(true);
                objReturnDrawing.SetActive(true);

                objReturnMoney.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);
                objReturnDrawing.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);
            }
        } 
        else
        {
            butOk.SetActive(true);
            butOk.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);
        }

        yield return new WaitForSeconds(0.5f);

        if (objPanel2Open)
        {
            butOk.SetActive(true);
            butOk.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);
        }
    }
}
