using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpNewItems : MonoBehaviour
{
    private PopUpController _popUpController;

    [Header("Gun")]
    public GameObject objTextGunReward;
    public GameObject objGunReward1, objGunReward2, objGunReward3;
    public TMP_Text tGunReward1Name, tGunReward2Name, tGunReward3Name;
    public Image imgIconGun1, imgIconGun2, imgIconGun3;
    
    public Sprite sprGun1012;
    public Sprite sprGun1006;
    public Sprite sprGun1013;
    public Sprite sprGun1015;
    public Sprite sprGun1007;
    public Sprite sprGun1008;
    public Sprite sprGun1003;
    public Sprite sprGun1005;
    public Sprite sprGun1011;

    [Header("Passive")]
    public Sprite sprPassiveArmor;
    public Sprite sprPassiveRage;
    public Sprite sprPassiveMassEnemyDamage;
    public Sprite sprPassiveLucky;
    public Sprite sprPassiveEffectsDuration;
    public Sprite sprPassiveBackDamage;
    public Sprite sprPassiveDodge;
    public GameObject objPassiveReward1, objPassiveReward2;
    public TMP_Text tPassiveReward1Name, tPassiveReward2Name;
    public Image imgIconPassive1, imgIconPassive2;

    public PopUpNewLevel popUpNewLevel;
    


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();

        CheckUnlock();
    }

    void Initialize()
    {
        objGunReward1.SetActive(false);
        objGunReward2.SetActive(false);
        objGunReward3.SetActive(false);
        objTextGunReward.SetActive(false);

        objPassiveReward1.SetActive(false);
        objPassiveReward2.SetActive(false);

        if (PlayerPrefs.GetInt("maxLocation") == 2)
        {
            #region Gun
            if (PlayerPrefs.GetInt("unlockGun1012") == 0)
            {
                objTextGunReward.SetActive(true);

                objGunReward1.SetActive(true);
                imgIconGun1.sprite = sprGun1012;
                tGunReward1Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_1012Name");

                PlayerPrefs.SetInt("unlockGun1012", 1);
            }

            if (PlayerPrefs.GetInt("unlockGun1006") == 0)
            {
                objTextGunReward.SetActive(true);

                objGunReward2.SetActive(true);
                imgIconGun2.sprite = sprGun1006;
                tGunReward2Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_1006Name");

                PlayerPrefs.SetInt("unlockGun1006", 1);
            }
            #endregion

            #region Passive
            if (PlayerPrefs.GetInt("unlockPassiveArmor") == 0)
            {
                objPassiveReward1.SetActive(true);
                imgIconPassive1.sprite = sprPassiveArmor;
                tPassiveReward1Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_upgCardNameArmor");

                PlayerPrefs.SetInt("unlockPassiveArmor", 1);
            }

            if (PlayerPrefs.GetInt("unlockPassiveRage") == 0)
            {
                objPassiveReward2.SetActive(true);
                imgIconPassive2.sprite = sprPassiveRage;
                tPassiveReward2Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_upgCardNameRage");

                PlayerPrefs.SetInt("unlockPassiveRage", 1);
            }
            #endregion
        }

        if (PlayerPrefs.GetInt("maxLocation") == 3)
        {
            #region Gun
            if (PlayerPrefs.GetInt("unlockGun1013") == 0)
            {
                objTextGunReward.SetActive(true);

                objGunReward1.SetActive(true);
                imgIconGun1.sprite = sprGun1013;
                tGunReward1Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_1013Name");

                PlayerPrefs.SetInt("unlockGun1013", 1);
            }

            if (PlayerPrefs.GetInt("unlockGun1015") == 0)
            {
                objTextGunReward.SetActive(true);

                objGunReward2.SetActive(true);
                imgIconGun2.sprite = sprGun1015;
                tGunReward2Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_1015Name");

                PlayerPrefs.SetInt("unlockGun1015", 1);
            }
            #endregion

            #region Passive
            if (PlayerPrefs.GetInt("unlockPassiveMassEnemyDamage") == 0)
            {
                objPassiveReward1.SetActive(true);
                imgIconPassive1.sprite = sprPassiveMassEnemyDamage;
                tPassiveReward1Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_upgCardNameMassEnemyDamage");

                PlayerPrefs.SetInt("unlockPassiveMassEnemyDamage", 1);
            }

            if (PlayerPrefs.GetInt("unlockPassiveLucky") == 0)
            {
                objPassiveReward2.SetActive(true);
                imgIconPassive2.sprite = sprPassiveLucky;
                tPassiveReward2Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_upgCardNameLucky");

                PlayerPrefs.SetInt("unlockPassiveLucky", 1);
            }
            #endregion
        }

        if (PlayerPrefs.GetInt("maxLocation") == 4)
        {
            #region Gun
            if (PlayerPrefs.GetInt("unlockGun1007") == 0)
            {
                objTextGunReward.SetActive(true);

                objGunReward1.SetActive(true);
                imgIconGun1.sprite = sprGun1007;
                tGunReward1Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_1007Name");

                PlayerPrefs.SetInt("unlockGun1007", 1);
            }

            if (PlayerPrefs.GetInt("unlockGun1008") == 0)
            {
                objTextGunReward.SetActive(true);

                objGunReward2.SetActive(true);
                imgIconGun2.sprite = sprGun1008;
                tGunReward2Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_1008Name");

                PlayerPrefs.SetInt("unlockGun1008", 1);
            }
            #endregion

            #region Passive
            if (PlayerPrefs.GetInt("unlockPassiveEffectsDuration") == 0)
            {
                objPassiveReward1.SetActive(true);
                imgIconPassive1.sprite = sprPassiveEffectsDuration;
                tPassiveReward1Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_upgCardNameEffectsDuration");

                PlayerPrefs.SetInt("unlockPassiveEffectsDuration", 1);
            }

            if (PlayerPrefs.GetInt("unlockPassiveBackDamage") == 0)
            {
                objPassiveReward2.SetActive(true);
                imgIconPassive2.sprite = sprPassiveBackDamage;
                tPassiveReward2Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_upgCardNameBackDamage");

                PlayerPrefs.SetInt("unlockPassiveBackDamage", 1);
            }
            #endregion
        }

        if (PlayerPrefs.GetInt("maxLocation") == 5)
        {
            #region Gun
            if (PlayerPrefs.GetInt("unlockGun1003") == 0)
            {
                objTextGunReward.SetActive(true);

                objGunReward1.SetActive(true);
                imgIconGun1.sprite = sprGun1003;
                tGunReward1Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_1003Name");

                PlayerPrefs.SetInt("unlockGun1003", 1);
            }

            if (PlayerPrefs.GetInt("unlockGun1005") == 0)
            {
                objTextGunReward.SetActive(true);

                objGunReward2.SetActive(true);
                imgIconGun2.sprite = sprGun1005;
                tGunReward2Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_1005Name");

                PlayerPrefs.SetInt("unlockGun1005", 1);
            }

            if (PlayerPrefs.GetInt("unlockGun1011") == 0)
            {
                objTextGunReward.SetActive(true);

                objGunReward3.SetActive(true);
                imgIconGun3.sprite = sprGun1011;
                tGunReward3Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_1011Name");

                PlayerPrefs.SetInt("unlockGun1011", 1);
            }
            #endregion

            #region Passive
            if (PlayerPrefs.GetInt("unlockPassiveDodge") == 0)
            {
                objPassiveReward1.SetActive(true);
                imgIconPassive1.sprite = sprPassiveDodge;
                tPassiveReward1Name.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_upgCardNameDodge");

                PlayerPrefs.SetInt("unlockPassiveDodge", 1);
            }
            #endregion
        }

        GameObject.Find("GameCloud").GetComponent<GameCloud>().SaveData();
    }

    public void CheckUnlock()
    {
        if (PlayerPrefs.GetString("unlockNewItems") == "true")
        {
            Initialize();
            ButOpen();
            PlayerPrefs.SetString("unlockNewItems", "false");
        }
        else
        {
            popUpNewLevel.CheckPlayerExp();
        }
    }

    public void ButOpen()
    {
        _popUpController.OpenPopUp();
    }

    public void ButClosed()
    {
        _popUpController.ClosedPopUp();
        popUpNewLevel.CheckPlayerExp();
    }
}
