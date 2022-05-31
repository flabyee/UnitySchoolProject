using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointGizmos : MonoBehaviour
{
    public bool isMp = true;
    private void OnDrawGizmos()
    {
        // MovePoint�� ����������, SpawnPoint�� �Ķ������� ǥ��
        if(isMp)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.blue;
        }
        Gizmos.DrawSphere(transform.position, 0.25f);
    }
}
