using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRootGame : MonoBehaviour
{
    [SerializeField]
    private Image testImage;

    [SerializeField]
    private Slider hpBar;

    void Awake()
    {
        GameSceneClass.gUiRootGame = this;
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            List<string> keyList = new List<string>(Global.spritesDic.Keys);
            int randomIdx = Random.Range(0, keyList.Count - 1);
            
            testImage.sprite = Global.spritesDic[keyList[randomIdx]];
            testImage.SetNativeSize();
        }
    }

    public void StageStart()
    {
        GameSceneClass.gMGGame.StageStart();
    }

    public void SetHpBar(float n)
    {
        hpBar.value = n;
    }

    public void TestFunc()
    {
        print("call UIRootGame");
    }
}
