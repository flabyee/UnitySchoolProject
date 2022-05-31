using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selecter : CharacterMove
{
    protected bool onLeft = false;
    protected bool onRight = false;

    // ���� LeftMove������ RightMove�������� ���� ������ ��������� �޶���
    // ����) �������� �̵����϶� �������� �̵��ϸ� �������� �ٽ� ���������ʿ䰡 ����
    protected bool isLeft = true;

    protected void Update()
    {
        if(onLeft)
        {
            LeftMove();
        }
        else if(onRight)
        {
            RightMove();
        }
    }

    // ���� ��ư�� �� ������ onLeft�� Ȱ��ȭ�ϰ� ��Ȱ��ȭ�ϴ� �Լ�
    public void OnLeftButtonDown()
    {
        // ������ �ٲ� ������ ������ �ٽ� ����
        if(!isLeft)
        {
            moveIndex = (++moveIndex) % GameManager.Instance.moveTrList.Count;
            Debug.Log("not is left");
        }


        onLeft = true;
        isLeft = true;
    }
    public void OnLeftButtonUp()
    {
        onLeft = false;
    }

    // ���� ��ư�� �� ������ onRight Ȱ��ȭ�ϰ� ��Ȱ��ȭ�ϴ� �Լ�
    public void OnRightButtonDown()
    {
        // ������ �ٲ� ������ ������ �ٽ� ����
        if (isLeft)
        {
            if (moveIndex - 1 < 0)
            {
                moveIndex = GameManager.Instance.moveTrList.Count - 1;
            }
            else
            {
                moveIndex = --moveIndex;
            }
        }

        onRight = true;
        isLeft = false;
    }
    public void OnRightButtonUp()
    {
        onRight = false;
    }

    // ��� ��ư�� ������ Mover ��ȯ�ϴ� �Լ�
    public void OnSetButtonClick()
    {
        GameManager.Instance.StartMove(transform.position, moveIndex, isLeft);
    }
}
