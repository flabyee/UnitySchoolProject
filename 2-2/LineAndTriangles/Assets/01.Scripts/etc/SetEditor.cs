using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SetObject))]
public class SetEditor : Editor
{
    // 인스펙터창에서 SetPosition 함수를 사용하게 해줌
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SetObject itemMove = (SetObject)target;

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Set", GUILayout.Width(120), GUILayout.Height(30)))
        {
            itemMove.SetPosition();
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
}