using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : CharacterMove
{
    // 아이템에 의해 속도가 변한 이후에 다시 초기화해주기위한 원조 값
    private const float originSpeed = 3f;

    public BoxCollider2D boxCollider;
    protected bool canMove = false;

    // Mover끼리의 충돌을 막고(Item과는 충돌함) canMove를 활성화
    protected void StartMove()
    {
        boxCollider.enabled = false;
        canMove = true;
    }

    // Mover끼리의 충돌 활성화 및 비활성화
    protected void OnCollision()
    {
        boxCollider.enabled = true;
    }
    protected void OffCollision()
    {
        boxCollider.enabled = false;
    }

    // canMove와 Mover끼리의 충돌을 비활성화하고 speed를 원래값으로 초기화
    protected void StopMove()
    {
        canMove = false;
        boxCollider.enabled = false;
        speed = originSpeed;
    }

    // Mover끼리의 충돌
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("mover");
        if (collision.CompareTag("MOVER"))
        {
            GameManager.Instance.EndMove();
        }
    }
}
