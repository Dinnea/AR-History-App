using NotificationSamples;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class AppNotificationManager : MonoBehaviour
{
    [SerializeField, Tooltip("Reference to the notification manager.")]
    public GameNotificationsManager manager;
    [SerializeField] public Text notificationScheduledText;

    private const string notifChannelID= "notification_channel_id";
    private const string notifChannelTitle = "You are near something interesting!";
    private const string notifChannelDescription = "You are near Twente Airport! Enter the app to explore it!";
    private const int displayAfterTime = 1;

    // notification icons
    private string smallIconName = "logo0";
    private string largeIconName = "logo1";

    private void Start()
    {
        InitializeNotifChannel();

        ScheduleNotificationForUnactivity();
        //
        DisplayPendingNotification();
    }


    private void ScheduleNotificationForUnactivity() 
    {
        manager.CancelAllNotifications();
        ScheduleNotificationForUnactivity(displayAfterTime);
    }
    private void ScheduleNotificationForUnactivity(int increment)
    {
        string title = notifChannelTitle;
        string description = notifChannelDescription;
        DateTime deliveryTime = DateTime.UtcNow.AddMinutes(increment);
        string channel = notifChannelID;

        SendNotification(title, description, deliveryTime, channelID: channel, smallIcon: smallIconName, largeIcon: largeIconName);

    }

    private void SendNotification(string title, string body, DateTime deliveryTime, int? badgeNumber = null, bool reschedule = false, string channelID = null, string smallIcon = null, string largeIcon = null)
    {
        IGameNotification notification = manager.CreateNotification();

        if (notification == null) return;

        notification.Title = title;
        notification.Body = body;
        notification.Group = !string.IsNullOrEmpty(channelID) ? channelID : notifChannelID;
        notification.DeliveryTime = deliveryTime;
        notification.SmallIcon = smallIcon;
        notification.LargeIcon = largeIcon;

        if (badgeNumber != null) notification.BadgeNumber = badgeNumber;

        PendingNotification pendingNotification = manager.ScheduleNotification(notification);
        pendingNotification.Reschedule = reschedule;
        Debug.Log($"Queued notification for unactivity with ID \"{notification.Id}\" at time {deliveryTime:dd.MM.yyyy HH:mm:ss}");

    }

    private void DisplayPendingNotification()
    {
        StringBuilder notifStringBuilder = new StringBuilder("Pending notifications at:");
        notifStringBuilder.AppendLine();

        for (int i = manager.PendingNotifications.Count - 1; i>= 0; i--)
        {
            PendingNotification qdNotif = manager.PendingNotifications[i];
            DateTime? time = qdNotif.Notification.DeliveryTime;

            if (time != null) 
            {
                notifStringBuilder.Append($"{time:dd.MM.yyyy HH:mm:ss}");
                notifStringBuilder.AppendLine();
            }
        }

        notificationScheduledText.text = notifStringBuilder.ToString();
    }

    private void InitializeNotifChannel()
    {
        var channel = new GameNotificationChannel(notifChannelID, notifChannelTitle, notifChannelDescription);
        manager.Initialize(channel);
    }
}
