using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Transform _menuParent;

    // �������� ĵ���� �޴����� ����
    private Stack<Menu> _menuStack = new Stack<Menu>();

    // �̱���
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

    // MainMenu�� �����ϰ� ��� ��Ȱ��ȭ
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

    // ������ �����ִ� �޴��� ��Ȱ��ȭ�ϰ� �Ű������� ���� �޴��� Ȱ��ȭ + stack�� �߰�
    public void OpenMenu(Menu menuInstance)
    {
        if (menuInstance == null)
        {
            Debug.Log("�޴� �ν��Ͻ��� �������");
            return;
        }

        // �ٸ���(������ ������� ��)�� ������ �ٸ��͵� SetActive(false)
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

    // stack.pop���� ���� �����ִ� �޴��� ��Ȱ��ȭ�ϰ� �� ���� �޴��� ������(stack.peek) Ȱ��ȭ
    public void CloseMenu()
    {
        if (_menuStack.Count == 0)
        {
            Debug.Log("�޴� ���ÿ� �ƹ��͵� ���� ����");
            return;
        }


        // �������� ���� �޴��� ������ ����
        Menu topMenu = _menuStack.Pop();
        topMenu.gameObject.SetActive(false);

        if (_menuStack.Count > 0)
        {
            // �ٷ� �� ���� �޴��� ������ Ȱ��ȭ(���� No)
            Menu nextMenu = _menuStack.Peek();  // Pop�� ���ű��� ��, Peek�� �����⸸
            nextMenu.gameObject.SetActive(true);
        }
    }
}
