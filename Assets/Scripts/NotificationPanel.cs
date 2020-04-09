using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPanel : MonoBehaviour {

    GameObject notificationTemplate;

	void LoadTemplate()
    {
        notificationTemplate = Resources.Load<GameObject>("Notification");
    }
	
    public void ToggleNotifications()
    {
        if (gameObject.activeSelf)
            HideNotifications();
        else
            ShowNotifications();
    }

    void HideNotifications() {
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        gameObject.SetActive(false);
    }

    void ShowNotifications() {
        Debug.Log(NotificationHandler.instance.AmountOfNotifications + " eday nots");
        for(int i = 0; i < NotificationHandler.instance.AmountOfNotifications; i++)
        {
            Debug.Log("Heo " + i);
            AddNotification(NotificationHandler.instance.GetNotification(i));
        }
        gameObject.SetActive(true);
    }

    void AddNotification(string text) {
        if (notificationTemplate == null)
            LoadTemplate();
        GameObject notification = Instantiate(notificationTemplate, transform);
        notification.GetComponentInChildren<Text>().text = text;
    }

}
