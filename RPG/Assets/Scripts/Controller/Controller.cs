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

        // Component
        public Animator animator;
        public Status status;
        public NavMeshAgent nav;

        // AI State
        public StateContext stateContext;
        public IdelState idleState;
        public ChaseState chaseState;
        public AttackState attackState;
        public DeadState deadState;

        // Behaviour
        public Movement movement;
        public Attack attack;

        // Battle
        public Controller target;



        private void Awake()
        {
            // 생성자
            stateContext = new StateContext(this);

            idleState = new IdelState();
            chaseState = new ChaseState();
            attackState = new AttackState();
            deadState = new DeadState();

            movement = new Movement(transform, status, nav);
            attack = new Attack(status);
        }

        private void Start()
        {
            // 시작 시 
            Initialize();
        }

        private void Update()
        {
            // 전투중인가?
            if (BattleManager.GetInstance().CurrentStats != BattleState.BATTLE) return;

            // AI 수행
            if (CheckDeadState())           { stateContext.SetState(deadState); }
            else if (CheckChaseState())     { stateContext.SetState(chaseState); }
            else if (CheckAttackState())    { stateContext.SetState(attackState); }
            else if (CheckIdleState())      { stateContext.SetState(idleState); }

            stateContext.Update();
        }

        public void Initialize()
        {
            stateContext.SetState(idleState);
        }

        public virtual bool CheckDeadState()
        {
            // 나는 죽어있는가?
            if (status.IsDead)      return true;

            return false;
        }

        public virtual bool CheckChaseState()
        {
            // 타겟된 적이 없는가?
            // 타겟할 적이 있는지 확인했지만 적이 없다.

            return false;
        }

        public virtual bool CheckAttackState()
        {
            return false;
        }

        public virtual bool CheckIdleState()
        {
            return false;
        }
    }
}