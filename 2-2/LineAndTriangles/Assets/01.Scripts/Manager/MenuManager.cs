using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Transform _menuParent;

    // 스택으로 캔버스 메뉴들을 관리
    private Stack<Menu> _menuStack = new Stack<Menu>();

    // 싱글톤
    private static MenuManager _instance;
    public static MenuManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            InitMenu();
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void OnEnable()
    {
        InitMenu();
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    // MainMenu를 제외하고 모두 비활성화
    private void InitMenu()
    {
        Menu[] menus = _menuParent.GetComponentsInChildren<Menu>();
        
        foreach(Menu menu in menus)
        {
            if(!(menu is MainMenu))
            {
                menu.gameObject.SetActive(false);
            }
            else
            {
                OpenMenu(menu);
            }
        }
    }

    // 이전에 켜져있던 메뉴를 비활성화하고 매개변수로 받은 메뉴를 활성화 + stack에 추가
    public void OpenMenu(Menu menuInstance)
    {
        if (menuInstance == null)
        {
            Debug.Log("메뉴 인스턴스가 존재안함");
            return;
        }

        // 다른게(이전에 열어놓은 것)이 있으면 다른것들 SetActive(false)
        if (_menuStack.Count > 0)
        {
            foreach (Menu menu in _menuStack)
            {
                menu.gameObject.SetActive(false);
            }
        }

        menuInstance.gameObject.SetActive(true);

        _menuStack.Push(menuInstance);
    }

    // stack.pop으로 현재 켜져있는 메뉴를 비활성화하고 그 다음 메뉴를 꺼내서(stack.peek) 활성화
    public void CloseMenu()
    {
        if (_menuStack.Count == 0)
        {
            Debug.Log("메뉴 스택에 아무것도 존재 안함");
            return;
        }


        // 마지막에 열린 메뉴를 꺼내고 제거
        Menu topMenu = _menuStack.Pop();
        topMenu.gameObject.SetActive(false);

        if (_menuStack.Count > 0)
        {
            // 바로 그 다음 메뉴를 꺼내서 활성화(제거 No)
            Menu nextMenu = _menuStack.Peek();  // Pop은 제거까지 함, Peek은 꺼내기만
            nextMenu.gameObject.SetActive(true);
        }
    }
}
