using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopUpCarUpgrade : MonoBehaviour
{
    PopUpController _popUpController;

    public ChangeCarController changeCarController;
    private CarButtonController carItem;

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

    [Header("Upgrade")]
    public float upgradePrice;
    public TMP_Text tPrice;
    public GameObject butUpgrade;
    public GameObject butUpgradeGray;

    [Header("Open Characters")]
    public TMP_Text tUpgradeLvl10;
    public TMP_Text tUpgradeLvl20;
    public TMP_Text tUpgradeLvl30;
    public TMP_Text tUpgradeLvl40;
    public Image imgUpgrade10Lvl;
    public Image imgUpgrade20Lvl;
    public Image imgUpgrade30Lvl;
    public Image imgUpgrade40Lvl;
    public Sprite sprUpgradeLvlUnlock;
    public Sprite sprUpgradeLvlNotUnlock;
    public Image imgLockUpgrade10Lvl;
    public Image imgLockUpgrade20Lvl;
    public Image imgLockUpgrade30Lvl;
    public Image imgLockUpgrade40Lvl;



    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
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

    private void Initialize()
    {
        carItem = changeCarController._activeCarObj;

        int _carID = carItem.carID;
        string _carName = carItem.carName;
        Sprite _sprCarImage = carItem.imgCar.sprite;

        tDamage.text = "+" + PlayerPrefs.GetFloat(_carName + "carDamage") + "% (<color=green>+" + PlayerPrefs.GetFloat(_carName + "carDamageStepUp") + "%</color>)";
        tHealth.text = PlayerPrefs.GetFloat(_carName + "carHealth") + " (<color=green>+" + PlayerPrefs.GetFloat(_carName + "carHealthStepUp") + "</color>)";
        tKrit.text = "+" + PlayerPrefs.GetFloat(_carName + "carKritChance") + "% (<color=green>+" + PlayerPrefs.GetFloat(_carName + "carKritChanceStepUp") + "%</color>)";
        tDodge.text = "+" + PlayerPrefs.GetFloat(_carName + "carDodge") + "% (<color=green>+" + PlayerPrefs.GetFloat(_carName + "carDodgeStepUp") + "%</color>)";

        tCarName.text = _carName;
        imgCar.sprite = _sprCarImage;

        #region Заполняем прогресс бары
        float _fillDamage = (float)PlayerPrefs.GetFloat(_carName + "carDamage") / 300;
        float _fillHealth = (float)PlayerPrefs.GetFloat(_carName + "carHealth") / 3200;
        float _fillKrit = (float)PlayerPrefs.GetFloat(_carName + "carKritChance") / 25;
        float _fillDodge = (float)PlayerPrefs.GetFloat(_carName + "carDodge") / 20;

        fillDamage.DOFillAmount(_fillDamage, 0.5f);
        fillHealth.DOFillAmount(_fillHealth, 0.5f);
        fillKrit.DOFillAmount(_fillKrit, 0.5f);
        fillDodge.DOFillAmount(_fillDodge, 0.5f);

        if (_fillDamage > 0 && _fillDamage < 1)
        {
            fillEndDamage.gameObject.SetActive(true);
            fillEndDamage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(_fillDamage * 242, 0), 0.5f);
        }
        else
        {
            fillEndDamage.gameObject.SetActive(false);
        }

        if (_fillHealth > 0 && _fillHealth < 1)
        {
            fillEndHealth.gameObject.SetActive(true);
            fillEndHealth.GetComponent<RectTransform>().DOAnchorPos(new Vector2(_fillHealth * 242, 0), 0.5f);
        }
        else
        {
            fillEndHealth.gameObject.SetActive(false);
        }

        if (_fillKrit > 0 && _fillKrit < 1)
        {
            fillEndKrit.gameObject.SetActive(true);
            fillEndKrit.GetComponent<RectTransform>().DOAnchorPos(new Vector2(_fillKrit * 242, 0), 0.5f);
        }
        else
        {
            fillEndKrit.gameObject.SetActive(false);
        }

        if (_fillDodge > 0 && _fillDodge < 1)
        {
            fillEndDodge.gameObject.SetActive(true);
            fillEndDodge.GetComponent<RectTransform>().DOAnchorPos(new Vector2(_fillDodge * 242, 0), 0.5f);
        }
        else
        {
            fillEndDodge.gameObject.SetActive(false);
        }

        fillDamageMax.DOFillAmount((float)PlayerPrefs.GetFloat(_carName + "carDamageMax") / 300f, 0.5f);
        fillHealthMax.DOFillAmount((float)PlayerPrefs.GetFloat(_carName + "carHealthMax") / 3200f, 0.5f);
        fillKritMax.DOFillAmount((float)PlayerPrefs.GetFloat(_carName + "carKritChanceMax") / 25, 0.5f);
        fillDodgeMax.DOFillAmount((float)PlayerPrefs.GetFloat(_carName + "carDodgeMax") / 20, 0.5f);
        #endregion

        #region Устанавливаем уровень машины
        tCurrentLevel.text = PlayerPrefs.GetInt(_carName + "carLevel") + "/40";
        fillCarLevel.DOFillAmount((float)PlayerPrefs.GetInt(_carName + "carLevel") / 40, 0.5f);
        fillEndCarLevel.GetComponent<RectTransform>().DOAnchorPos(new Vector2((float)PlayerPrefs.GetInt(_carName + "carLevel") / 40 * 233f, 0), 0.5f);
        #endregion

        #region Open Characters
        for (int i = 0; i < 4; i++)
        {
            string s = "";
            int upgNum = 0;

            if (i == 0)
                upgNum = 10;
            if (i == 1)
                upgNum = 20;
            if (i == 2)
                upgNum = 30;
            if (i == 3)
                upgNum = 40;

            switch (PlayerPrefs.GetString(_carName + "carUpgrade" + upgNum + "lvlId"))
            {
                case "health":
                    s = "Здоровье";
                    break;

                case "shotSpeed":
                    s = "Скорость стрельбы";
                    break;

                case "kritChance":
                    s = "Шанс крита";
                    break;

                case "kritDamage":
                    s = "Урон крита";
                    break;

                case "vampirizm":
                    s = "Вампиризм";
                    break;

                case "backDamage":
                    s = "Обратка";
                    break;

                case "distanceDamage":
                    s = "Урон от расстояния";
                    break;

                case "dodge":
                    s = "Уклонение";
                    break;

                case "armor":
                    s = "Броня";
                    break;

                case "massEnemyDamage":
                    s = "Ахуевание";
                    break;

                case "headshot":
                    s = "Хэдшот";
                    break;

                case "screwValueUp":
                    s = "Множитель гаек";
                    break;

                case "lucky":
                    s = "Удача";
                    break;

                case "effectsDuration":
                    s = "Длительность эффектов";
                    break;
            }

            if (i == 0)
            {
                tUpgradeLvl10.text = "<color=yellow>ВСЕ МАШИНЫ</color> " + s + " +" + PlayerPrefs.GetFloat(_carName + "carUpgrade" + upgNum + "lvl");
            }

            if (i == 1)
            {
                tUpgradeLvl20.text = "<color=yellow>ВСЕ МАШИНЫ</color> " + s + " +" + PlayerPrefs.GetFloat(_carName + "carUpgrade" + upgNum + "lvl");
            }

            if (i == 2)
            {
                tUpgradeLvl30.text = "<color=yellow>ВСЕ МАШИНЫ</color> " + s + " +" + PlayerPrefs.GetFloat(_carName + "carUpgrade" + upgNum + "lvl");
            }

            if (i == 3)
            {
                tUpgradeLvl40.text = "<color=yellow>ВСЕ МАШИНЫ</color> " + s + " +" + PlayerPrefs.GetFloat(_carName + "carUpgrade" + upgNum + "lvl");
            }
        }

        if (PlayerPrefs.GetInt(_carName + "carLevel") < 10)
        {
            imgUpgrade10Lvl.sprite = sprUpgradeLvlNotUnlock;
            imgUpgrade20Lvl.sprite = sprUpgradeLvlNotUnlock;
            imgUpgrade30Lvl.sprite = sprUpgradeLvlNotUnlock;
            imgUpgrade40Lvl.sprite = sprUpgradeLvlNotUnlock;

            imgLockUpgrade10Lvl.gameObject.SetActive(true);
            imgLockUpgrade20Lvl.gameObject.SetActive(true);
            imgLockUpgrade30Lvl.gameObject.SetActive(true);
            imgLockUpgrade40Lvl.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt(_carName + "carLevel") < 20 && PlayerPrefs.GetInt(_carName + "carLevel") >= 10)
        {
            imgUpgrade10Lvl.sprite = sprUpgradeLvlUnlock;
            imgUpgrade20Lvl.sprite = sprUpgradeLvlNotUnlock;
            imgUpgrade30Lvl.sprite = sprUpgradeLvlNotUnlock;
            imgUpgrade40Lvl.sprite = sprUpgradeLvlNotUnlock;

            imgLockUpgrade10Lvl.gameObject.SetActive(false);
            imgLockUpgrade20Lvl.gameObject.SetActive(true);
            imgLockUpgrade30Lvl.gameObject.SetActive(true);
            imgLockUpgrade40Lvl.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt(_carName + "carLevel") < 30 && PlayerPrefs.GetInt(_carName + "carLevel") >= 20)
        {
            imgUpgrade10Lvl.sprite = sprUpgradeLvlUnlock;
            imgUpgrade20Lvl.sprite = sprUpgradeLvlUnlock;
            imgUpgrade30Lvl.sprite = sprUpgradeLvlNotUnlock;
            imgUpgrade40Lvl.sprite = sprUpgradeLvlNotUnlock;

            imgLockUpgrade10Lvl.gameObject.SetActive(false);
            imgLockUpgrade20Lvl.gameObject.SetActive(false);
            imgLockUpgrade30Lvl.gameObject.SetActive(true);
            imgLockUpgrade40Lvl.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt(_carName + "carLevel") < 40 && PlayerPrefs.GetInt(_carName + "carLevel") >= 30)
        {
            imgUpgrade10Lvl.sprite = sprUpgradeLvlUnlock;
            imgUpgrade20Lvl.sprite = sprUpgradeLvlUnlock;
            imgUpgrade30Lvl.sprite = sprUpgradeLvlUnlock;
            imgUpgrade40Lvl.sprite = sprUpgradeLvlNotUnlock;

            imgLockUpgrade10Lvl.gameObject.SetActive(false);
            imgLockUpgrade20Lvl.gameObject.SetActive(false);
            imgLockUpgrade30Lvl.gameObject.SetActive(false);
            imgLockUpgrade40Lvl.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt(_carName + "carLevel") == 40)
        {
            imgUpgrade10Lvl.sprite = sprUpgradeLvlUnlock;
            imgUpgrade20Lvl.sprite = sprUpgradeLvlUnlock;
            imgUpgrade30Lvl.sprite = sprUpgradeLvlUnlock;
            imgUpgrade40Lvl.sprite = sprUpgradeLvlUnlock;

            imgLockUpgrade10Lvl.gameObject.SetActive(false);
            imgLockUpgrade20Lvl.gameObject.SetActive(false);
            imgLockUpgrade30Lvl.gameObject.SetActive(false);
            imgLockUpgrade40Lvl.gameObject.SetActive(false);
        }
        #endregion

        tPrice.text = upgradePrice + "";

        if (PlayerPrefs.GetInt(_carName + "carLevel") >= 40)
        {
            butUpgrade.SetActive(false);
            butUpgradeGray.SetActive(true);
        } 
        else
        {
            butUpgrade.SetActive(true);
            butUpgradeGray.SetActive(false);
        }
    }

    public void ButUpgrade()
    {
        string _carName = carItem.carName;

        if (PlayerPrefs.GetInt("playerMoney") >= upgradePrice && PlayerPrefs.GetInt(_carName + "carLevel") < 40)
        {
            PlayerPrefs.SetInt("playerMoney", PlayerPrefs.GetInt("playerMoney") - (int)upgradePrice);
            
            PlayerPrefs.SetFloat(_carName + "carDamage", PlayerPrefs.GetFloat(_carName + "carDamage") + PlayerPrefs.GetFloat(_carName + "carDamageStepUp"));
            PlayerPrefs.SetFloat(_carName + "carHealth", PlayerPrefs.GetFloat(_carName + "carHealth") + PlayerPrefs.GetFloat(_carName + "carHealthStepUp"));
            PlayerPrefs.SetFloat(_carName + "carKritChance", PlayerPrefs.GetFloat(_carName + "carKritChance") + PlayerPrefs.GetFloat(_carName + "carKritChanceStepUp"));
            PlayerPrefs.SetFloat(_carName + "carDodge", PlayerPrefs.GetFloat(_carName + "carDodge") + PlayerPrefs.GetFloat(_carName + "carDodgeStepUp"));

            PlayerPrefs.SetInt(_carName + "carLevel", PlayerPrefs.GetInt(_carName + "carLevel") + 1);

            changeCarController.UpdateProgressBar();

            Initialize();
        }
    }
}
