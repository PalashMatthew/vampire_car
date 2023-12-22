using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpDailyReward : MonoBehaviour
{
    private PopUpController _popUpController;

    bool isRewardReady;

    public GameObject toggle1, toggle2, toggle3, toggle4, toggle5, toggle6, toggle7;
    public GameObject shadow1, shadow2, shadow3, shadow4, shadow5, shadow6, shadow7;
    public Image imgPanel1, imgPanel2, imgPanel3, imgPanel4, imgPanel5, imgPanel6, imgPanel7;
    public Sprite sprPanelDefault, sprPanelActive;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();

        Initialize();
    }

    void Initialize()
    {
        if (!PlayerPrefs.HasKey("DailyRewardCurrentDay"))
        {
            PlayerPrefs.SetInt("DailyRewardCurrentDay", 1);
        }

        switch (PlayerPrefs.GetInt("DailyRewardCurrentDay"))
        {
            case 1:
                toggle1.SetActive(false);
                toggle2.SetActive(false);
                toggle3.SetActive(false);
                toggle4.SetActive(false);
                toggle5.SetActive(false);
                toggle6.SetActive(false);
                toggle7.SetActive(false);

                shadow1.SetActive(false);
                shadow2.SetActive(false);
                shadow3.SetActive(false);
                shadow4.SetActive(false);
                shadow5.SetActive(false);
                shadow6.SetActive(false);
                shadow7.SetActive(false);

                imgPanel1.sprite = sprPanelActive;
                imgPanel2.sprite = sprPanelDefault;
                imgPanel3.sprite = sprPanelDefault;
                imgPanel4.sprite = sprPanelDefault;
                imgPanel5.sprite = sprPanelDefault;
                imgPanel6.sprite = sprPanelDefault;
                imgPanel7.sprite = sprPanelDefault;
                break;

            case 2:
                toggle1.SetActive(true);
                toggle2.SetActive(false);
                toggle3.SetActive(false);
                toggle4.SetActive(false);
                toggle5.SetActive(false);
                toggle6.SetActive(false);
                toggle7.SetActive(false);

                shadow1.SetActive(true);
                shadow2.SetActive(false);
                shadow3.SetActive(false);
                shadow4.SetActive(false);
                shadow5.SetActive(false);
                shadow6.SetActive(false);
                shadow7.SetActive(false);

                imgPanel1.sprite = sprPanelDefault;
                imgPanel2.sprite = sprPanelActive;
                imgPanel3.sprite = sprPanelDefault;
                imgPanel4.sprite = sprPanelDefault;
                imgPanel5.sprite = sprPanelDefault;
                imgPanel6.sprite = sprPanelDefault;
                imgPanel7.sprite = sprPanelDefault;
                break;

            case 3:
                toggle1.SetActive(true);
                toggle2.SetActive(true);
                toggle3.SetActive(false);
                toggle4.SetActive(false);
                toggle5.SetActive(false);
                toggle6.SetActive(false);
                toggle7.SetActive(false);

                shadow1.SetActive(true);
                shadow2.SetActive(true);
                shadow3.SetActive(false);
                shadow4.SetActive(false);
                shadow5.SetActive(false);
                shadow6.SetActive(false);
                shadow7.SetActive(false);

                imgPanel1.sprite = sprPanelDefault;
                imgPanel2.sprite = sprPanelDefault;
                imgPanel3.sprite = sprPanelActive;
                imgPanel4.sprite = sprPanelDefault;
                imgPanel5.sprite = sprPanelDefault;
                imgPanel6.sprite = sprPanelDefault;
                imgPanel7.sprite = sprPanelDefault;
                break;

            case 4:
                toggle1.SetActive(true);
                toggle2.SetActive(true);
                toggle3.SetActive(true);
                toggle4.SetActive(false);
                toggle5.SetActive(false);
                toggle6.SetActive(false);
                toggle7.SetActive(false);

                shadow1.SetActive(true);
                shadow2.SetActive(true);
                shadow3.SetActive(true);
                shadow4.SetActive(false);
                shadow5.SetActive(false);
                shadow6.SetActive(false);
                shadow7.SetActive(false);

                imgPanel1.sprite = sprPanelDefault;
                imgPanel2.sprite = sprPanelDefault;
                imgPanel3.sprite = sprPanelDefault;
                imgPanel4.sprite = sprPanelActive;
                imgPanel5.sprite = sprPanelDefault;
                imgPanel6.sprite = sprPanelDefault;
                imgPanel7.sprite = sprPanelDefault;
                break;

            case 5:
                toggle1.SetActive(true);
                toggle2.SetActive(true);
                toggle3.SetActive(true);
                toggle4.SetActive(true);
                toggle5.SetActive(false);
                toggle6.SetActive(false);
                toggle7.SetActive(false);

                shadow1.SetActive(true);
                shadow2.SetActive(true);
                shadow3.SetActive(true);
                shadow4.SetActive(true);
                shadow5.SetActive(false);
                shadow6.SetActive(false);
                shadow7.SetActive(false);

                imgPanel1.sprite = sprPanelDefault;
                imgPanel2.sprite = sprPanelDefault;
                imgPanel3.sprite = sprPanelDefault;
                imgPanel4.sprite = sprPanelDefault;
                imgPanel5.sprite = sprPanelActive;
                imgPanel6.sprite = sprPanelDefault;
                imgPanel7.sprite = sprPanelDefault;
                break;

            case 6:
                toggle1.SetActive(true);
                toggle2.SetActive(true);
                toggle3.SetActive(true);
                toggle4.SetActive(true);
                toggle5.SetActive(true);
                toggle6.SetActive(false);
                toggle7.SetActive(false);

                shadow1.SetActive(true);
                shadow2.SetActive(true);
                shadow3.SetActive(true);
                shadow4.SetActive(true);
                shadow5.SetActive(true);
                shadow6.SetActive(false);
                shadow7.SetActive(false);

                imgPanel1.sprite = sprPanelDefault;
                imgPanel2.sprite = sprPanelDefault;
                imgPanel3.sprite = sprPanelDefault;
                imgPanel4.sprite = sprPanelDefault;
                imgPanel5.sprite = sprPanelDefault;
                imgPanel6.sprite = sprPanelActive;
                imgPanel7.sprite = sprPanelDefault;
                break;

            case 7:
                toggle1.SetActive(true);
                toggle2.SetActive(true);
                toggle3.SetActive(true);
                toggle4.SetActive(true);
                toggle5.SetActive(true);
                toggle6.SetActive(true);
                toggle7.SetActive(false);

                shadow1.SetActive(true);
                shadow2.SetActive(true);
                shadow3.SetActive(true);
                shadow4.SetActive(true);
                shadow5.SetActive(true);
                shadow6.SetActive(true);
                shadow7.SetActive(false);

                imgPanel1.sprite = sprPanelDefault;
                imgPanel2.sprite = sprPanelDefault;
                imgPanel3.sprite = sprPanelDefault;
                imgPanel4.sprite = sprPanelDefault;
                imgPanel5.sprite = sprPanelDefault;
                imgPanel6.sprite = sprPanelDefault;
                imgPanel7.sprite = sprPanelActive;
                break;
        }

        if (PlayerPrefs.HasKey("OfflineTimeLast"))
        {
            int seconds = 0;

            DateTime ts;
            ts = DateTime.Parse(PlayerPrefs.GetString("OfflineTimeLast"));

            if (ts.Hour > 0)
            {
                seconds += ts.Hour * 60 * 60;
            }

            if (ts.Minute > 0)
            {
                seconds += ts.Minute * 60;
            }

            if (ts.Second > 0)
            {
                seconds += ts.Second;
            }

            PlayerPrefs.SetInt("DailyRewardTimerSaveTime", PlayerPrefs.GetInt("DailyRewardTimerSaveTime") + seconds);

            if (PlayerPrefs.GetInt("DailyRewardTimerSaveTime") > 86400)
            {
                isRewardReady = true;
                ButOpen();
            }
            else
            {
                StartCoroutine(DailyRewardTimer());
            }
        }
    }

    IEnumerator DailyRewardTimer()
    {
        yield return new WaitForSeconds(1);
        PlayerPrefs.SetInt("DailyRewardTimerSaveTime", PlayerPrefs.GetInt("DailyRewardTimerSaveTime") + 1);

        if (PlayerPrefs.GetInt("DailyRewardTimerSaveTime") < 86400)
        {
            StartCoroutine(DailyRewardTimer());
        }
    }

    public void But_Reward(int buttonNum)
    {
        if (isRewardReady)
        {
            int day = PlayerPrefs.GetInt("DailyRewardCurrentDay");

            if (GameObject.Find("Firebase") != null)
                GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_DailyReward(day);

            if (buttonNum == day)
            {
                if (day == 1)
                {
                    PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + 20);
                    PlayerPrefs.SetInt("playerFuelCurrent", PlayerPrefs.GetInt("playerFuelCurrent") + 10);
                }

                if (day == 2)
                {
                    PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + 100);
                    PlayerPrefs.SetInt("playerKey1", PlayerPrefs.GetInt("playerKey1") + 1);
                    PlayerPrefs.SetInt("drawingGunCount", PlayerPrefs.GetInt("drawingGunCount") + 5);
                    PlayerPrefs.SetInt("playerFuelCurrent", PlayerPrefs.GetInt("playerFuelCurrent") + 5);
                }

                if (day == 3)
                {
                    PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + 20);
                    PlayerPrefs.SetInt("drawingGunCount", PlayerPrefs.GetInt("drawingGunCount") + 5);
                    PlayerPrefs.SetInt("drawingEngineCount", PlayerPrefs.GetInt("drawingEngineCount") + 5);
                    PlayerPrefs.SetInt("playerTitan", PlayerPrefs.GetInt("playerTitan") + 10);
                }

                if (day == 4)
                {
                    PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + 50);
                    PlayerPrefs.SetInt("playerKey2", PlayerPrefs.GetInt("playerKey2") + 1);
                    PlayerPrefs.SetInt("drawingEngineCount", PlayerPrefs.GetInt("drawingEngineCount") + 5);
                    PlayerPrefs.SetInt("drawingFuelSystemCount", PlayerPrefs.GetInt("drawingFuelSystemCount") + 5);
                }

                if (day == 5)
                {
                    PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + 20);
                    PlayerPrefs.SetInt("drawingSuspensionCount", PlayerPrefs.GetInt("drawingSuspensionCount") + 5);
                    PlayerPrefs.SetInt("drawingTransmissionCount", PlayerPrefs.GetInt("drawingTransmissionCount") + 5);
                }

                if (day == 6)
                {
                    PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + 50);
                    PlayerPrefs.SetInt("playerKey2", PlayerPrefs.GetInt("playerKey2") + 1);
                    PlayerPrefs.SetInt("drawingGunCount", PlayerPrefs.GetInt("drawingGunCount") + 5);
                    PlayerPrefs.SetInt("playerTitan", PlayerPrefs.GetInt("playerTitan") + 10);
                }

                if (day == 7)
                {
                    PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + 100);
                    PlayerPrefs.SetInt("playerKey1", PlayerPrefs.GetInt("playerKey1") + 1);
                    PlayerPrefs.SetInt("playerKey2", PlayerPrefs.GetInt("playerKey2") + 1);
                    PlayerPrefs.SetInt("drawingGunCount", PlayerPrefs.GetInt("drawingGunCount") + 5);
                    PlayerPrefs.SetInt("drawingBrakesCount", PlayerPrefs.GetInt("drawingBrakesCount") + 5);
                    PlayerPrefs.SetInt("drawingEngineCount", PlayerPrefs.GetInt("drawingEngineCount") + 5);
                    PlayerPrefs.SetInt("drawingFuelSystemCount", PlayerPrefs.GetInt("drawingFuelSystemCount") + 5);
                    PlayerPrefs.SetInt("drawingSuspensionCount", PlayerPrefs.GetInt("drawingSuspensionCount") + 5);
                    PlayerPrefs.SetInt("drawingTransmissionCount", PlayerPrefs.GetInt("drawingTransmissionCount") + 5);
                }

                isRewardReady = false;
                PlayerPrefs.SetInt("DailyRewardTimerSaveTime", 0);
                PlayerPrefs.SetInt("DailyRewardCurrentDay", PlayerPrefs.GetInt("DailyRewardCurrentDay") + 1);

                if (PlayerPrefs.GetInt("DailyRewardCurrentDay") > 7)
                {
                    PlayerPrefs.SetInt("DailyRewardCurrentDay", 1);
                }

                ButClosed();
            }
        }
    }

    public void ButOpen()
    {
        _popUpController.OpenPopUp();
    }

    public void ButClosed()
    {
        _popUpController.ClosedPopUp();
    }
}
