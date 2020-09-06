using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InGameNotification;

public class NotificationTest : MonoBehaviour
{
    private Dictionary<MessagePriority, string> priorityLookup = new Dictionary<MessagePriority, string>();

    void Start()
	{
        priorityLookup.Add(MessagePriority.Low, "Low");
        priorityLookup.Add(MessagePriority.Medium, "Medium");
        priorityLookup.Add(MessagePriority.High, "High");
        TestNotification();
    }

    public void TestNotification()
    {
        List<INotificationEvent> notificationEvents = new List<INotificationEvent>() {
            new NotificationEvent(MessagePriority.High, "Hello Notification1", 4, System.DateTime.Now, () => Debug.Log("Notification Tapped")),
            new NotificationEvent(MessagePriority.High, "Hello Notification2", 4, System.DateTime.Now.AddSeconds(10), () => Debug.Log("Notification Tapped")),
            //new NotificationEvent(MessagePriority.High, "Hello Notification3", 4, System.DateTime.Now.AddSeconds(10), () => Debug.Log("Notification Tapped")),
            //new NotificationEvent(MessagePriority.High, "Hello Notification4", 4, System.DateTime.Now.AddSeconds(20), () => Debug.Log("Notification Tapped"))
        };
        NotificationManager.instance.SendNotification(notificationEvents);
    }
}
