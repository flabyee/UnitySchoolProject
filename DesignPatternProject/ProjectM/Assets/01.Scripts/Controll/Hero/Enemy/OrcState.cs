using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrcSpace
{
    public partial class OrcState
    {
        public enum eState  // 가질 수 있는 상태 나열
        {
            IDLE, MOVE, ATTACK
        };

        public enum eEvent  // 이벤트 나열
        {
            ENTER, UPDATE, EXIT
        };

        public eState stateName;

        protected eEvent curEvent;

        protected GameObject myObj;
        protected CONOrc myOrc;
        protected Animator myAnim;
        protected Transform playerTrm;  // 타겟팅 할 플레이어의 트랜스폼

        protected OrcState nextState;  // 다음 상태를 나타냄

        float detectDist = 10.0f;
        float detectAngle = 30.0f;
        float shootDist = 7.0f;

        public OrcState(GameObject obj, CONOrc orc,Animator anim, Transform targetTrm)
        {
            myObj = obj;
            myOrc = orc;
            anim = myAnim;
            playerTrm = targetTrm;

            // 최초 이벤트를 엔터로
            curEvent = eEvent.ENTER;
        }

        public virtual void Enter() { curEvent = eEvent.UPDATE; }
        public virtual void Update() { curEvent = eEvent.UPDATE; }
        public virtual void Exit() { curEvent = eEvent.EXIT; }

        public OrcState Process()
        {
            if (curEvent == eEvent.ENTER) Enter();
            if (curEvent == eEvent.UPDATE) Update();
            if (curEvent == eEvent.EXIT)
            {
                Exit();
                return nextState;
            }

            return this;
        }
    }

    public class Idle : OrcState
    {
        public Idle(GameObject obj, CONOrc orc, Animator anim, Transform targetTrm)
        : base(obj, orc, anim, targetTrm)
        {
            stateName = eState.IDLE;
        }

        public override void Enter()
        {
            //myAnim.SetTrigger("isIdle");
            base.Enter();
        }

        public override void Update()
        {
            if (Mathf.Abs(GameSceneClass.gMGGame.castleTrm.position.x - myObj.transform.position.x) > 1)
            {
                nextState = new Move(myObj, myOrc, myAnim, playerTrm);
                curEvent = eEvent.EXIT;
            }
        }

        public override void Exit()
        {
            //myAnim.ResetTrigger("isIdle");
            base.Exit();
        }
    }

    public class Move : OrcState
    {
        public Move(GameObject obj, CONOrc orc, Animator anim, Transform targetTrm)
        : base(obj, orc, anim, targetTrm)
        {
            stateName = eState.IDLE;
        }

        public override void Enter()
        {
            //myAnim.SetTrigger("isIdle");
            base.Enter();
        }

        public override void Update()
        {
            myObj.transform.position += new Vector3(myOrc.speed * -1f * Time.deltaTime, 0, 0);

            if(GameSceneClass.gMGGame.castleTrm != null)
            {
                if (Mathf.Abs(GameSceneClass.gMGGame.castleTrm.position.x - myObj.transform.position.x) <= 1)
                {
                    nextState = new Attack(myObj, myOrc, myAnim, playerTrm);
                    curEvent = eEvent.EXIT;
                }
            }
            else
            {
                nextState = new Idle(myObj, myOrc, myAnim, playerTrm);
                curEvent = eEvent.EXIT;
            }
            
        }

        public override void Exit()
        {
            //myAnim.ResetTrigger("isIdle");
            base.Exit();
        }
    }

    public class Attack : OrcState
    {
        private float attackCoolTime;
        private float lastAttackTime;

        public Attack(GameObject obj, CONOrc orc, Animator anim, Transform targetTrm)
          : base(obj, orc, anim, targetTrm)
        {
            stateName = eState.IDLE;
        }

        public override void Enter()
        {
            attackCoolTime = 1f;
            lastAttackTime = 0f;

            //myAnim.SetTrigger("isIdle");
            base.Enter();
        }

        public override void Update()
        {
            if(Time.time >= lastAttackTime)
            {
                // attack
                lastAttackTime = Time.time + attackCoolTime;
                GameSceneClass.gMGGame.OnCastleDamaged(10);
            }
        }

        public override void Exit()
        {
            //myAnim.ResetTrigger("isIdle");
            base.Exit();
        }
    }
}

