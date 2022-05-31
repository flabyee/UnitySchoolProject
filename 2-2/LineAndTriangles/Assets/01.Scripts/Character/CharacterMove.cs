using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMove : MonoBehaviour
{
    public float speed = 10f;
    public int moveIndex;

    public SpriteRenderer spriteRenderer;

    // 목적지에 도착하면 다음 목적지로 이동하는 함수 + 방향지정 (moveIndex를 더해서 목적지 설정)
    protected void LeftMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, GameManager.Instance.moveTrList[moveIndex].position, speed / 100f);

        if (transform.position == GameManager.Instance.moveTrList[moveIndex].position)
        {
            moveIndex = (++moveIndex) % GameManager.Instance.moveTrList.Count;

            SetRotation(GameManager.Instance.moveTrList[moveIndex].position);
        }
    }

    // 목적지에 도착하면 다음 목적지로 이동하는 함수 + 방향지정 (moveIndex를 빼서 목적지 설정)
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

    // 시작위치 지정 함수
    public void SetPosition(Vector2 pos)
    {
        transform.position = pos;
    }

    // 방향 지정
    public void SetRotation(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
