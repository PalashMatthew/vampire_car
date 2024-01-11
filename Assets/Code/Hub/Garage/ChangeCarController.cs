using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCarController : MonoBehaviour
{
    public GameObject canvasGarage;
    public PlayerHubVisual playerHubVisual;
    [HideInInspector] public CarButtonController _activeCarObj;

    public List<GameObject> carsButtons;

    PopUpController _popUpController;

    [Header("Stats")]
    public TMP_Text tDamage;
    public TMP_Text tHealth;
    public TMP_Text tKrit;
    public TMP_Text tDodge;

    [Header("Car Level")]
    public TMP_Text tCurrentLevel;
    public Image fillCarLevel;
    public Image fillEndCarLevel;

    [Header("Progress Bar Fill")]
    public Image fillDamage;
    public Image fillHealth;
    public Image fillKrit;
    public Image fillDodge;

    [Header("Progress Bar Fill End")]
    public Image fillEndDamage;
    public Image fillEndHealth;
    public Image fillEndKrit;
    public Image fillEndDodge;

    [Header("Progress Bar Fill Max")]
    public Image fillDamageMax;
    public Image fillHealthMax;
    public Image fillKritMax;
    public Image fillDodgeMax;

    [Header("Base")]
    public Image imgCar;
    public TMP_Text tCarName;

    [Header("Selected Menu")]
    public GameObject objChoose;
    public GameObject objTActive;

    [Header("Purchased")]
    public GameObject butUpgrade;
    public GameObject butPurchase;
    public GameObject objButPurchaseSoft;
    public GameObject objButPurchaseHard;
    public GameObject objButPurchaseReal;
    public TMP_Text tPriceSoft;
    public TMP_Text tPriceHard;
    public TMP_Text tPriceReal;

    [Header("Talant")]
    public TMP_Text tTalant;
    public Image imgTalant;
    public Sprite sprTalantDionysus;
    public Sprite sprTalantTaiowa;
    public Sprite sprTalantPRun;
    public Sprite sprTalantLyssa;
    public Sprite sprTalantAeolus;
    public Sprite sprTalantHyas;
    public Sprite sprTalantHemera;
    public Sprite sprTalantEos;

    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    public void OpenPopUp()
    {
        for (int i = 0; i < carsButtons.Count; i++)
        {
            carsButtons[i].GetComponent<CarButtonController>().Initialize();            
        }

        foreach (GameObject _car in carsButtons)
        {
            if (_car.GetComponent<CarButtonController>().carName == PlayerPrefs.GetString("selectedCarID"))
            {
                _car.GetComponent<CarButtonController>().ButSelect();
                _activeCarObj = _car.GetComponent<CarButtonController>();
            }
        }
    }

    public void SelectCar(GameObject _carItem)
    {
        int _carID = _carItem.GetComponent<CarButtonController>().carID;
        string _carName = _carItem.GetComponent<CarButtonController>().carName;
        Sprite _sprCarImage = _carItem.GetComponent<CarButtonController>().imgCar.sprite;

        _activeCarObj = _carItem.GetComponent<CarButtonController>();

        for (int i = 0; i < carsButtons.Count; i++)
        {
            if (carsButtons[i] != _carItem)
                carsButtons[i].GetComponent<CarButtonController>().ButUnselect();
        }       

        tCarName.text = _carName;
        imgCar.sprite = _sprCarImage;

        Sequence imgCarAnim = DOTween.Sequence();
        imgCarAnim.Append(imgCar.transform.DOScale(1.15f, 0.1f));
        imgCarAnim.Append(imgCar.transform.DOScale(1f, 0.1f));

        UpdateProgressBar();

        #region Button Upgrade Settings
        if (PlayerPrefs.GetInt(_carName + "carPurchased") == 1)
        {
            butPurchase.SetActive(false);
            butUpgrade.SetActive(true);
        }
        else
        {
            butPurchase.SetActive(true);
            butUpgrade.SetActive(false);

            if (_carItem.GetComponent<CarButtonController>().priceType == CarButtonController.PriceType.Soft)
            {
                objButPurchaseSoft.SetActive(true);
                objButPurchaseHard.SetActive(false);
                objButPurchaseReal.SetActive(false);

                tPriceSoft.text = _carItem.GetComponent<CarButtonController>().price + "";
            }

            if (_carItem.GetComponent<CarButtonController>().priceType == CarButtonController.PriceType.Hard)
            {
                objButPurchaseSoft.SetActive(false);
                objButPurchaseHard.SetActive(true);
                objButPurchaseReal.SetActive(false);

                tPriceHard.text = _carItem.GetComponent<CarButtonController>().price + "";
            }

            if (_carItem.GetComponent<CarButtonController>().priceType == CarButtonController.PriceType.Real)
            {
                objButPurchaseSoft.SetActive(false);
                objButPurchaseHard.SetActive(false);
                objButPurchaseReal.SetActive(true);

                //tPriceReal.text = _carItem.GetComponent<CarButtonController>().price + "р";
                tPriceReal.text = GameObject.Find("HubController").GetComponent<ShopController>().prices[_carItem.GetComponent<CarButtonController>().carShopID];
            }
        }
        #endregion

        #region Устанавливаем уровень машины
        tCurrentLevel.text = PlayerPrefs.GetInt(_carName + "carLevel") + "/40";
        fillCarLevel.DOFillAmount((float)PlayerPrefs.GetInt(_carName + "carLevel") / 40, 0.5f);
        fillEndCarLevel.GetComponent<RectTransform>().DOAnchorPos(new Vector2((float)PlayerPrefs.GetInt(_carName + "carLevel") / 40 * 132f, 0), 0.5f);
        #endregion

        #region Selection Menu
        if (PlayerPrefs.GetInt(_carName + "carPurchased") == 1)
        {
            if (PlayerPrefs.GetString("selectedCarID") == _carName)
            {
                objTActive.gameObject.SetActive(true);
                objChoose.SetActive(false);
            } 
            else
            {
                objTActive.gameObject.SetActive(false);
                objChoose.SetActive(true);
            }
        } 
        else
        {
            objTActive.gameObject.SetActive(false);
            objChoose.SetActive(false);
        }
        #endregion

        #region Talant
        if (_carName == "Dionysus")
        {
            tTalant.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_carSkillDionisus");
            imgTalant.sprite = sprTalantDionysus;
        }

        if (_carName == "Taiowa")
        {
            tTalant.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_carSkillTaiowa");
            imgTalant.sprite = sprTalantDionysus;
        }

        if (_carName == "P-Run")
        {
            tTalant.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_carSkillP-Run");
            imgTalant.sprite = sprTalantDionysus;
        }

        if (_carName == "Lyssa")
        {
            tTalant.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_carSkillLyssa");
            imgTalant.sprite = sprTalantDionysus;
        }

        if (_carName == "Aeolus")
        {
            tTalant.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_carSkillAeolus");
            imgTalant.sprite = sprTalantDionysus;
        }

        if (_carName == "Hyas")
        {
            tTalant.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_carSkillHyas");
            imgTalant.sprite = sprTalantDionysus;
        }

        if (_carName == "Hemera")
        {
            tTalant.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_carSkillHemera");
            imgTalant.sprite = sprTalantDionysus;
        }

        if (_carName == "Eos")
        {
            tTalant.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_carSkillEos");
            imgTalant.sprite = sprTalantDionysus;
        }
        #endregion
    }

    public void ChooseCar()
    {
        PlayerPrefs.SetString("selectedCarID", _activeCarObj.carName);

        #region Selection Menu
        if (PlayerPrefs.GetInt(_activeCarObj.carName + "carPurchased") == 1)
        {
            if (PlayerPrefs.GetString("selectedCarID") == _activeCarObj.carName)
            {
                objTActive.gameObject.SetActive(true);
                objChoose.SetActive(false);
            }
            else
            {
                objTActive.gameObject.SetActive(false);
                objChoose.SetActive(true);
            }
        }
        else
        {
            objTActive.gameObject.SetActive(false);
            objChoose.SetActive(false);
        }
        #endregion

        playerHubVisual.ChangeCar();

        GameObject.Find("GameCloud").GetComponent<GameCloud>().SaveData();
    }

    public void ButBack()
    {
        canvasGarage.SetActive(true);
        _popUpController.ClosedPopUp();
    }

    public void ButBuyCar()
    {
        bool _isPurchaseSuccess = false;

        if (_activeCarObj.priceType == CarButtonController.PriceType.Soft)
        {
            if (PlayerPrefs.GetInt("playerMoney") >= _activeCarObj.price)
            {
                PlayerPrefs.SetInt("playerMoney", PlayerPrefs.GetInt("playerMoney") - (int)_activeCarObj.price);
                PlayerPrefs.SetInt(_activeCarObj.carName + "carPurchased", 1);
                _activeCarObj.Initialize();

                GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_BuyCar(_activeCarObj.carName, "Money", _activeCarObj.price);

                _isPurchaseSuccess = true;

                GameObject.Find("GameCloud").GetComponent<GameCloud>().SaveData();
            }
        }

        if (_activeCarObj.priceType == CarButtonController.PriceType.Hard)
        {
            if (PlayerPrefs.GetInt("playerHard") >= _activeCarObj.price)
            {
                PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") - (int)_activeCarObj.price);
                PlayerPrefs.SetInt(_activeCarObj.carName + "carPurchased", 1);
                _activeCarObj.Initialize();

                GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_BuyCar(_activeCarObj.carName, "Hard", _activeCarObj.price);

                _isPurchaseSuccess = true;

                GameObject.Find("GameCloud").GetComponent<GameCloud>().SaveData();
            }
        }

        if (_activeCarObj.priceType == CarButtonController.PriceType.Real)
        {
            GameObject.Find("HubController").GetComponent<ShopController>().ButBuyCar(_activeCarObj.carShopID);            
        }

        if (_isPurchaseSuccess)
        {
            PlayerPrefs.SetString("selectedCarID", _activeCarObj.carName);

            #region Selection Menu
            if (PlayerPrefs.GetInt(_activeCarObj.carName + "carPurchased") == 1)
            {
                if (PlayerPrefs.GetString("selectedCarID") == _activeCarObj.carName)
                {
                    objTActive.gameObject.SetActive(true);
                    objChoose.SetActive(false);
                }
                else
                {
                    objTActive.gameObject.SetActive(false);
                    objChoose.SetActive(true);
                }
            }
            else
            {
                objTActive.gameObject.SetActive(false);
                objChoose.SetActive(false);
            }
            #endregion

            butPurchase.SetActive(false);
            butUpgrade.SetActive(true);

            playerHubVisual.ChangeCar();

            UpdateProgressBar();

            GameObject.Find("GameCloud").GetComponent<GameCloud>().SaveData();
        }
    }

    public void BuyCarCallBack()
    {
        PlayerPrefs.SetInt(_activeCarObj.carName + "carPurchased", 1);
        _activeCarObj.Initialize();

        PlayerPrefs.SetString("selectedCarID", _activeCarObj.carName);

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_BuyCar(_activeCarObj.carName, "Real", 0);

        #region Selection Menu
        if (PlayerPrefs.GetInt(_activeCarObj.carName + "carPurchased") == 1)
        {
            if (PlayerPrefs.GetString("selectedCarID") == _activeCarObj.carName)
            {
                objTActive.gameObject.SetActive(true);
                objChoose.SetActive(false);
            }
            else
            {
                objTActive.gameObject.SetActive(false);
                objChoose.SetActive(true);
            }
        }
        else
        {
            objTActive.gameObject.SetActive(false);
            objChoose.SetActive(false);
        }
        #endregion

        butPurchase.SetActive(false);
        butUpgrade.SetActive(true);

        playerHubVisual.ChangeCar();

        UpdateProgressBar();

        GameObject.Find("GameCloud").GetComponent<GameCloud>().SaveData();
    }

    public void UpdateProgressBar()
    {
        string _carName = _activeCarObj.carName;

        if (PlayerPrefs.GetInt(_carName + "carPurchased") == 1)
        {
            tDamage.text = "+" + PlayerPrefs.GetFloat(_carName + "carDamage") + "%";
            tHealth.text = PlayerPrefs.GetFloat(_carName + "carHealth") + "";
            tKrit.text = "+" + PlayerPrefs.GetFloat(_carName + "carKritChance") + "%";
            tDodge.text = "+" + PlayerPrefs.GetFloat(_carName + "carDodge") + "%";
        }
        else
        {
            tDamage.text = "+" + PlayerPrefs.GetFloat(_carName + "carDamage") + "% (<color=green>+" + PlayerPrefs.GetFloat(_carName + "carDamageStepUp") + "%</color>)";
            tHealth.text = PlayerPrefs.GetFloat(_carName + "carHealth") + " (<color=green>+" + PlayerPrefs.GetFloat(_carName + "carHealthStepUp") + "</color>)";
            tKrit.text = "+" + PlayerPrefs.GetFloat(_carName + "carKritChance") + "% (<color=green>+" + PlayerPrefs.GetFloat(_carName + "carKritChanceStepUp") + "%</color>)";
            tDodge.text = "+" + PlayerPrefs.GetFloat(_carName + "carDodge") + "% (<color=green>+" + PlayerPrefs.GetFloat(_carName + "carDodgeStepUp") + "%</color>)";
        }

        float _fillDamage = (float)PlayerPrefs.GetFloat(_carName + "carDamage") / 170;
        float _fillHealth = (float)PlayerPrefs.GetFloat(_carName + "carHealth") / 300;
        float _fillKrit = (float)PlayerPrefs.GetFloat(_carName + "carKritChance") / 7.2f;
        float _fillDodge = (float)PlayerPrefs.GetFloat(_carName + "carDodge") / 28;

        fillDamage.DOFillAmount(_fillDamage, 0.5f);
        fillHealth.DOFillAmount(_fillHealth, 0.5f);
        fillKrit.DOFillAmount(_fillKrit, 0.5f);
        fillDodge.DOFillAmount(_fillDodge, 0.5f);

        if (_fillDamage > 0 && _fillDamage < 1)
        {
            fillEndDamage.gameObject.SetActive(true);
            fillEndDamage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(_fillDamage * 296, 0), 0.5f);
        }
        else
        {
            fillEndDamage.gameObject.SetActive(false);
        }

        if (_fillHealth > 0 && _fillHealth < 1)
        {
            fillEndHealth.gameObject.SetActive(true);
            fillEndHealth.GetComponent<RectTransform>().DOAnchorPos(new Vector2(_fillHealth * 296, 0), 0.5f);
        }
        else
        {
            fillEndHealth.gameObject.SetActive(false);
        }

        if (_fillKrit > 0 && _fillKrit < 1)
        {
            fillEndKrit.gameObject.SetActive(true);
            fillEndKrit.GetComponent<RectTransform>().DOAnchorPos(new Vector2(_fillKrit * 296, 0), 0.5f);
        }
        else
        {
            fillEndKrit.gameObject.SetActive(false);
        }

        if (_fillDodge > 0 && _fillDodge < 1)
        {
            fillEndDodge.gameObject.SetActive(true);
            fillEndDodge.GetComponent<RectTransform>().DOAnchorPos(new Vector2(_fillDodge * 296, 0), 0.5f);
        }
        else
        {
            fillEndDodge.gameObject.SetActive(false);
        }

        fillDamageMax.DOFillAmount((float)PlayerPrefs.GetFloat(_carName + "carDamageMax") / 170, 0.5f);
        fillHealthMax.DOFillAmount((float)PlayerPrefs.GetFloat(_carName + "carHealthMax") / 300, 0.5f);
        fillKritMax.DOFillAmount((float)PlayerPrefs.GetFloat(_carName + "carKritChanceMax") / 7.2f, 0.5f);
        fillDodgeMax.DOFillAmount((float)PlayerPrefs.GetFloat(_carName + "carDodgeMax") / 28, 0.5f);
    }
}
