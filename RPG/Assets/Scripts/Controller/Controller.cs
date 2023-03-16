using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Battle.Core;
using RPG.Battle.Move;
using RPG.Battle.Fight;
using RPG.Battle.Character;
using RPG.Battle.AI;
using System;

namespace RPG.Battle.Control
{
    public class Controller : MonoBehaviour
    {
        public StateContext stateContext;

        public IdelState idelState = new IdelState();
        public ChaseState chaseState = new ChaseState();
        public AttackState attackState = new AttackState();

        // Component
        public Animator animator;
        public Movement movement;
        public Attack attack;
        public Status status;

        private Controller target;
        // Encapsulation
        public Controller Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
            }
        }

        protected virtual void Awake()
        {
            stateContext = new StateContext(this);
        }

        protected virtual void Start()
        {
            stateContext.SetState(idelState);
        }

        private void OnEnable()
        {
            status.SetHpBar();
        }

        private void Update()
        {
            if (BattleManager.GetInstance().CurrentStats != BattleState.BATTLE) return;

            if (CheckTarget()) { SetChaseState(); }
            if (CheckMoveDistacne()) { SetAttackState(); }

            stateContext.Update();
        }

        private bool CheckMoveDistacne()
        {
            // 타겟된 적이 공격 범위 내에 있는가
            if (!movement.MoveDistanceResult(target.transform)) return true;

            return false;
        }

        private bool CheckTarget()
        {
            // 타겟된 적이 있는가?
            if (Target == null) return true;
            // 적이 죽어있는가?
            if (Target.status.IsDead) return true;

            return false;
        }

        protected virtual void SetAttackState()
        {
            stateContext.SetState(attackState);
        }

        protected virtual void SetChaseState()
        {
            stateContext.SetState(chaseState);
        }

        public virtual void DeadAction()
        {
            animator.SetTrigger("Dead");
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}