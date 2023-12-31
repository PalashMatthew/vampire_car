using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpWaveReward : MonoBehaviour
{
    private PopUpController _popUpController;
    public ChooseLocationController locationController;

    public GameObject toggle1, toggle2, toggle3, toggle4, toggle5;
    public GameObject shadow1, shadow2, shadow3, shadow4, shadow5;
    public Image imgPanel1, imgPanel2, imgPanel3, imgPanel4, imgPanel5;
    public Sprite sprPanelDefault, sprPanelActive;

    public GameObject panel2KeyReward;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();        
    }

    void Initialize()
    {
        int currentLoc = locationController.currentLocNum;

        if (currentLoc == 1)
        {
            panel2KeyReward.SetActive(false);
        } 
        else
        {
            panel2KeyReward.SetActive(true);
        }

        //Reward 1
        if (PlayerPrefs.GetInt("loc" + currentLoc + "reward" + 1 + "Take") == 0)
        {
            if (PlayerPrefs.GetInt("loc_" + currentLoc + "_maxWave") >= 2)
            {
                toggle1.SetActive(false);
                shadow1.SetActive(false);
                imgPanel1.sprite = sprPanelActive;
            }
            else
            {
                toggle1.SetActive(false);
                shadow1.SetActive(false);
                imgPanel1.sprite = sprPanelDefault;
            }
        }
        else
        {
            toggle1.SetActive(true);
            shadow1.SetActive(true);
            imgPanel1.sprite = sprPanelDefault;
        }

        //Reward 2
        if (PlayerPrefs.GetInt("loc" + currentLoc + "reward" + 2 + "Take") == 0)
        {
            if (PlayerPrefs.GetInt("loc_" + currentLoc + "_maxWave") >= 4)
            {
                toggle2.SetActive(false);
                shadow2.SetActive(false);
                imgPanel2.sprite = sprPanelActive;
            }
            else
            {
                toggle2.SetActive(false);
                shadow2.SetActive(false);
                imgPanel2.sprite = sprPanelDefault;
            }
        }
        else
        {
            toggle2.SetActive(true);
            shadow2.SetActive(true);
            imgPanel2.sprite = sprPanelDefault;
        }

        //Reward 3
        if (PlayerPrefs.GetInt("loc" + currentLoc + "reward" + 3 + "Take") == 0)
        {
            if (PlayerPrefs.GetInt("loc_" + currentLoc + "_maxWave") >= 6)
            {
                toggle3.SetActive(false);
                shadow3.SetActive(false);
                imgPanel3.sprite = sprPanelActive;
            }
            else
            {
                toggle3.SetActive(false);
                shadow3.SetActive(false);
                imgPanel3.sprite = sprPanelDefault;
            }
        }
        else
        {
            toggle3.SetActive(true);
            shadow3.SetActive(true);
            imgPanel3.sprite = sprPanelDefault;
        }

        //Reward 4
        if (PlayerPrefs.GetInt("loc" + currentLoc + "reward" + 4 + "Take") == 0)
        {
            if (PlayerPrefs.GetInt("loc_" + currentLoc + "_maxWave") >= 8)
            {
                toggle4.SetActive(false);
                shadow4.SetActive(false);
                imgPanel4.sprite = sprPanelActive;
            }
            else
            {
                toggle4.SetActive(false);
                shadow4.SetActive(false);
                imgPanel4.sprite = sprPanelDefault;
            }
        }
        else
        {
            toggle4.SetActive(true);
            shadow4.SetActive(true);
            imgPanel4.sprite = sprPanelDefault;
        }

        //Reward 5
        if (PlayerPrefs.GetInt("loc" + currentLoc + "reward" + 5 + "Take") == 0)
        {
            if (PlayerPrefs.GetInt("loc_" + currentLoc + "_maxWave") >= 10)
            {
                toggle5.SetActive(false);
                shadow5.SetActive(false);
                imgPanel5.sprite = sprPanelActive;
            }
            else
            {
                toggle5.SetActive(false);
                shadow5.SetActive(false);
                imgPanel5.sprite = sprPanelDefault;
            }
        }
        else
        {
            toggle5.SetActive(true);
            shadow5.SetActive(true);
            imgPanel5.sprite = sprPanelDefault;
        }
    }

    public void But_Reward(int rewardNum)
    {        
        if (PlayerPrefs.GetInt("loc" + locationController.currentLocNum + "reward" + rewardNum + "Take") == 0)
        {
            if (PlayerPrefs.GetInt("loc_" + locationController.currentLocNum + "_maxWave") >= rewardNum * 2)
            {
                if (locationController.currentLocNum == 1)
                {
                    switch (rewardNum)
                    {
                        case 1:
                            PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + 20);
                            PlayerPrefs.SetInt("playerFuelCurrent", PlayerPrefs.GetInt("playerFuelCurrent") + 5);
                            break;

                        case 2:
                            PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + 20);
                            PlayerPrefs.SetInt("playerFuelCurrent", PlayerPrefs.GetInt("playerFuelCurrent") + 5);
                            break;

                        case 3:
                            PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + 20);
                            PlayerPrefs.SetInt("playerFuelCurrent", PlayerPrefs.GetInt("playerFuelCurrent") + 5);
                            break;

                        case 4:
                            PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + 20);
                            PlayerPrefs.SetInt("playerFuelCurrent", PlayerPrefs.GetInt("playerFuelCurrent") + 5);
                            PlayerPrefs.SetInt("playerKey2", PlayerPrefs.GetInt("playerKey2") + 1);
                            break;

                        case 5:
                            PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + 20);
                            PlayerPrefs.SetInt("playerFuelCurrent", PlayerPrefs.GetInt("playerFuelCurrent") + 5);
                            break;
                    }
                }
                else
                {
                    switch (rewardNum)
                    {
                        case 1:
                            PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + 20);
                            PlayerPrefs.SetInt("playerFuelCurrent", PlayerPrefs.GetInt("playerFuelCurrent") + 5);
                            break;

                        case 2:
                            PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + 20);
                            PlayerPrefs.SetInt("playerFuelCurrent", PlayerPrefs.GetInt("playerFuelCurrent") + 5);
                            PlayerPrefs.SetInt("playerKey1", PlayerPrefs.GetInt("playerKey1") + 1);                            
                            break;

                        case 3:
                            PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + 20);
                            PlayerPrefs.SetInt("playerFuelCurrent", PlayerPrefs.GetInt("playerFuelCurrent") + 5);
                            break;

                        case 4:
                            PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + 20);
                            PlayerPrefs.SetInt("playerFuelCurrent", PlayerPrefs.GetInt("playerFuelCurrent") + 5);
                            PlayerPrefs.SetInt("playerKey2", PlayerPrefs.GetInt("playerKey2") + 1);
                            break;

                        case 5:
                            PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + 20);
                            PlayerPrefs.SetInt("playerFuelCurrent", PlayerPrefs.GetInt("playerFuelCurrent") + 5);
                            break;
                    }
                }

                PlayerPrefs.SetInt("loc" + locationController.currentLocNum + "reward" + rewardNum + "Take", 1);

                GameObject.Find("GameCloud").GetComponent<GameCloud>().SaveData();
                Initialize();
            }
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
