using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMove : MonoBehaviour
{
    public float speed = 10f;
    public int moveIndex;

    public SpriteRenderer spriteRenderer;

    // �������� �����ϸ� ���� �������� �̵��ϴ� �Լ� + �������� (moveIndex�� ���ؼ� ������ ����)
    protected void LeftMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, GameManager.Instance.moveTrList[moveIndex].position, speed / 100f);

        if (transform.position == GameManager.Instance.moveTrList[moveIndex].position)
        {
            moveIndex = (++moveIndex) % GameManager.Instance.moveTrList.Count;

            SetRotation(GameManager.Instance.moveTrList[moveIndex].position);
        }
    }

    // �������� �����ϸ� ���� �������� �̵��ϴ� �Լ� + �������� (moveIndex�� ���� ������ ����)
    protected void RightMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, GameManager.Instance.moveTrList[moveIndex].position, speed / 100f);

        if (transform.position == GameManager.Instance.moveTrList[moveIndex].position)
        {
            if(moveIndex - 1 < 0)
            {
                moveIndex = GameManager.Instance.moveTrList.Count - 1;
            }
            else
            {
                moveIndex = (--moveIndex);
            }

            SetRotation(GameManager.Instance.moveTrList[moveIndex].position);
        }
    }

    // ������ġ ���� �Լ�
    public void SetPosition(Vector2 pos)
    {
        transform.position = pos;
    }

    // ���� ����
    public void SetRotation(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
