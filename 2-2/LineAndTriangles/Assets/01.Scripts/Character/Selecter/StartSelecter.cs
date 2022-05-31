using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSelecter : Selecter
{
    private void Start()
    {
        GameManager.Instance.startGame += StartReset;
        GameManager.Instance.startMove += EndReset;
    }

    // SpriteRenderer�� Ȱ��ȭ�ϰ� ��ġ�� �������ִ� �Լ�
    private void StartReset()
    {
        GetComponent<SpriteRenderer>().enabled = true;

        SetPosition(GameManager.Instance.moveTrList[1].position);
        moveIndex = 1;
    }

    // ��ġ�� �Ⱥ��̴� ������ �ű�� SpriteRenderer ��Ȱ��ȭ // ���� Action���� ���ؼ�?? ������� �Ű������� ������ �ִ�
    private void EndReset(Vector2 pos, int index, bool isLeft)
    {
        transform.position = new Vector2(4, 2);

        GetComponent<SpriteRenderer>().enabled = false;
    }
}