using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;

public class FirebaseSetup : MonoBehaviour
{
    private FirebaseApp app;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);     
    }

    public void FirebaseInit()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = FirebaseApp.DefaultInstance;
                Debug.Log("Firebase Initialize Success");

                FirebaseAnalytics.SetUserProperty("language", PlayerPrefs.GetString("activeLang"));
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

        InitScene.initCount++;
    }

    #region Аналитика прогресса игрока
    //Done
    public void Event_CarUpgrade(int _upgradeLevel, string _name, string _resBalance, string _resType)
    {
        string s = "CarUpgrade_CarLevel_" + _upgradeLevel + "_Name_" + _name + "_" + _resBalance + "_" + _resType;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("PlayerProgress", s));

        Debug.Log("Event Send!\n" + s);
    }

    //Done
    public void Event_DetailApply(string _detailType, int _id, int _level, string _rare)
    {
        string s = "DetailApply_" + _detailType + "_ID_" + _id + "_Level_" + _level + "_" + _rare;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("PlayerProgress", s));

        Debug.Log("Event Send!\n" + s);
    }

    //Done
    public void Event_DetailTakeOff(string _detailType, int _id, int _level, string _rare)
    {
        string s = "DetailTakeOff_" + _detailType + "_ID_" + _id + "_Level_" + _level + "_" + _rare;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("PlayerProgress", s));

        Debug.Log("Event Send!\n" + s);
    }

    //Done
    public void Event_DetailUpgrade(string _invStatus, string _detailType, int _id, int _level, string _rare, string _resBalance, string _resType)
    {
        string s = "DetailTakeOff_" + _invStatus + "_" + _detailType + "_ID_" + _id + "_Level_" + _level + "_" + _rare + "_" + _resBalance + "_" + _resType;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("PlayerProgress", s));

        Debug.Log("Event Send!\n" + s);
    }

    //Done
    public void Event_Merge(string _detailType, int _id, int _level, string _rare)
    {
        string s = "Merge_" + _detailType + "_ID_" + _id + "_Level_" + _level + "_" + _rare;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("PlayerProgress", s));

        Debug.Log("Event Send!\n" + s);
    }

    //Done
    public void Event_TalentUpgrade(int _globalLevel, string _resBalance, string _resType)
    {
        string s = "TalentUpgrade_GlobalLevel_" + _globalLevel + "_" + _resBalance + "_" + _resType;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("PlayerProgress", s));

        Debug.Log("Event Send!\n" + s);
    }

    //Done
    public void Event_WaveReward(int _waveNum, int _locNum, int _moneyCount, int _expCount, int _drawingGunCount, int _drawingDetailCount, int _titanCount, int _itemCount, string _itemsID)
    {
        string s = "WaveReward_WaveNum_" + _waveNum + "_LocNum_" + _locNum + "_Money_" + _moneyCount + "_Exp_" + _expCount + "_DrawingGun_" + _drawingGunCount
                                             + "_DrawingDetail_" + _drawingDetailCount + "_Titan_" + _titanCount + "_ItemCount_" + _itemCount + "_ItemsId_" + _itemsID;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("PlayerProgress", s));

        Debug.Log("Event Send!\n" + s);
    }

    public void Event_DailyReward(int _dayValue)
    {
        string s = "DailyReward_Day_" + _dayValue;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("PlayerProgress", s));

        Debug.Log("Event Send!\n" + s);
    }
    #endregion

    #region Игрокая аналитика
    //Done
    public void Event_Play(int _fuelCount, float _damageValue, float _healthValue)
    {
        string s = "Play_FuelCount_" + _fuelCount + "_Damage_" + _damageValue + "_Health_" + _healthValue;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("Game", s));

        Debug.Log("Event Send!\n" + s);
    }

    //Done
    public void Event_WaveClear(int _waveNum, string _locNum, int _gunCount)
    {
        string s = "WaveClear_WaveNum_" + _waveNum + "_LocNum_" + _locNum + "_GunCount_" + _gunCount;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("Game", s));

        Debug.Log("Event Send!\n" + s);
    }

    //Done
    public void Event_TakePassiveCard(string _id, string _rare)
    {
        string s = "TakePassiveCard_ID_" + _id + "_" + _rare;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("Game", s));

        Debug.Log("Event Send!\n" + s);
    }

    //Done
    public void Event_TakeGunCard1(string _id, string _rare)
    {
        string s = "TakeGunCard1_ID_" + _id + "_" + _rare;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("Game", s));

        Debug.Log("Event Send!\n" + s);
    }

    //Done
    public void Event_TakeGunCard2(string _id, string _rare)
    {
        string s = "TakeGunCard2_ID_" + _id + "_" + _rare;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("Game", s));

        Debug.Log("Event Send!\n" + s);
    }

    //Done
    public void Event_Pause()
    {
        string s = "Pause";

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("Game", s));

        Debug.Log("Event Send!\n" + s);
    }

    //Done
    public void Event_Continue()
    {
        string s = "Continue";

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("Game", s));

        Debug.Log("Event Send!\n" + s);
    }

    //Done
    public void Event_GoToHub()
    {
        string s = "GoToHub";

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("Game", s));

        Debug.Log("Event Send!\n" + s);
    }

    //Done
    public void Event_TakeFirstAidKit(int _waveNum, string _locNum, string _hpBar)
    {
        string s = "TakeFirstAidKit_WaveNum_" + _waveNum + "_LocNum_" + _locNum + "_" + _hpBar;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("Game", s));

        Debug.Log("Event Send!\n" + s);
    }

    //Done
    public void Event_FirstAidKitDestroy(string _hpBar)
    {
        string s = "FirstAidKitDestroy_" + _hpBar;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("Game", s));

        Debug.Log("Event Send!\n" + s);
    }

    public void Event_PlayerDie(int _waveNum, string _locNum, int _seconds, int _dieNum)
    {
        string s = "PlayerDie_WaveNum_" + _waveNum + "_LocNum_" + _locNum + "_Seconds_" + _seconds + "_DieNum_" + _dieNum;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("Game", s));

        Debug.Log("Event Send!\n" + s);
    }

    public void Event_Recovery(int _waveNum, string _locNum)
    {
        string s = "Recovery_WaveNum_" + _waveNum + "_LocNum_" + _locNum;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("Game", s));

        Debug.Log("Event Send!\n" + s);
    }
    #endregion

    #region Монетизационная аналитика
    public void Event_OpenCase1(string _openType, string _rare, string _itemType)
    {
        string s = "OpenCase1_" + _openType + "_" + _rare + "_" + _itemType;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("Game", s));

        Debug.Log("Event Send!\n" + s);
    }

    public void Event_OpenCase2(string _openType, string _rare, string _itemType)
    {
        string s = "OpenCase2_" + _openType + "_" + _rare + "_" + _itemType;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("Game", s));

        Debug.Log("Event Send!\n" + s);
    }

    public void Event_BuyMoney(int _value, string _firstBuy, int _accountMoneyValue)
    {
        string s = "BuyMoney_Value" + _value + "_" + _firstBuy + "_AccountValue_" + _accountMoneyValue;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("Game", s));

        Debug.Log("Event Send!\n" + s);
    }

    public void Event_BuyHard(int _value, string _firstBuy, int _accountHardValue)
    {
        string s = "BuyHard_Value" + _value + "_" + _firstBuy + "_AccountValue_" + _accountHardValue;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("Game", s));

        Debug.Log("Event Send!\n" + s);
    }

    public void Event_BuyCar(string _name, string _priceType, float _price)
    {
        string s = "BuyCar_" + _name + "_" + _priceType + "_Price_" + _price;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("Game", s));

        Debug.Log("Event Send!\n" + s);
    }
    #endregion
}
