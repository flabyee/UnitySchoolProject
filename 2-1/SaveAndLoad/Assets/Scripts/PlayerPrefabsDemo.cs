using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefabsDemo : MonoBehaviour
{
    void Update()
    {
        const string SaveHpKey = "CurHp";   //�̷��Ե� ����

        if(Input.GetKeyDown(KeyCode.S))
        {
            PlayerPrefs.SetInt("CurHp", 100);
            print("���� ü���� �����ߴ�");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            print("���� ü���� �ҷ��� : " + PlayerPrefs.GetInt("CurHp"));
        }
    }
}
