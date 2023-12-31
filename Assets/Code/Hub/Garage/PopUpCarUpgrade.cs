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
    public CalculateCharacteristics calculateCharacteristics;

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
    public TMP_Text tTitan;
    public TMP_Text tPrice;
    public GameObject butUpgrade;
    public GameObject butMaxLevel;
    public GameObject butNeedLevel;
    public GameObject butUpgradeBlock;
    public TMP_Text tNeedLevel;
    public TMP_Text tNeedMoney;
    public List<int> titanCount;
    public List<int> upgradePrice;

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

        #region ��������� �������� ����
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

        fillDamageMax.DOFillAmount((float)PlayerPrefs.GetFloat(_carName + "carDamageMax") / 170, 0.5f);
        fillHealthMax.DOFillAmount((float)PlayerPrefs.GetFloat(_carName + "carHealthMax") / 300, 0.5f);
        fillKritMax.DOFillAmount((float)PlayerPrefs.GetFloat(_carName + "carKritChanceMax") / 7.2f, 0.5f);
        fillDodgeMax.DOFillAmount((float)PlayerPrefs.GetFloat(_carName + "carDodgeMax") / 28, 0.5f);
        #endregion

        #region ������������� ������� ������
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
                case "damage":
                    s = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_damage");
                    break;

                case "health":
                    s = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_health");
                    break;

                case "shotSpeed":
                    s = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_shotSpeed");
                    break;

                case "kritChance":
                    s = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_kritChance");
                    break;

                case "kritDamage":
                    s = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_kritDamage");
                    break;

                case "vampirizm":
                    s = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_vampirizm");
                    break;

                case "backDamage":
                    s = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_backDamage");
                    break;

                case "distanceDamage":
                    s = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_distanceDamage");
                    break;

                case "dodge":
                    s = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_dodge");
                    break;

                case "armor":
                    s = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_armor");
                    break;

                case "massEnemyDamage":
                    s = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_massEnemyDamage");
                    break;

                case "headshot":
                    s = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_headshot");
                    break;

                case "screwValueUp":
                    s = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_screwValueUp");
                    break;

                case "lucky":
                    s = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_lucky");
                    break;

                case "effectsDuration":
                    s = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_effectsDuration");
                    break;
            }

            if (i == 0)
            {
                if (PlayerPrefs.GetString(_carName + "carUpgrade" + upgNum + "lvlId") == "health")
                {
                    tUpgradeLvl10.text = "<color=yellow>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_allCars") + "</color> " + s + " +" + PlayerPrefs.GetFloat(_carName + "carUpgrade" + upgNum + "lvl");
                } 
                else
                {
                    tUpgradeLvl10.text = "<color=yellow>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_allCars") + "</color> " + s + " +" + PlayerPrefs.GetFloat(_carName + "carUpgrade" + upgNum + "lvl") + "%";
                }
                
            }

            if (i == 1)
            {
                if (PlayerPrefs.GetString(_carName + "carUpgrade" + upgNum + "lvlId") == "health")
                {
                    tUpgradeLvl20.text = "<color=yellow>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_allCars") + "</color> " + s + " +" + PlayerPrefs.GetFloat(_carName + "carUpgrade" + upgNum + "lvl");
                }
                else
                {
                    tUpgradeLvl20.text = "<color=yellow>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_allCars") + "</color> " + s + " +" + PlayerPrefs.GetFloat(_carName + "carUpgrade" + upgNum + "lvl") + "%";
                }                    
            }

            if (i == 2)
            {
                if (PlayerPrefs.GetString(_carName + "carUpgrade" + upgNum + "lvlId") == "health")
                {
                    tUpgradeLvl30.text = "<color=yellow>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_allCars") + "</color> " + s + " +" + PlayerPrefs.GetFloat(_carName + "carUpgrade" + upgNum + "lvl");
                }
                else
                {
                    tUpgradeLvl30.text = "<color=yellow>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_allCars") + "</color> " + s + " +" + PlayerPrefs.GetFloat(_carName + "carUpgrade" + upgNum + "lvl") + "%";
                }                    
            }

            if (i == 3)
            {
                if (PlayerPrefs.GetString(_carName + "carUpgrade" + upgNum + "lvlId") == "health")
                {
                    tUpgradeLvl40.text = "<color=yellow>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_allCars") + "</color> " + s + " +" + PlayerPrefs.GetFloat(_carName + "carUpgrade" + upgNum + "lvl");
                }
                else
                {
                    tUpgradeLvl40.text = "<color=yellow>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_allCars") + "</color> " + s + " +" + PlayerPrefs.GetFloat(_carName + "carUpgrade" + upgNum + "lvl") + "%";
                }                    
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

        tPrice.text = upgradePrice[PlayerPrefs.GetInt(_carName + "carLevel")] + "";

        if (PlayerPrefs.GetInt(_carName + "carLevel") >= 40 || 
            PlayerPrefs.GetInt("playerTitan") < titanCount[PlayerPrefs.GetInt(_carName + "carLevel")] ||
            PlayerPrefs.GetInt("playerLevel") == PlayerPrefs.GetInt(_carName + "carLevel") ||
            PlayerPrefs.GetInt("playerMoney") < upgradePrice[PlayerPrefs.GetInt(_carName + "carLevel")])
        {
            butUpgrade.SetActive(false);

            if (PlayerPrefs.GetInt(_carName + "carLevel") < 40)
            {           
                if (PlayerPrefs.GetInt("playerTitan") < titanCount[PlayerPrefs.GetInt(_carName + "carLevel")] ||
                    PlayerPrefs.GetInt("playerMoney") < upgradePrice[PlayerPrefs.GetInt(_carName + "carLevel")])
                {
                    butMaxLevel.SetActive(false);
                    butUpgradeBlock.SetActive(true);
                    tNeedMoney.text = upgradePrice[PlayerPrefs.GetInt(_carName + "carLevel")].ToString();
                    butNeedLevel.SetActive(false);
                }
                
                if (PlayerPrefs.GetInt("playerLevel") == PlayerPrefs.GetInt(_carName + "carLevel"))
                {
                    butMaxLevel.SetActive(false);
                    butUpgradeBlock.SetActive(false);
                    butNeedLevel.SetActive(true);
                    tNeedLevel.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_needLevel") + " " + (PlayerPrefs.GetInt(_carName + "carLevel") + 1);
                }
            }
            else
            {
                butMaxLevel.SetActive(true);
                butUpgradeBlock.SetActive(false);
                butNeedLevel.SetActive(false);
            }            
        } 
        else
        {
            butUpgrade.SetActive(true);
            butMaxLevel.SetActive(false);
            butUpgradeBlock.SetActive(false);
            butNeedLevel.SetActive(false);
        }

        tTitan.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_titan") + ": " + PlayerPrefs.GetInt("playerTitan") + "/" + titanCount[PlayerPrefs.GetInt(_carName + "carLevel")];

        calculateCharacteristics.GlobalCarCharacteristics();
    }

    public void ButUpgrade()
    {
        string _carName = carItem.carName;

        if (PlayerPrefs.GetInt("playerMoney") >= upgradePrice[PlayerPrefs.GetInt(_carName + "carLevel")] && PlayerPrefs.GetInt(_carName + "carLevel") < 40)
        {
            if (PlayerPrefs.GetInt("playerTitan") >= titanCount[PlayerPrefs.GetInt(_carName + "carLevel")] && PlayerPrefs.GetInt(_carName + "carLevel") < PlayerPrefs.GetInt("playerLevel"))
            {
                PlayerPrefs.SetInt("playerMoney", PlayerPrefs.GetInt("playerMoney") - upgradePrice[PlayerPrefs.GetInt(_carName + "carLevel")]);

                PlayerPrefs.SetFloat(_carName + "carDamage", PlayerPrefs.GetFloat(_carName + "carDamage") + PlayerPrefs.GetFloat(_carName + "carDamageStepUp"));
                PlayerPrefs.SetFloat(_carName + "carHealth", PlayerPrefs.GetFloat(_carName + "carHealth") + PlayerPrefs.GetFloat(_carName + "carHealthStepUp"));
                PlayerPrefs.SetFloat(_carName + "carKritChance", PlayerPrefs.GetFloat(_carName + "carKritChance") + PlayerPrefs.GetFloat(_carName + "carKritChanceStepUp"));
                PlayerPrefs.SetFloat(_carName + "carDodge", PlayerPrefs.GetFloat(_carName + "carDodge") + PlayerPrefs.GetFloat(_carName + "carDodgeStepUp"));              

                PlayerPrefs.SetInt("playerTitan", PlayerPrefs.GetInt("playerTitan") - titanCount[PlayerPrefs.GetInt(_carName + "carLevel")]);

                PlayerPrefs.SetInt(_carName + "carLevel", PlayerPrefs.GetInt(_carName + "carLevel") + 1);

                changeCarController.UpdateProgressBar();

                //��� ������� ��������� ���������� upgradePrice
                carItem.Initialize();

                #region Events
                string resBalans = "";
                string resType = "";

                if (PlayerPrefs.GetInt("playerMoney") < upgradePrice[PlayerPrefs.GetInt(_carName + "carLevel")] || PlayerPrefs.GetInt("playerTitan") < titanCount[PlayerPrefs.GetInt(_carName + "carLevel")] || PlayerPrefs.GetInt(_carName + "carLevel") < PlayerPrefs.GetInt("playerLevel"))
                {
                    resBalans = "EmptyRes";
                } 
                else
                {
                    resBalans = "NotEmptyRes";
                }

                if (PlayerPrefs.GetInt("playerMoney") >= upgradePrice[PlayerPrefs.GetInt(_carName + "carLevel")] && PlayerPrefs.GetInt("playerTitan") >= titanCount[PlayerPrefs.GetInt(_carName + "carLevel")] && PlayerPrefs.GetInt(_carName + "carLevel") >= PlayerPrefs.GetInt("playerLevel"))
                {
                    resType = "none";
                }
                else
                {
                    if (PlayerPrefs.GetInt("playerMoney") < upgradePrice[PlayerPrefs.GetInt(_carName + "carLevel")])
                    {
                        resType += "_Money_";
                    }

                    if (PlayerPrefs.GetInt("playerTitan") < titanCount[PlayerPrefs.GetInt(_carName + "carLevel")])
                    {
                        resType += "_Titan_";
                    }

                    if (PlayerPrefs.GetInt("playerLevel") < PlayerPrefs.GetInt(_carName + "carLevel"))
                    {
                        resType += "_PlayerLevel_";
                    }
                }               

                GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_CarUpgrade(PlayerPrefs.GetInt(_carName + "carLevel"), _carName, resBalans, resType);
                #endregion

                Initialize();

                GameObject.Find("GameCloud").GetComponent<GameCloud>().SaveData();
            }            
        }
    }
}
