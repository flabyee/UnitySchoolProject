using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : Item
{
    public bool isUp = true;
    public float value = 0.9f;

    // isUp�� ���� Mover�� �ӵ��� ��ȭ��Ű�� �Լ�
    public override void Use(GameObject target)
    {
        Mover mover = target.GetComponent<Mover>();
        if (mover != null)
        {
            mover.speed += isUp ? value : -value;
        }
    }
}
