using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : Item
{
    public bool isUp = true;
    public float value = 0.9f;

    // isUp에 따라 Mover의 속도를 변화시키는 함수
    public override void Use(GameObject target)
    {
        Mover mover = target.GetComponent<Mover>();
        if (mover != null)
        {
            mover.speed += isUp ? value : -value;
        }
    }
}
