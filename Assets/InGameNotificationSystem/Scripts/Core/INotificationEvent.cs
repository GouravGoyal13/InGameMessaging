using UnityEngine;
using System;
using System.Collections;

namespace InGameNotification
{
	public enum MessagePriority
	{
		Low,
		Medium,
		High
	}

	public interface INotificationEvent
	{
        DateTime timeRaised { get; set; }
        float displayTime { get; set; }
        MessagePriority priority { get; set; }
        object message { get; set; }
        Action onTapAction { get; set; }
	}
}
