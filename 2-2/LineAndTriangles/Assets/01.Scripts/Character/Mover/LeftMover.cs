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

    // StartSelecter�� position, moveIndex, isLeft�� �̿��� �����̱� �����ϴ� �Լ�
    private void StartReset(Vector2 pos, int index, bool isLeft)
    {
        // SpriteRenderer Ȱ��ȭ
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
        // To Do : ��������Ʈ ����
    }

    // �������ʴ� ��ġ�� �̵���Ű�� Mover������ �浹�� ���� �Լ�
    private void EndReset()
    {
        transform.position = new Vector2(4, 4);
        OffCollision();
    }

}
