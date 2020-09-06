using System;
using System.Collections;
using System.Collections.Generic;
using InGameNotification;
using UnityEngine;
using UnityEngine.UI;

public enum Direction
{
    SLIDEIN,
    SLIDEOUT
}
public class NotificationPopup : MonoBehaviour
{
    [Header("Notification Context Items")]
    [SerializeField] Text message;
    [SerializeField] Image icon;

    [Space(10)]
    [SerializeField] AudioClip audioClip;
    [SerializeField] Animator animator;

    private INotificationEvent notificationEventData;
    private float timer = 0f;
    private Direction direction;
    private float showTime;

    public void SetContext(object context)
    {
        if (context != null)
        {
            if (context is INotificationEvent)
            {
                notificationEventData = (INotificationEvent)context;
                SetData();
            }
        }
    }

    void SetData()
    {
        //Playsound
        gameObject.SetActive(true);
        showTime = notificationEventData.displayTime;
        message.text = notificationEventData.message.ToString();
		StartCoroutine(ShowMessage(notificationEventData.displayTime));
    }

    IEnumerator ShowMessage(float delay)
    {
        if (animator != null)
        {
            animator.SetTrigger("In");
        }
        yield return new WaitForSeconds(delay);
        if (animator != null)
        {
            animator.SetTrigger("Out");
        }
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

	private void OnDisable()
	{
        Debug.Log("Destroying Object");
        if (animator != null)
        {
            animator.SetTrigger("Out");
        }
        Events.instance.Raise(new DisposeEvent());
	}

    public void OnNotificationTap()
    {
        Debug.Log("Closing Notification");
        if(notificationEventData!=null && notificationEventData.onTapAction!=null)
        {
            notificationEventData.onTapAction();
        }
        gameObject.SetActive(false);
    }

 //   private void Move(Direction direction, Action action=null)
	//{
 //       while(timer<=slideSpeed)
 //       {
	//		timer += Time.deltaTime;
 //           transform.position += Vector3.MoveTowards(transform.position, new Vector3(0, -60, 0), Time.deltaTime * slideSpeed);
 //       }
 //       if(action!=null)
 //       {
 //           action();
 //       }
	//}
}
