using Firebase.Analytics;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLocationController : MonoBehaviour
{
    public int currentLocNum;
    public int maxLocNum;

    public List<GameObject> locationsObj;
    public Camera mainCamera;

    public GameObject butNext, butPrev;

    public List<string> levelName;

    public ASyncLoader loader;

    public TMP_Text tChapter;

    [Header("UI")]
    public GameObject butPlay;
    public GameObject tOpenPrevLoc;
    public GameObject imgLock;

    public TMP_Text tLocName;
    public TMP_Text tWaveClearCount;

    public TMP_Text tRewards;
    public GameObject objReward;

    public GameObject popUpBuyFuel;

    public Material sky1, sky2, sky3, sky4;


    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (PlayerPrefs.GetInt("maxLocation") == 0)
        {
            PlayerPrefs.SetInt("maxLocation", 1);
        }

        currentLocNum = PlayerPrefs.GetInt("maxLocation");

        butPlay.SetActive(true);
        tOpenPrevLoc.SetActive(false);
        imgLock.SetActive(false);

        for (int i = 1; i <= maxLocNum; i++)
        {
            if (currentLocNum == i)
                locationsObj[i - 1].SetActive(true);
            else
                locationsObj[i - 1].SetActive(false);
        }

        if (currentLocNum == 1)
        {
            butNext.SetActive(true);
            butPrev.SetActive(false);
        }

        if (currentLocNum == maxLocNum)
        {
            butNext.SetActive(false);
            butPrev.SetActive(true);
        }

        ChangeLocation();

        CameraColorSettings();
    }

    private void Update()
    {
        tChapter.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_play");
        //tRewards.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_reward") + "\n" + (PlayerPrefs.GetInt("loc_" + currentLocNum + "_maxWave") / 2) + "/5";
        tRewards.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_reward");
        tLocName.text = currentLocNum + ". " + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_location" + currentLocNum + "Name");
        tWaveClearCount.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_maxWaveClear") + " " + PlayerPrefs.GetInt("loc_" + currentLocNum + "_maxWave") + "/" + 10;
    }

    public void ChangeLocation()
    {
        string locOpen = "closed";

        for (int i = 1; i <= maxLocNum; i++)
        {
            if (currentLocNum == i)
                locationsObj[i - 1].SetActive(true);
            else
                locationsObj[i - 1].SetActive(false);
        }

        if (currentLocNum == 1)
        {
            butNext.SetActive(true);
            butPrev.SetActive(false);            
        }

        if (currentLocNum > 1 && currentLocNum < maxLocNum)
        {
            butNext.SetActive(true);
            butPrev.SetActive(true);
        }

        if (currentLocNum == maxLocNum)
        {
            butNext.SetActive(false);
            butPrev.SetActive(true);
        }

        if (PlayerPrefs.GetInt("maxLocation") < currentLocNum)
        {
            butPlay.SetActive(false);
            tOpenPrevLoc.SetActive(true);
            imgLock.SetActive(true);
            objReward.SetActive(false);
        } 
        else
        {
            butPlay.SetActive(true);
            tOpenPrevLoc.SetActive(false);
            imgLock.SetActive(false);
            objReward.SetActive(true);

            locOpen = "open";
        }

        tRewards.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_reward") + "\n" + (PlayerPrefs.GetInt("loc_" + currentLocNum + "_maxWave") / 2) + "/5";

        tLocName.text = currentLocNum + ". " + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_location" + currentLocNum + "Name");
        tWaveClearCount.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_maxWaveClear") + " " + PlayerPrefs.GetInt("loc_" + currentLocNum + "_maxWave") + "/" + 10;
        
        CameraColorSettings();

        GameObject.Find("HubController").GetComponent<RedPushController>().CheckRedPush();

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_ChangeLocation(currentLocNum, PlayerPrefs.GetInt("loc_" + currentLocNum + "_maxWave"), locOpen);        
    }

    public void CameraColorSettings()
    {
        #region Camera Color Settings
        if (currentLocNum == 1)
        {
            mainCamera.backgroundColor = new Color(0.2311321f, 1f, 0.9111943f);
            //RenderSettings.skybox = sky1;
        }

        if (currentLocNum == 2)
        {
            mainCamera.backgroundColor = new Color(0.2311321f, 1f, 0.9111943f);
           // RenderSettings.skybox = sky1;
        }

        if (currentLocNum == 3)
        {
            mainCamera.backgroundColor = new Color(0.8313726f, 0.7568628f, 0.5294118f);
            //RenderSettings.skybox = sky2;
        }

        if (currentLocNum == 4)
        {
            mainCamera.backgroundColor = new Color(0.2311321f, 1f, 0.9111943f);
            //RenderSettings.skybox = sky1;
        }

        if (currentLocNum == 5)
        {
            mainCamera.backgroundColor = new Color(0.1759968f, 0.3301887f, 0.3120947f);
            //RenderSettings.skybox = sky3;
        }

        if (currentLocNum == 6)
        {
            mainCamera.backgroundColor = new Color(0.2311321f, 1f, 0.9111943f);
            //RenderSettings.skybox = sky1;
        }

        if (currentLocNum == 7)
        {
            mainCamera.backgroundColor = new Color(0.2006621f, 0.3113208f, 0.02202741f);
            //RenderSettings.skybox = sky4;
        }

        if (currentLocNum == 8)
        {
            mainCamera.backgroundColor = new Color(0.2311321f, 1f, 0.9111943f);
            //RenderSettings.skybox = sky1;
        }

        if (currentLocNum == 9)
        {
            mainCamera.backgroundColor = new Color(0.2311321f, 1f, 0.9111943f);
            //RenderSettings.skybox = sky1;
        }

        if (currentLocNum == 10)
        {
            mainCamera.backgroundColor = new Color(0.2311321f, 1f, 0.9111943f);
            //RenderSettings.skybox = sky1;
        }
        #endregion
    }

    public void ButNextLocation()
    {
        currentLocNum++;
        ChangeLocation();
    }

    public void ButPrevLocation()
    {
        currentLocNum--;
        ChangeLocation();
    }

    public void ButPlay()
    {
        if (PlayerPrefs.GetInt("playerFuelCurrent") >= 5)
        {
            FirebaseAnalytics.SetUserProperty("locationNum", currentLocNum.ToString());

            PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
            PlayerPrefs.SetInt("playerFuelCurrent", PlayerPrefs.GetInt("playerFuelCurrent") - 5);

            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Play(PlayerPrefs.GetInt("playerFuelCurrent"), PlayerPrefs.GetFloat("CurrentAllDamage"), PlayerPrefs.GetFloat("CurrentAllHealth"), currentLocNum);

            loader.LoadLevel(levelName[currentLocNum - 1]);
        }
        else
        {
            popUpBuyFuel.GetComponent<PopUpBuyFuel>().ButOpen();
        }
    }

    public void MaxLocationUnlock()
    {
        PlayerPrefs.SetInt("maxLocation", 10);
    }

    public void ButTestPlay(string levelName)
    {
        loader.LoadLevel(levelName);
    }
}
