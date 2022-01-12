using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityGameObjectEvent : UnityEvent<GameObject> { }

public class EventListener : MonoBehaviour
{
    public EventSO gEvent;

    public UnityGameObjectEvent responseObj = new UnityGameObjectEvent();   // �������� �͵�

    private void OnEnable()
    {
        gEvent.Register(this);
    }

    private void OnDisable()
    {
        gEvent.UnResister(this);
    }

    // �뺸���� �ൿ�� �ֽ��ϰ� �ִ� ������"��"�� �� �ൿ�� ���������� ���� ������ ������ �����ָ� �ȴ�
    public void OnEventOccurs(GameObject obj = null)
    {
        responseObj.Invoke(obj);
    }
}
