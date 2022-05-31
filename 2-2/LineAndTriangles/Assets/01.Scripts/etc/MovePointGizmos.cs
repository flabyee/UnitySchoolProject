using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointGizmos : MonoBehaviour
{
    public bool isMp = true;
    private void OnDrawGizmos()
    {
        // MovePoint는 빨간색으로, SpawnPoint는 파란색으로 표시
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
