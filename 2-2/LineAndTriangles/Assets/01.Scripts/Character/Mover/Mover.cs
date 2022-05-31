using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : CharacterMove
{
    // �����ۿ� ���� �ӵ��� ���� ���Ŀ� �ٽ� �ʱ�ȭ���ֱ����� ���� ��
    private const float originSpeed = 3f;

    public BoxCollider2D boxCollider;
    protected bool canMove = false;

    // Mover������ �浹�� ����(Item���� �浹��) canMove�� Ȱ��ȭ
    protected void StartMove()
    {
        boxCollider.enabled = false;
        canMove = true;
    }

    // Mover������ �浹 Ȱ��ȭ �� ��Ȱ��ȭ
    protected void OnCollision()
    {
        boxCollider.enabled = true;
    }
    protected void OffCollision()
    {
        boxCollider.enabled = false;
    }

    // canMove�� Mover������ �浹�� ��Ȱ��ȭ�ϰ� speed�� ���������� �ʱ�ȭ
    protected void StopMove()
    {
        canMove = false;
        boxCollider.enabled = false;
        speed = originSpeed;
    }

    // Mover������ �浹
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("mover");
        if (collision.CompareTag("MOVER"))
        {
            GameManager.Instance.EndMove();
        }
    }
}
