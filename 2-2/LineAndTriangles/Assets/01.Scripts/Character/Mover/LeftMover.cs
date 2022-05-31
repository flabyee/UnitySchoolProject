using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMover : Mover
{
    private void Start()
    {
        GameManager.Instance.startMove += StartReset;
        GameManager.Instance.startCollision += StartCollision;
        GameManager.Instance.endMove += StopMove;
        GameManager.Instance.endGame += EndReset;
    }

    void Update()
    {
        if(canMove)
        {
            LeftMove();
        }
    }

    // StartSelecter의 position, moveIndex, isLeft를 이용해 움직이기 시작하는 함수
    private void StartReset(Vector2 pos, int index, bool isLeft)
    {
        // SpriteRenderer 활성화
        spriteRenderer.enabled = true;

        if (isLeft)
        {
            moveIndex = index;

        }
        else
        {
            moveIndex = (index + 1) % GameManager.Instance.moveTrList.Count;
        }

        SetPosition(pos);
        SetRotation(GameManager.Instance.moveTrList[moveIndex].position);

        StartMove();
    }


    private void StartCollision()
    {
        OnCollision();
        // To Do : 스프라이트 변경
    }

    // 보이지않는 위치로 이동시키고 Mover끼리의 충돌을 끄는 함수
    private void EndReset()
    {
        transform.position = new Vector2(4, 4);
        OffCollision();
    }

}
