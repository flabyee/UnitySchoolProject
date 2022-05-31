using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu<T> : Menu where T : Menu<T>
{
    // 싱글톤
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

    // Open 모두 동일, 자기자신을 Open
    public static void Open()
    {
        if (MenuManager.Instance != null && Instance != null)
        {
            MenuManager.Instance.OpenMenu(Instance);
        }
    }
}

// OnBackPressed는 모두 동일, 수정하고싶으면 override (예 : 처음화면에서 뒤로가면 종료로 수정가능)
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
