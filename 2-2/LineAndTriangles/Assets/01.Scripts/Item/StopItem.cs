using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopItem : Item
{
    public float stopTime = 1f;
    private float originSpeed;

    // Mover�� �̵��� ��� ���ߴ� �Լ�
    public override void Use(GameObject target)
    {
        Mover mover = target.GetComponent<Mover>();
        if (mover != null)
        {
            originSpeed = mover.speed;
            mover.speed = 0;
            StartCoroutine(ResetSpeed(mover));
        }
    }

    private IEnumerator ResetSpeed(Mover mover)
    {
        yield return new WaitForSeconds(stopTime);
        mover.speed = originSpeed;
    }
}
