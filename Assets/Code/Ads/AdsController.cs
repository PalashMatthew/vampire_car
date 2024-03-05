using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsController : MonoBehaviour, IAppodealInitializationListener, IRewardedVideoAdListener
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Initialize()
    {        
        Appodeal.setTesting(false);
        Appodeal.disableLocationPermissionCheck();
        Appodeal.muteVideosIfCallsMuted(true);

        int adTypes = Appodeal.REWARDED_VIDEO;
        string appKey = "e621171e5c33c5248863d4d89fb29f62b4f4a85f8f393995";
        Appodeal.initialize(appKey, adTypes, this);

        Appodeal.setRewardedVideoCallbacks(this);

        Appodeal.cache(Appodeal.REWARDED_VIDEO);

        InitScene.initCount++;
    }

    public void onInitializationFinished(List<string> errors)
    {
        
        
    }

    public void ShowAds(string placementName)
    {
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
        {
            if (GameObject.Find("Firebase") != null)
                GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_ShowAdsStart(placementName);

            PlayerPrefs.SetString("currentAdsPlacementName", placementName);
            Appodeal.show(Appodeal.REWARDED_VIDEO);
        }
        else if (placementName == "adsChest")
        {
            GameObject.Find("HubController").GetComponent<ShopController>().openChestAccess = true;
        }
    }

    #region Rewarded Video callback handlers

    //Called when rewarded video was loaded (precache flag shows if the loaded ad is precache).
    public void onRewardedVideoLoaded(bool isPrecache)
    {
        Debug.Log("RewardedVideo loaded");
    }

    // Called when rewarded video failed to load
    public void onRewardedVideoFailedToLoad()
    {
        Debug.Log("RewardedVideo failed to load");
    }

    // Called when rewarded video was loaded, but cannot be shown (internal network errors, placement settings, etc.)
    public void onRewardedVideoShowFailed()
    {
        Debug.Log("RewardedVideo show failed");
    }

    // Called when rewarded video is shown
    public void onRewardedVideoShown()
    {
        //string placementName = PlayerPrefs.GetString("currentAdsPlacementName");

        //if (GameObject.Find("Firebase") != null)
        //    GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_ShowAdsFinish(placementName);
    }

    // Called when reward video is clicked
    public void onRewardedVideoClicked()
    {
        Debug.Log("RewardedVideo clicked");
    }

    // Called when rewarded video is closed
    public void onRewardedVideoClosed(bool finished)
    {
        Debug.Log("RewardedVideo closed");
    }

    // Called when rewarded video is viewed until the end
    public void onRewardedVideoFinished(double amount, string name)
    {
        string placementName = PlayerPrefs.GetString("currentAdsPlacementName");

        Debug.Log(placementName);

        if (placementName == "fuel_5")
        {
            PlayerPrefs.SetInt("playerFuelCurrent", PlayerPrefs.GetInt("playerFuelCurrent") + 5);
            GameObject.Find("GameCloud").GetComponent<GameCloud>().SaveData();
        }
        else if (placementName == "recovery")
        {
            GameObject.Find("PopUp Recovery").GetComponent<PopUpRecovery>().ContinueAds_Reward();
        }
        else if (placementName == "moneyX2")
        {
            GameObject.Find("PopUp Win").GetComponent<PopUpWin>().Ads_Reward();
        }
        else if (placementName == "freeMoney")
        {
            PlayerPrefs.SetInt("playerMoney", PlayerPrefs.GetInt("playerMoney") + PlayerPrefs.GetInt("freeMoneyAdsValue"));
            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_BuyMoney(1200, "-", PlayerPrefs.GetInt("playerMoney"));
            GameObject.Find("GameCloud").GetComponent<GameCloud>().SaveData();
        }
        else if (placementName == "adsChest")
        {
            StartCoroutine(GiveChest());
        }
        else if (placementName == "gunReroll")
        {
            GameObject.Find("PopUp Upgrade").GetComponent<PopUpUpgrade>().CallBackRerollAds("gun");
        }
        else if (placementName == "passiveReroll")
        {
            GameObject.Find("PopUp Upgrade").GetComponent<PopUpUpgrade>().CallBackRerollAds("passive");
        }

        if (GameObject.Find("Firebase") != null)
            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_ShowAdsFinish(placementName);

        Appodeal.cache(Appodeal.REWARDED_VIDEO);
    }

    IEnumerator GiveChest()
    {
        yield return new WaitForSeconds(0.4f);

        GameObject.Find("HubController").GetComponent<ShopController>().GiveAdsChest();
    }

    //Called when rewarded video is expired and can not be shown
    public void onRewardedVideoExpired()
    {
        Debug.Log("RewardedVideo expired");
    }

    #endregion
}
