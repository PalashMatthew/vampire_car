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
            Name = "Name",
            Description = "Description",
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
                    Title = "Title",
                    Text = "Message",
                    FireTime = System.DateTime.Now.AddSeconds(5),
                    SmallIcon = "small_icon",
                    LargeIcon = "large_icon"
                };

                AndroidNotificationCenter.SendNotification(notification, "nameId");
            }

            if (_id == "ads_chest")
            {
                AndroidNotification notification = new AndroidNotification()
                {
                    Title = "Title",
                    Text = "Message",
                    FireTime = System.DateTime.Now.AddSeconds(5),
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
