using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Game Event", order = 52)]
public class EventSO : ScriptableObject
{
    // ���, ����, �߻�
    private List<EventListener> eventListenerList = new List<EventListener>();  // ���� �����ϴ� ���� �͵�

    public void Register(EventListener listener)
    {
        eventListenerList.Add(listener);
    }

    public void UnResister(EventListener listener)
    {
        eventListenerList.Remove(listener);
    }

    public void Occurred(GameObject obj = null)
    {
        for (int i = 0; i < eventListenerList.Count; i++)
        {
            eventListenerList[i].OnEventOccurs(obj);
        }
    }
}
