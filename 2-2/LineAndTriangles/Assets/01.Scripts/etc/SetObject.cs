using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class SetObject : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;

    [Range(0f, 1f)]
    public float distance = 0.5f;

    // 맵 사이사이에 Item을 배치하게 해주는 함수, Editor이용해서 인스펙터창에서 실행 가능
    public void SetPosition()
    {
        if(startPos != null && endPos != null)
        {
            transform.position = Vector2.Lerp(startPos.position, endPos.position, distance);
        }
    }
}
