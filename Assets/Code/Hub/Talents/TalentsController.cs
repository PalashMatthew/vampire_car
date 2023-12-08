using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalentsController : MonoBehaviour
{
    public PopUpTalentsFinal popUpTalentFinal;

    [Header("Cell Block")]
    public GameObject cell1block;
    public GameObject cell2block;
    public GameObject cell3block;
    public GameObject cell4block;
    public GameObject cell5block;
    public GameObject cell6block;
    public GameObject cell7block;
    public GameObject cell8block;
    public GameObject cell9block;

    [Header("Cell Level")]
    public TMP_Text tCell1Level;
    public TMP_Text tCell2Level;
    public TMP_Text tCell3Level;
    public TMP_Text tCell4Level;
    public TMP_Text tCell5Level;
    public TMP_Text tCell6Level;
    public TMP_Text tCell7Level;
    public TMP_Text tCell8Level;
    public TMP_Text tCell9Level;

    [Header("Cell Level Object")]
    public GameObject tCell1LevelObj;
    public GameObject tCell2LevelObj;
    public GameObject tCell3LevelObj;
    public GameObject tCell4LevelObj;
    public GameObject tCell5LevelObj;
    public GameObject tCell6LevelObj;
    public GameObject tCell7LevelObj;
    public GameObject tCell8LevelObj;
    public GameObject tCell9LevelObj;

    [Header("Cell Icon")]
    public GameObject cell1Icon;
    public GameObject cell2Icon;
    public GameObject cell3Icon;
    public GameObject cell4Icon;
    public GameObject cell5Icon;
    public GameObject cell6Icon;
    public GameObject cell7Icon;
    public GameObject cell8Icon;
    public GameObject cell9Icon;

    [Header("Cell Name")]
    public TMP_Text tCell1Name;
    public TMP_Text tCell2Name;
    public TMP_Text tCell3Name;
    public TMP_Text tCell4Name;
    public TMP_Text tCell5Name;
    public TMP_Text tCell6Name;
    public TMP_Text tCell7Name;
    public TMP_Text tCell8Name;
    public TMP_Text tCell9Name;

    [Header("Cell Value")]
    public TMP_Text tCell1Value;
    public TMP_Text tCell2Value;
    public TMP_Text tCell3Value;
    public TMP_Text tCell4Value;
    public TMP_Text tCell5Value;
    public TMP_Text tCell6Value;
    public TMP_Text tCell7Value;
    public TMP_Text tCell8Value;
    public TMP_Text tCell9Value;

    public int damageLevel;
    public int healthLevel;
    public int ironLevel;
    public int recoveryHpInFirstAidKitLevel;
    public int shotSpeedLevel;
    public int blockLevel;
    public int equipmentImprovementLevel;
    public int carImprovementLevel;
    public int gunSlotLevel;

    public int damageLevelMax;
    public int healthLevelMax;
    public int ironLevelMax;
    public int recoveryHpInFirstAidKitLevelMax;
    public int shotSpeedLevelMax;
    public int blockLevelMax;
    public int equipmentImprovementLevelMax;
    public int carImprovementLevelMax;
    public int gunSlotLevelMax;

    public TMP_Text tPrice;
    private int price;

    public TMP_Text tTalentsLevel;

    private int currentMaxTalantLevel;


    private void OnEnable()
    {
        Initialize();
    }

    public void Initialize()
    {
        SaveCheck();

        if (PlayerPrefs.GetInt("playerLevel") < 3)
        {
            currentMaxTalantLevel = 4;
        }

        if (PlayerPrefs.GetInt("playerLevel") >= 3 && PlayerPrefs.GetInt("playerLevel") < 5)
        {
            currentMaxTalantLevel = 7;
        }

        if (PlayerPrefs.GetInt("playerLevel") >= 5 && PlayerPrefs.GetInt("playerLevel") < 8)
        {
            currentMaxTalantLevel = 12;
        }

        if (PlayerPrefs.GetInt("playerLevel") >= 8 && PlayerPrefs.GetInt("playerLevel") < 10)
        {
            currentMaxTalantLevel = 18;
        }

        if (PlayerPrefs.GetInt("playerLevel") >= 10)
        {
            currentMaxTalantLevel = 150;
        }

        #region Health
        healthLevel = PlayerPrefs.GetInt("talentHealthLevel");

        if (healthLevel == 0)
        {
            cell1block.SetActive(true);
            tCell1LevelObj.SetActive(false);
            cell1Icon.SetActive(false);
            tCell1Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_notStudied");
            tCell1Value.text = "";
        } 
        else
        {
            cell1block.SetActive(false);
            tCell1LevelObj.SetActive(true);
            cell1Icon.SetActive(true);
            tCell1Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameHealth");
            tCell1Value.text = PlayerPrefs.GetInt("talentHealthCurrentValue") + "%";

            if (healthLevel >= healthLevelMax)
            {
                tCell1Level.text = "Max";
            } 
            else
            {
                tCell1Level.text = "Lv " + healthLevel;
            }            
        }
        #endregion

        #region Damage
        damageLevel = PlayerPrefs.GetInt("talentDamageLevel");

        if (damageLevel == 0)
        {
            cell2block.SetActive(true);
            tCell2LevelObj.SetActive(false);
            cell2Icon.SetActive(false);
            tCell2Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_notStudied");
            tCell2Value.text = "";
        }
        else
        {
            cell2block.SetActive(false);
            tCell2LevelObj.SetActive(true);
            cell2Icon.SetActive(true);
            tCell2Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameDamage");
            tCell2Value.text = PlayerPrefs.GetInt("talentDamageCurrentValue") + "%";

            if (damageLevel >= damageLevelMax)
            {
                tCell2Level.text = "Max";
            }
            else
            {
                tCell2Level.text = "Lv " + damageLevel;
            }            
        }
        #endregion

        #region RecoveryHpInFirstAidKit
        recoveryHpInFirstAidKitLevel = PlayerPrefs.GetInt("talentRecoveryHpInFirstAidKitLevel");

        if (recoveryHpInFirstAidKitLevel == 0)
        {
            cell3block.SetActive(true);
            tCell3LevelObj.SetActive(false);
            cell3Icon.SetActive(false);
            tCell3Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_notStudied");
            tCell3Value.text = "";
        }
        else
        {
            cell3block.SetActive(false);
            tCell3LevelObj.SetActive(true);
            cell3Icon.SetActive(true);
            tCell3Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameFirstAidKit");
            tCell3Value.text = PlayerPrefs.GetInt("talentRecoveryHpInFirstAidKitCurrentValue") + "%";

            if (recoveryHpInFirstAidKitLevel >= recoveryHpInFirstAidKitLevelMax)
            {
                tCell3Level.text = "Max";
            }
            else
            {
                tCell3Level.text = "Lv " + recoveryHpInFirstAidKitLevel;
            }
        }
        #endregion

        #region Block
        blockLevel = PlayerPrefs.GetInt("talentBlockLevel");

        if (blockLevel == 0)
        {
            cell4block.SetActive(true);
            tCell4LevelObj.SetActive(false);
            cell4Icon.SetActive(false);
            tCell4Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_notStudied");
            tCell4Value.text = "";
        }
        else
        {
            cell4block.SetActive(false);
            tCell4LevelObj.SetActive(true);
            cell4Icon.SetActive(true);
            tCell4Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameBlock");
            tCell4Value.text = PlayerPrefs.GetInt("talentBlockCurrentValue") + "";

            if (blockLevel >= blockLevelMax)
            {
                tCell4Level.text = "Max";
            }
            else
            {
                tCell4Level.text = "Lv " + blockLevel;
            }
        }
        #endregion

        #region Iron
        ironLevel = PlayerPrefs.GetInt("talentIronLevel");

        if (ironLevel == 0)
        {
            cell5block.SetActive(true);
            tCell5LevelObj.SetActive(false);
            cell5Icon.SetActive(false);
            tCell5Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_notStudied");
            tCell5Value.text = "";
        }
        else
        {
            cell5block.SetActive(false);
            tCell5LevelObj.SetActive(true);
            cell5Icon.SetActive(true);
            tCell5Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameIron");
            tCell5Value.text = PlayerPrefs.GetInt("talentIronCurrentValue") + "";

            if (ironLevel >= ironLevelMax)
            {
                tCell5Level.text = "Max";
            }
            else
            {
                tCell5Level.text = "Lv " + ironLevel;
            }
        }
        #endregion

        #region ShotSpeed
        shotSpeedLevel = PlayerPrefs.GetInt("talentShotSpeedLevel");

        if (shotSpeedLevel == 0)
        {
            cell6block.SetActive(true);
            tCell6LevelObj.SetActive(false);
            cell6Icon.SetActive(false);
            tCell6Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_notStudied");
            tCell6Value.text = "";
        }
        else
        {
            cell6block.SetActive(false);
            tCell6LevelObj.SetActive(true);
            cell6Icon.SetActive(true);
            tCell6Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameShotSpeed");
            tCell6Value.text = PlayerPrefs.GetInt("talentShotSpeedCurrentValue") + "";

            if (shotSpeedLevel >= shotSpeedLevelMax)
            {
                tCell6Level.text = "Max";
            }
            else
            {
                tCell6Level.text = "Lv " + shotSpeedLevel;
            }
        }
        #endregion

        #region GunSlot
        gunSlotLevel = PlayerPrefs.GetInt("talentGunSlotLevel");

        if (gunSlotLevel == 0)
        {
            cell7block.SetActive(true);
            tCell7LevelObj.SetActive(false);
            cell7Icon.SetActive(false);
            tCell7Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_notStudied");
            tCell7Value.text = "";
        }
        else
        {
            cell7block.SetActive(false);
            tCell7LevelObj.SetActive(true);
            cell7Icon.SetActive(true);
            tCell7Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameGunSlot");
            tCell7Value.text = PlayerPrefs.GetInt("talentGunSlotCurrentValue") + "";

            if (gunSlotLevel >= gunSlotLevelMax)
            {
                tCell7Level.text = "Max";
            }
            else
            {
                tCell7Level.text = "Lv " + gunSlotLevel;
            }
        }
        #endregion

        #region EquipmentImprovement
        equipmentImprovementLevel = PlayerPrefs.GetInt("talentEquipmentImprovementLevel");

        if (equipmentImprovementLevel == 0)
        {
            cell8block.SetActive(true);
            tCell8LevelObj.SetActive(false);
            cell8Icon.SetActive(false);
            tCell8Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_notStudied");
            tCell8Value.text = "";
        }
        else
        {
            cell8block.SetActive(false);
            tCell8LevelObj.SetActive(true);
            cell8Icon.SetActive(true);
            tCell8Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameEquipments");
            tCell8Value.text = PlayerPrefs.GetInt("talentEquipmentImprovementCurrentValue") + "";

            if (equipmentImprovementLevel >= equipmentImprovementLevelMax)
            {
                tCell8Level.text = "Max";
            }
            else
            {
                tCell8Level.text = "Lv " + equipmentImprovementLevel;
            }
        }
        #endregion

        #region CarImprovement
        carImprovementLevel = PlayerPrefs.GetInt("talentCarImprovementLevel");

        if (carImprovementLevel == 0)
        {
            cell9block.SetActive(true);
            tCell9LevelObj.SetActive(false);
            cell9Icon.SetActive(false);
            tCell9Name.text = "Не изучено";
            tCell9Value.text = "";
        }
        else
        {
            cell9block.SetActive(false);
            tCell9LevelObj.SetActive(true);
            cell9Icon.SetActive(true);
            tCell9Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameCar");
            tCell9Value.text = PlayerPrefs.GetInt("talentCarImprovementCurrentValue") + "";

            if (carImprovementLevel >= carImprovementLevelMax)
            {
                tCell9Level.text = "Max";
            }
            else
            {
                tCell9Level.text = "Lv " + carImprovementLevel;
            }
        }
        #endregion

        price = PlayerPrefs.GetInt("talent" + PlayerPrefs.GetInt("talentGlobalLevel") + "price");
        tPrice.text = price + "";

        tTalentsLevel.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsGlobalLevel") + " - " + (PlayerPrefs.GetInt("talentGlobalLevel") - 1);
    }

    public void ButUpgrade()
    {
        if (PlayerPrefs.GetInt("playerMoney") >= price && PlayerPrefs.GetInt("talentGlobalLevel") <= currentMaxTalantLevel)
        {
            string talantName = PlayerPrefs.GetString("talent" + PlayerPrefs.GetInt("talentGlobalLevel") + "name");

            PlayerPrefs.SetInt("talent" + talantName + "CurrentValue", 
                               PlayerPrefs.GetInt("talent" + PlayerPrefs.GetInt("talentGlobalLevel") + "value"));

            PlayerPrefs.SetInt("talent" + talantName + "Level", 
                               PlayerPrefs.GetInt("talent" + talantName + "Level") + 1);

            PlayerPrefs.SetInt("talentGlobalLevel", PlayerPrefs.GetInt("talentGlobalLevel") + 1);

            PlayerPrefs.SetInt("playerMoney", PlayerPrefs.GetInt("playerMoney") - price);

            Initialize();            
            
            popUpTalentFinal.value = PlayerPrefs.GetInt("talent" + talantName + "CurrentValue");
            popUpTalentFinal.talentName = talantName;
            popUpTalentFinal.Open();

            #region Event
            string _resBalanceType;
            string _resType = "";

            if (PlayerPrefs.GetInt("playerMoney") >= price && PlayerPrefs.GetInt("talentGlobalLevel") <= currentMaxTalantLevel)
            {
                _resBalanceType = "NotEmptyRes";
                _resType = "none";
            }
            else
            {
                _resBalanceType = "EmptyRes";

                if (PlayerPrefs.GetInt("playerMoney") < price)
                {
                    _resType = "_Money";
                }

                if (PlayerPrefs.GetInt("talentGlobalLevel") > currentMaxTalantLevel)
                {
                    _resType = "_PlayerLevel";
                }
            }

            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_TalentUpgrade(PlayerPrefs.GetInt("talentGlobalLevel"), _resBalanceType, _resType);
            #endregion
        }
    }

    void SaveCheck()
    {
        if (!PlayerPrefs.HasKey("talentGlobalLevel"))
        {
            PlayerPrefs.SetInt("talentGlobalLevel", 1);
        }

        if (!PlayerPrefs.HasKey("talentDamageLevel"))
        {
            PlayerPrefs.SetInt("talentDamageLevel", 0);
        }
        damageLevel = PlayerPrefs.GetInt("talentDamageLevel");

        if (!PlayerPrefs.HasKey("talentHealthLevel"))
        {
            PlayerPrefs.SetInt("talentHealthLevel", 0);
        }
        healthLevel = PlayerPrefs.GetInt("talentHealthLevel");

        if (!PlayerPrefs.HasKey("talentIronLevel"))
        {
            PlayerPrefs.SetInt("talentIronLevel", 0);
        }
        ironLevel = PlayerPrefs.GetInt("talentIronLevel");

        if (!PlayerPrefs.HasKey("talentRecoveryHpInFirstAidKitLevel"))
        {
            PlayerPrefs.SetInt("talentRecoveryHpInFirstAidKitLevel", 0);
        }
        recoveryHpInFirstAidKitLevel = PlayerPrefs.GetInt("talentRecoveryHpInFirstAidKitLevel");

        if (!PlayerPrefs.HasKey("talentShotSpeedLevel"))
        {
            PlayerPrefs.SetInt("talentShotSpeedLevel", 0);
        }
        shotSpeedLevel = PlayerPrefs.GetInt("talentShotSpeedLevel");

        if (!PlayerPrefs.HasKey("talentBlockLevel"))
        {
            PlayerPrefs.SetInt("talentBlockLevel", 0);
        }
        blockLevel = PlayerPrefs.GetInt("talentBlockLevel");

        if (!PlayerPrefs.HasKey("talentEquipmentImprovementLevel"))
        {
            PlayerPrefs.SetInt("talentEquipmentImprovementLevel", 0);
        }
        equipmentImprovementLevel = PlayerPrefs.GetInt("talentEquipmentImprovementLevel");

        if (!PlayerPrefs.HasKey("talentCarImprovementLevel"))
        {
            PlayerPrefs.SetInt("talentCarImprovementLevel", 0);
        }
        carImprovementLevel = PlayerPrefs.GetInt("talentCarImprovementLevel");

        if (!PlayerPrefs.HasKey("talentGunSlotLevel"))
        {
            PlayerPrefs.SetInt("talentGunSlotLevel", 0);
        }
        gunSlotLevel = PlayerPrefs.GetInt("talentGunSlotLevel");
    }
}
