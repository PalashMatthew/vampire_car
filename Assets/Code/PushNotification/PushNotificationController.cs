using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class PushNotificationController : MonoBehaviour
{
    private void Awake()
    {
        AndroidNotificationChannel channel = new AndroidNotificationChannel()
        {
            Name = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_pushChannelName"),
            Description = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_pushChannelDesk"),
            Id = "nameId",
            Importance = Importance.High
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        ClosedNotification();
    }

    public void SendNotification(string _id, int _time)
    {
        if (PlayerPrefs.GetInt("pushSendAccess") == 1)
        {
            if (_id == "fuel_max")
            {
                AndroidNotification notification = new AndroidNotification()
                {
                    Title = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_pushFuelMaxTitle"),
                    Text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_pushFuelMaxText"),
                    FireTime = System.DateTime.Now.AddSeconds(_time),
                    SmallIcon = "small_icon",
                    LargeIcon = "large_icon"
                };

                AndroidNotificationCenter.SendNotification(notification, "nameId");
            }

            if (_id == "ads_chest")
            {
                AndroidNotification notification = new AndroidNotification()
                {
                    Title = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_pushAdsChestTitle"),
                    Text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_pushAdsChestText"),
                    FireTime = System.DateTime.Now.AddSeconds(_time),
                    SmallIcon = "small_icon",
                    LargeIcon = "large_icon"
                };

                AndroidNotificationCenter.SendNotification(notification, "nameId");
            }
        }        
    }

    public void ClosedNotification()
    {
        AndroidNotificationCenter.CancelAllScheduledNotifications();
    }

    private void OnApplicationPause(bool pause)
    {
        SendNotification("fuel_max", 18000);
    }
}
