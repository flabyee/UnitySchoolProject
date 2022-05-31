using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selecter : CharacterMove
{
    protected bool onLeft = false;
    protected bool onRight = false;

    // 현재 LeftMove였는지 RightMove였는지에 따라 목적지 설정방식이 달라짐
    // 예시) 왼쪽으로 이동중일때 왼쪽으로 이동하면 목적지를 다시 설정해줄필요가 없음
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

    // 왼쪽 버튼을 꾹 눌러서 onLeft를 활성화하고 비활성화하는 함수
    public void OnLeftButtonDown()
    {
        // 방향을 바꿀 때에는 목적지 다시 설정
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

    // 왼쪽 버튼을 꾹 눌러서 onRight 활성화하고 비활성화하는 함수
    public void OnRightButtonDown()
    {
        // 방향을 바꿀 때에는 목적지 다시 설정
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

    // 가운데 버튼을 눌러서 Mover 소환하는 함수
    public void OnSetButtonClick()
    {
        GameManager.Instance.StartMove(transform.position, moveIndex, isLeft);
    }
}
