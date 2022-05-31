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
    
    // endSelecterSpawnTrList�߿� �������� �ϳ��� �̵��ϰ� SpriteRenderer Ȱ��ȭ�ϴ� �Լ�
    private void StartReset()
    {
        // spawnPoint �߿� �������� �̵�
        int randomIndex = Random.Range(0, GameManager.Instance.endSelecterSpawnTrList.Count);
        Vector2 randomPoint = GameManager.Instance.endSelecterSpawnTrList[randomIndex].position;
        
        SetPosition(randomPoint);


        // SpriteRenderer Ȱ��ȭ
        spriteRenderer.enabled = true;
    }

    // ��ġ�� �Ⱥ��̴� ������ �̵���Ų�� SpriteRenderer ��Ȱ��ȭ
    private void EndReset()
    {
        transform.position = new Vector2(6, 2);

        spriteRenderer.enabled = false;
    }

    //public void RandomMove()
    //{
    //    // ���� ��ġ
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


    //    // SpriteRenderer Ȱ��ȭ
    //    GetComponent<SpriteRenderer>().enabled = true;
    //    List<SpriteRenderer> list = GetComponentsInChildren<SpriteRenderer>().ToList();
    //    list.RemoveAt(0);

    //    for(int i = 0; i < list.Count; i++)
    //    {
    //        list[i].enabled = true;
    //    }
    //}
}
