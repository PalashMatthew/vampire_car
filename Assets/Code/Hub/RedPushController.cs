using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DetailCard;

public class RedPushController : MonoBehaviour
{
    public GameObject rewardRedPush;
    public GameObject talentsRedPush;
    public GameObject shopRedPush;
    public GameObject inventoryRedPush;

    private bool isTalentsRedPush;

    private ChooseLocationController locationController;

    private void Awake()
    {
        locationController = GetComponent<ChooseLocationController>();
    }

    private void Start()
    {
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f);        

        CheckRedPush();
    }

    public void CheckRedPush()
    {        
        if (CheckTalentsRedPush())
        {
            talentsRedPush.SetActive(true);
        }
        else
        {
            talentsRedPush.SetActive(false);
        }

        if (CheckShopRedPush())
        {
            shopRedPush.SetActive(true);
        }
        else
        {
            shopRedPush.SetActive(false);
        }

        if (CheckWaveRewardRedPush())
        {
            rewardRedPush.SetActive(true);
        }
        else
        {
            rewardRedPush.SetActive(false);
        }

        if (CheckGarageRedPush())
        {
            inventoryRedPush.SetActive(true);
        }
        else
        {
            inventoryRedPush.SetActive(false);
        }
    }

    bool CheckTalentsRedPush()
    {
        int price = PlayerPrefs.GetInt("talent" + PlayerPrefs.GetInt("talentGlobalLevel") + "price");

        int currentMaxTalantLevel = 0;

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

        if (PlayerPrefs.GetInt("playerMoney") >= price && PlayerPrefs.GetInt("talentGlobalLevel") <= currentMaxTalantLevel && PlayerPrefs.GetInt("talentGlobalLevel") < 100)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool CheckShopRedPush()
    {
        if (PlayerPrefs.GetInt("playerKey1") > 0 || PlayerPrefs.GetInt("playerKey2") > 0 || GetComponent<ShopController>().isChest1Ads)
        {
            return true;
        } else
        {
            return false;
        }
    }

    bool CheckWaveRewardRedPush()
    {
        for (int locNum = 1; locNum <= 10; locNum++)
        {
            for (int i = 1; i <= 5; i++)
            {
                if (PlayerPrefs.GetInt("loc" + locNum + "reward" + i + "Take") == 0)
                {
                    if (PlayerPrefs.GetInt("loc_" + locNum + "_maxWave") >= i * 2)
                    {
                        return true;
                    }
                }
            }
        }
        

        return false;
    }

    bool CheckGarageRedPush()
    {
        int cellCount;

        #region Gun
        cellCount = PlayerPrefs.GetInt("itemCountGun");

        for (int i = 0; i < cellCount; i++)
        {           
            if (PlayerPrefs.GetInt("itemGunNew" + i) == 1)
            {
                return true;
            }
        }
        #endregion

        #region Engine
        cellCount = PlayerPrefs.GetInt("itemCountEngine");

        for (int i = 0; i < cellCount; i++)
        {
            if (PlayerPrefs.GetInt("itemEngineNew" + i) == 1)
            {
                return true;
            }
        }
        #endregion

        #region Brakes
        cellCount = PlayerPrefs.GetInt("itemCountBrakes");

        for (int i = 0; i < cellCount; i++)
        {
            if (PlayerPrefs.GetInt("itemBrakesNew" + i) == 1)
            {
                return true;
            }
        }
        #endregion

        #region FuelSystem
        cellCount = PlayerPrefs.GetInt("itemCountFuelSystem");

        for (int i = 0; i < cellCount; i++)
        {
            if (PlayerPrefs.GetInt("itemFuelSystemNew" + i) == 1)
            {
                return true;
            }
        }
        #endregion

        #region Suspension
        cellCount = PlayerPrefs.GetInt("itemCountSuspension");

        for (int i = 0; i < cellCount; i++)
        {
            if (PlayerPrefs.GetInt("itemSuspensionNew" + i) == 1)
            {
                return true;
            }
        }
        #endregion

        #region Transmission
        cellCount = PlayerPrefs.GetInt("itemCountTransmission");

        for (int i = 0; i < cellCount; i++)
        {
            if (PlayerPrefs.GetInt("itemTransmissionNew" + i) == 1)
            {
                return true;
            }
        }
        #endregion

        return false;
    }
}
