using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using RPG.Battle.Core;
using RPG.Battle.Move;
using RPG.Battle.Fight;
using RPG.Battle.AI;
using RPG.Character.Status;

namespace RPG.Battle.Control
{
    public class Controller : MonoBehaviour
    {
        public StateContext stateContext;

        public IdelState idelState;
        public ChaseState chaseState;
        public AttackState attackState;

        // Component
        public Animator animator;
        public Status status;

        // Behaviour
        public Movement movement;
        public Attack attack;

        // Battle
        public Controller target;

        protected virtual void Awake()
        {
            movement = new Movement(transform, status ,GetComponent<NavMeshAgent>());
            attack = new Attack(this, status);

            stateContext = new StateContext(this);
        }

        protected virtual void Start()
        {
            stateContext.SetState(idelState);
        }

        private void Update()
        {
            if (BattleManager.GetInstance().CurrentStats != BattleState.BATTLE) return;

            if (CheckTarget()) { SetChaseState(); }
            if (CheckMoveDistacne()) { SetAttackState(); }

            stateContext.Update();
        }

        public virtual void Initialize()
        {
            attack.canAttack = true;
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
            if (target == null) return true;
            // 적이 죽어있는가?
            if (target.status.IsDead) return true;

            return false;
        }

        protected virtual void SetAttackState()
        {
            print(gameObject.name + " : 공격상태");
            stateContext.SetState(attackState);
        }

        protected virtual void SetChaseState()
        {
            print(gameObject.name + " : 추적상태");
            stateContext.SetState(chaseState);
        }

        public virtual void DeadAction()
        {
            animator.SetTrigger("Dead");
            GetComponent<Rigidbody>().isKinematic = true;
        }

        public virtual void AttactAction()
        {
            animator.SetTrigger("Attack");
            StartCoroutine(attack.WaitAttackTime());
            StartCoroutine(attack.WaitAttackDelay());
        }
    }
}