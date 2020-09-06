using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace InGameNotification
{
    public class NotificationManager : MonoBehaviour
    {
        [SerializeField] bool logToConsole = false;
        [SerializeField] bool prependDateTime = false;

        [Header("Message Prefab")]
        [SerializeField] GameObject notificationPrefab;
        [SerializeField] Transform notificationAnchor;

        private NotificationPopup notificationpopup;
        private Queue<INotificationEvent> _pendingQueue = new Queue<INotificationEvent>();
        private bool notificationActive;
        private RectTransform notificationRect;
        public static NotificationManager instance = null; 

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
          
        }

        void OnEnable()
        {
            if (_pendingQueue.Count > 0)
            {
                _pendingQueue.Clear();
            }

            Events.instance.AddListener<DisposeEvent>(OnMessageDispose);
        }

        void OnDisable()
        {
            Events.instance.RemoveListener<DisposeEvent>(OnMessageDispose);
        }

        private void Start()
        {
            StartCoroutine(DisplayNotification());
        }

        IEnumerator DisplayNotification()
        {
            if (!notificationActive)
            {
                if(_pendingQueue.Count>0)
                {
					INotificationEvent message = _pendingQueue.Peek();
                    if (notificationpopup == null)
                    {
                        GameObject notificationObject = (GameObject)Instantiate(notificationPrefab, notificationAnchor.position, Quaternion.identity) as GameObject;
                        notificationObject.transform.SetParent(notificationAnchor);
                        notificationpopup = notificationObject.GetComponent<NotificationPopup>();
                        notificationRect = notificationObject.GetComponent<RectTransform>();
                    }
                    if (notificationRect != null)
                    {
                        notificationRect.anchoredPosition = new Vector2(0, notificationRect.sizeDelta.y);
                    }
                    notificationpopup.SetContext(message);
                    notificationActive = true;
                }
            }
            yield return null;
            StartCoroutine(DisplayNotification());
        }

        void OnMessageDispose(DisposeEvent e)
        {
            Debug.Log("Dequeuing --Total Count"+ _pendingQueue.Count);
            if(_pendingQueue.Count>0)
            _pendingQueue.Dequeue();
            notificationActive = false;
        }

        public void SendNotification(INotificationEvent notification)
        {
            _pendingQueue.Enqueue(notification);

            Debug.Log(_pendingQueue.Count);

            if (logToConsole)
            {
                Debug.Log("Message Recieved [" + System.DateTime.Now + "]: " + notification.message.ToString());
            }
        }

        public void SendNotification(List<INotificationEvent> notifications)
        {
            foreach (INotificationEvent notification in notifications)
            {
                SendNotification(notification);
            }
        }
    }
}
