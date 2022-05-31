using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSelecter : Selecter
{
    private void Start()
    {
        GameManager.Instance.startGame += StartReset;
        GameManager.Instance.startMove += EndReset;
    }

    // SpriteRenderer를 활성화하고 위치를 지정해주는 함수
    private void StartReset()
    {
        GetComponent<SpriteRenderer>().enabled = true;

        SetPosition(GameManager.Instance.moveTrList[1].position);
        moveIndex = 1;
    }

    // 위치를 안보이는 곳으로 옮기고 SpriteRenderer 비활성화 // 같은 Action쓰기 위해서?? 쓸모없는 매개변수를 가지고 있다
    private void EndReset(Vector2 pos, int index, bool isLeft)
    {
        transform.position = new Vector2(4, 2);

        GetComponent<SpriteRenderer>().enabled = false;
    }
}