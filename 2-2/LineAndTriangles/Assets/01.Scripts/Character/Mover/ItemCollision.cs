using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    // �����۰��� �浹�� ����, Mover������ �浹�� ��ư�� ���� ���Ŀ� Ȱ��ȭ��
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();

        if (item != null)
        {
            item.Use(gameObject.transform.parent.gameObject);
        }
    }
}
