using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightMover : Mover
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
        if (canMove)
        {
            RightMove();
        }
    }

    // StartSelecter�� position, moveIndex, isLeft�� �̿��� �����̱� �����ϴ� �Լ�
    private void StartReset(Vector2 pos, int index, bool isLeft)
    {
        // SpriteRenderer Ȱ��ȭ
        spriteRenderer.enabled = true;

        if (isLeft)
        {
            if (index - 1 < 0)
            {
                moveIndex = GameManager.Instance.moveTrList.Count - 1;
            }
            else
            {
                moveIndex = index - 1;
            }
        }
        else
        {
            moveIndex = index;
        }

        SetPosition(pos);
        SetRotation(GameManager.Instance.moveTrList[moveIndex].position);

        StartMove();
    }

    private void StartCollision()
    {
        OnCollision();
        // To Do : ��������Ʈ ��ȭ
    }

    // �������ʴ� ��ġ�� �̵���Ű�� Mover������ �浹�� ���� �Լ�
    private void EndReset()
    {
        transform.position = new Vector2(6, 4);
        OffCollision();
    }
}
