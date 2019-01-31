using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public static class EventManager{


    private static Dictionary<string, UnityEvent> m_MyEvents = new Dictionary<string, UnityEvent>();


    public static void AddListener(string a_EventName, UnityAction a_Action)
    {
        UnityEvent thisEvent = null;

        bool t_EventExists = m_MyEvents.TryGetValue(a_EventName,out thisEvent);

        if (!t_EventExists)
        {
            thisEvent = new UnityEvent();
            m_MyEvents.Add(a_EventName, thisEvent);
        }

        thisEvent.AddListener(a_Action);
    }

    public static void RemoveListener(string a_EventName, UnityAction a_Action)
    {
        UnityEvent thisEvent = null;

        bool t_EventExists = m_MyEvents.TryGetValue(a_EventName,out thisEvent);

        if (t_EventExists)
        {
            thisEvent.RemoveListener(a_Action);
        }
    }

    public static void TriggerEvent(string a_EventName)
    {
        UnityEvent thisEvent = null;

        bool t_EventExists = m_MyEvents.TryGetValue(a_EventName, out thisEvent);

        if (t_EventExists)
        {
            thisEvent.Invoke();
        }
        else
        {
            Debug.Log("EventManage tryed to trigger non existing event.");
        }
    }

}
