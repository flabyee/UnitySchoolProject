using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefabsDemo : MonoBehaviour
{
    void Update()
    {
        const string SaveHpKey = "CurHp";   //이렇게도 가능

        if(Input.GetKeyDown(KeyCode.S))
        {
            PlayerPrefs.SetInt("CurHp", 100);
            print("현재 체력을 저장했다");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            print("현재 체력을 불러옴 : " + PlayerPrefs.GetInt("CurHp"));
        }
    }
}
