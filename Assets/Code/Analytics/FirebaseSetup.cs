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
        FirebaseInit();        
    }

    void FirebaseInit()
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
    }

    #region Аналитика прогресса игрока
    public void Event_CarUpgrade(int _upgradeLevel, string _name, string _resBalance, string _resType)
    {
        string s = "CarUpgrade_CarLevel_" + _upgradeLevel + "_Name_" + _name + "_" + _resBalance + "_" + _resType;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("PlayerProgress", s));

        Debug.Log("Event Send!\n" + s);
    }

    public void Event_DetailApply(string _detailType, int _id, int _level, string _rare)
    {
        string s = "DetailApply_" + _detailType + "_ID_" + _id + "_Level_" + _level + "_" + _rare;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("PlayerProgress", s));

        Debug.Log("Event Send!\n" + s);
    }

    public void Event_DetailTakeOff(string _detailType, int _id, int _level, string _rare)
    {
        string s = "DetailTakeOff_" + _detailType + "_ID_" + _id + "_Level_" + _level + "_" + _rare;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("PlayerProgress", s));

        Debug.Log("Event Send!\n" + s);
    }

    public void Event_DetailUpgrade(string _invStatus, string _detailType, int _id, int _level, string _rare, string _resBalance, string _resType)
    {
        string s = "DetailTakeOff_" + _invStatus + "_" + _detailType + "_ID_" + _id + "_Level_" + _level + "_" + _rare + "_" + _resBalance + "_" + _resType;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("PlayerProgress", s));

        Debug.Log("Event Send!\n" + s);
    }

    public void Event_Merge(string _detailType, int _id, int _level, string _rare)
    {
        string s = "Merge_" + _detailType + "_ID_" + _id + "_Level_" + _level + "_" + _rare;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("PlayerProgress", s));

        Debug.Log("Event Send!\n" + s);
    }

    public void Event_TalentUpgrade(int _globalLevel, string _resBalance, string _resType)
    {
        string s = "TalentUpgrade_GlobalLevel_" + _globalLevel + "_" + _resBalance + "_" + _resType;

        FirebaseAnalytics.LogEvent("Splash_Games",
            new Parameter("PlayerProgress", s));

        Debug.Log("Event Send!\n" + s);
    }

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
}
