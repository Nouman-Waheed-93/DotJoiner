using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationHandler {

    public static NotificationHandler instance;

    public int notificationCacheSize;
    string[] notificationText;

    int amountOfNotifications;

    public int AmountOfNotifications
    {
        get
        {
            return amountOfNotifications;
        }
    }

    public NotificationHandler(int size)
    {
        instance = this;
        notificationCacheSize = size;
        notificationText = new string[notificationCacheSize];
    }
    
    void PopNotification()
    {
        for(int i = 0; i < amountOfNotifications - 1; i++)
        {
            notificationText[i] = notificationText[i + 1];
        }
    }

	public void PushNotification(string newNotification)
    {
        if (amountOfNotifications < notificationCacheSize)
        {
            notificationText[amountOfNotifications] = newNotification;
        }
        else
        {
            PopNotification();
            notificationText[amountOfNotifications] = newNotification;
        }
        amountOfNotifications = Mathf.Clamp(amountOfNotifications + 1, 0, notificationCacheSize -1);
        if (Settings.instance.NotificationOn)
            NotificationAlert();
    }

    public void NotificationAlert() {
        SoundHandler.instance.NotificationAlert();
    }

    public string GetNotification(int index)
    {
        return notificationText[index];   
    }
    
}
