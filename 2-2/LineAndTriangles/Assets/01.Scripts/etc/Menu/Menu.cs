using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu<T> : Menu where T : Menu<T>
{
    // �̱���
    private static T _instance;
    public static T Instance { get { return _instance; } }

    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = (T)this;
        }
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    // Open ��� ����, �ڱ��ڽ��� Open
    public static void Open()
    {
        if (MenuManager.Instance != null && Instance != null)
        {
            MenuManager.Instance.OpenMenu(Instance);
        }
    }
}

// OnBackPressed�� ��� ����, �����ϰ������ override (�� : ó��ȭ�鿡�� �ڷΰ��� ����� ��������)
public abstract class Menu : MonoBehaviour
{
    public virtual void OnBackPressed()
    {
        if (MenuManager.Instance != null)
        {
            MenuManager.Instance.CloseMenu();
        }
    }
}
