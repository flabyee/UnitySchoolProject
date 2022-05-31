using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EndSelecter : Selecter
{
    private void Start()
    {
        GameManager.Instance.startGame += StartReset;
        GameManager.Instance.endGame += EndReset;
    }
    
    // endSelecterSpawnTrList중에 랜덤으로 하나로 이동하고 SpriteRenderer 활성화하는 함수
    private void StartReset()
    {
        // spawnPoint 중에 랜덤으로 이동
        int randomIndex = Random.Range(0, GameManager.Instance.endSelecterSpawnTrList.Count);
        Vector2 randomPoint = GameManager.Instance.endSelecterSpawnTrList[randomIndex].position;
        
        SetPosition(randomPoint);


        // SpriteRenderer 활성화
        spriteRenderer.enabled = true;
    }

    // 위치를 안보이는 곳으로 이동시킨후 SpriteRenderer 비활성화
    private void EndReset()
    {
        transform.position = new Vector2(6, 2);

        spriteRenderer.enabled = false;
    }

    //public void RandomMove()
    //{
    //    // 랜덤 위치
    //    StartCoroutine(RandomMove(Random.Range(3f, 5f)));
    //}

    //private IEnumerator RandomMove(float moveTime)
    //{
    //    float t = 0;
    //    while (moveTime > t)
    //    {
    //        t += Time.deltaTime;
    //        OnLeftButtonDown();
    //        yield return null;
    //    }
    //    OnLeftButtonUp();


    //    // SpriteRenderer 활성화
    //    GetComponent<SpriteRenderer>().enabled = true;
    //    List<SpriteRenderer> list = GetComponentsInChildren<SpriteRenderer>().ToList();
    //    list.RemoveAt(0);

    //    for(int i = 0; i < list.Count; i++)
    //    {
    //        list[i].enabled = true;
    //    }
    //}
}
