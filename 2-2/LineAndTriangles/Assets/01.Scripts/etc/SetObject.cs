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

    // �� ���̻��̿� Item�� ��ġ�ϰ� ���ִ� �Լ�, Editor�̿��ؼ� �ν�����â���� ���� ����
    public void SetPosition()
    {
        if(startPos != null && endPos != null)
        {
            transform.position = Vector2.Lerp(startPos.position, endPos.position, distance);
        }
    }
}
