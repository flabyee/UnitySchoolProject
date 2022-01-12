using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OrcSpace;

public class CONOrc : CONCharacter
{
    OrcState curState;

    public int maxHp = 5;
    private int curHp;

    public float speed = 5f;

    public override void Start()
    {
        curState = new Idle(this.gameObject, this, null, this.transform);

        curHp = maxHp;
    }

    public override void Update()
    {
        curState = this.curState.Process();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            collision.gameObject.GetComponent<CONEntity>().SetActive(false);
            curHp--;
            if(curHp <= 0)
            {
                GameSceneClass.gMGGame.DestoryEnemy(this);
            }
        }
    }
}