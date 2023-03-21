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
            // ������
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
            // ���� �� 
            Initialize();
        }

        private void Update()
        {
            // �������ΰ�?
            if (BattleManager.GetInstance().CurrentStats != BattleState.BATTLE) return;

            // AI ����
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
            // ���� �׾��ִ°�?
            if (status.IsDead)      return true;

            return false;
        }

        public virtual bool CheckChaseState()
        {
            // Ÿ�ٵ� ���� ���°�?
            // Ÿ���� ���� �ִ��� Ȯ�������� ���� ����.

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