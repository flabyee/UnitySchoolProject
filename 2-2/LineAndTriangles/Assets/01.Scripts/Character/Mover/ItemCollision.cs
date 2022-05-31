using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    // 아이템과의 충돌을 감지, Mover끼리의 충돌은 버튼을 누른 이후에 활성화됨
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();

        if (item != null)
        {
            item.Use(gameObject.transform.parent.gameObject);
        }
    }
}
