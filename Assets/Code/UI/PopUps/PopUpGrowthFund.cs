using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpGrowthFund : MonoBehaviour
{
    private PopUpController _popUpController;

    public List<GameObject> blackPanelObj;
    int playerLevel;
    public List<int> needPlayerLevel;

    [Header ("Free Reward")]
    public List<string> freePassRewardName;
    public List<int> freePassRewardValue;
    public List<GameObject> freePassToggleObj;

    [Header("Rare Reward")]
    public List<string> rarePassRewardName;
    public List<int> rarePassRewardValue;
    public List<GameObject> rarePassBlockObj;
    public List<GameObject> rarePassToggleObj;
    public TMP_Text tRarePrice;

    [Header("Epic Reward")]
    public List<string> epicPassRewardName;
    public List<int> epicPassRewardValue;
    public List<GameObject> epicPassBlockObj;
    public List<GameObject> epicPassToggleObj;
    public TMP_Text tEpicPrice;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    public void ButOpen()
    {
        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_PopUpGrowthFundStatus("Open");

        _popUpController.OpenPopUp();

        Initialize();
    }

    public void ButClosed()
    {
        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_PopUpGrowthFundStatus("Closed");

        _popUpController.ClosedPopUp();
    }

    void Initialize()
    {
        playerLevel = PlayerPrefs.GetInt("playerLevel");
        bool rareBuyed;
        bool epicBuyed;

        if (PlayerPrefs.GetInt("rareGrowthFundBuyed") == 1)
        {
            rareBuyed = true;

            tRarePrice.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rare");
        }
        else
        {
            rareBuyed = false;

            string price = GameObject.Find("HubController").GetComponent<ShopController>().prices[8];
            tRarePrice.text = price;
        }

        if (PlayerPrefs.GetInt("epicGrowthFundBuyed") == 1)
        {
            epicBuyed = true;

            tEpicPrice.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_epic");
        }
        else
        {
            epicBuyed = false;

            string price = GameObject.Find("HubController").GetComponent<ShopController>().prices[9];
            tEpicPrice.text = price;
        }

        for (int i = 0; i < needPlayerLevel.Count; i++)
        {
            if (playerLevel >= needPlayerLevel[i])
            {
                blackPanelObj[i].SetActive(false);

                if (PlayerPrefs.GetInt("growFundOpenFree" + needPlayerLevel[i]) == 1)
                {
                    freePassToggleObj[i].SetActive(true);
                }
                else
                {
                    freePassToggleObj[i].SetActive(false);
                }

                if (rareBuyed)
                {
                    rarePassBlockObj[i].SetActive(false);

                    if (PlayerPrefs.GetInt("growFundOpenRare" + needPlayerLevel[i]) == 1)
                    {
                        rarePassToggleObj[i].SetActive(true);
                    }
                    else
                    {
                        rarePassToggleObj[i].SetActive(false);
                    }
                }
                else
                {
                    rarePassBlockObj[i].SetActive(true);
                }

                if (epicBuyed)
                {
                    epicPassBlockObj[i].SetActive(false);

                    if (PlayerPrefs.GetInt("growFundOpenEpic" + needPlayerLevel[i]) == 1)
                    {
                        epicPassToggleObj[i].SetActive(true);
                    }
                    else
                    {
                        epicPassToggleObj[i].SetActive(false);
                    }
                }
                else
                {
                    epicPassBlockObj[i].SetActive(true);
                }
            }
            else
            {
                blackPanelObj[i].SetActive(true);

                freePassToggleObj[i].SetActive(false);

                rarePassToggleObj[i].SetActive(false);
                rarePassBlockObj[i].SetActive(false);

                epicPassToggleObj[i].SetActive(false);
                epicPassBlockObj[i].SetActive(false);
            }            
        }
    }

    public void BuyRarePass()
    {
        if (PlayerPrefs.GetInt("rareGrowthFundBuyed") != 1)
            GameObject.Find("HubController").GetComponent<ShopController>().ButBuyRarePass();
    }

    public void BuyRarePassCallBack()
    {
        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_PopUpGrowthFundBuy("Rare");

        PlayerPrefs.SetInt("rareGrowthFundBuyed", 1);
        Initialize();
    }

    public void BuyEpicPass()
    {
        if (PlayerPrefs.GetInt("epicGrowthFundBuyed") != 1)
            GameObject.Find("HubController").GetComponent<ShopController>().ButBuyEpicPass();
    }

    public void BuyEpicPassCallBack()
    {
        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_PopUpGrowthFundBuy("Epic");

        PlayerPrefs.SetInt("epicGrowthFundBuyed", 1);
        Initialize();
    }

    public void ButCellFree(int num)
    {       
        if (needPlayerLevel[num - 1] <= playerLevel && PlayerPrefs.GetInt("growFundOpenFree" + needPlayerLevel[num - 1]) != 1)
        {
            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_PopUpGrowthFundTakeReward("Free", num);

            PlayerPrefs.SetInt("growFundOpenFree" + needPlayerLevel[num - 1], 1);

            if (freePassRewardName[num - 1] == "hard")
            {
                PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + freePassRewardValue[num - 1]);
            }

            if (freePassRewardName[num - 1] == "key1")
            {
                PlayerPrefs.SetInt("playerKey1", PlayerPrefs.GetInt("playerKey1") + freePassRewardValue[num - 1]);
            }

            if (freePassRewardName[num - 1] == "key2")
            {
                PlayerPrefs.SetInt("playerKey2", PlayerPrefs.GetInt("playerKey2") + freePassRewardValue[num - 1]);
            }

            Initialize();
        }
    }

    public void ButCellRare(int num)
    {
        if (PlayerPrefs.GetInt("rareGrowthFundBuyed") == 1)
        {
            if (needPlayerLevel[num - 1] <= playerLevel && PlayerPrefs.GetInt("growFundOpenRare" + needPlayerLevel[num - 1]) != 1)
            {
                GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_PopUpGrowthFundTakeReward("Rare", num);

                PlayerPrefs.SetInt("growFundOpenRare" + needPlayerLevel[num - 1], 1);

                if (rarePassRewardName[num - 1] == "hard")
                {
                    PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + rarePassRewardValue[num - 1]);
                }

                if (rarePassRewardName[num - 1] == "key1")
                {
                    PlayerPrefs.SetInt("playerKey1", PlayerPrefs.GetInt("playerKey1") + rarePassRewardValue[num - 1]);
                }

                if (rarePassRewardName[num - 1] == "key2")
                {
                    PlayerPrefs.SetInt("playerKey2", PlayerPrefs.GetInt("playerKey2") + rarePassRewardValue[num - 1]);
                }

                Initialize();
            }
        }   
    }

    public void ButCellEpic(int num)
    {
        if (PlayerPrefs.GetInt("epicGrowthFundBuyed") == 1)
        {
            if (needPlayerLevel[num - 1] <= playerLevel && PlayerPrefs.GetInt("growFundOpenEpic" + needPlayerLevel[num - 1]) != 1)
            {
                GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_PopUpGrowthFundTakeReward("Epic", num);

                PlayerPrefs.SetInt("growFundOpenEpic" + needPlayerLevel[num - 1], 1);

                if (epicPassRewardName[num - 1] == "hard")
                {
                    PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + epicPassRewardValue[num - 1]);
                }

                if (epicPassRewardName[num - 1] == "key1")
                {
                    PlayerPrefs.SetInt("playerKey1", PlayerPrefs.GetInt("playerKey1") + epicPassRewardValue[num - 1]);
                }

                if (epicPassRewardName[num - 1] == "key2")
                {
                    PlayerPrefs.SetInt("playerKey2", PlayerPrefs.GetInt("playerKey2") + epicPassRewardValue[num - 1]);
                }

                Initialize();
            }
        }
    }
}
