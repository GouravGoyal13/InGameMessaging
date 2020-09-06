using UnityEngine;
using System;
using System.Collections;

namespace InGameNotification
{
    public abstract class GameEvents
    {}

    public class NotificationEvent : GameEvents, INotificationEvent
	{
        public DateTime timeRaised{ get; set; }
        public float displayTime { get; set; }
        public MessagePriority priority { get ; set; }
        public object message { get; set; }
        public Action onTapAction { get; set; }
        public NotificationEvent( MessagePriority _prority, object _message, float _displayTime, DateTime _timeRaised,Action _onTapAction  = null){
            this.displayTime = _displayTime;
            this.priority = _prority;
            this.message = _message;
            this.timeRaised = _timeRaised;
            this.onTapAction = _onTapAction ;
        }
	}

    public class DisposeEvent : GameEvents
    {
        
    }
}