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
using RPG.Battle.UI;
using RPG.Character.Status;


namespace RPG.Battle.Control
{
    public abstract class Controller : MonoBehaviour
    {
        // Component
        public CharacterUI ui;
        public Animator animator;
        public NavMeshAgent nav;
        public BattleStatus status;

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

        public CombatState state;

        private void Awake()
        {
            SetUp();
        }

        private void OnEnable()
        {
            status.Init();
            ui.Init();
            Init();
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Win, Win);
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Defeat, Defeat);
        }

        private void OnDisable()
        {
            status.Release();
            ui.ReleaseUI();
            Release();
            BattleManager.Instance.UnsubscribeEvent(BattleSceneState.Win, Win);
            BattleManager.Instance.UnsubscribeEvent(BattleSceneState.Defeat, Defeat);
        }

        private void Start()
        {
        }

        private void Update()
        {
            // �������ΰ�?
            if (BattleManager.Instance == null) return;
            if (BattleManager.Instance.currentBattleState != BattleSceneState.Battle) return;

            // AI ����
            if (CheckDeadState())           { stateContext.SetState(deadState); }
            else if (CheckChaseState())     { stateContext.SetState(chaseState); }
            else if (CheckAttackState())    { stateContext.SetState(attackState); }
            else if (CheckIdleState())      { stateContext.SetState(idleState); }

            stateContext.Update();
        }

        // ���� �� �ʱ�ȭ �ܰ�
        public virtual void SetUp()
        {
            movement = new Movement(transform, status, nav);
            attack = new Attack(transform ,status);

            stateContext = new StateContext(this);
            idleState = new IdelState(this);
            chaseState = new ChaseState(this);
            attackState = new AttackState(this);
            deadState = new DeadState(this);
        }

        public virtual void Init()
        {
            nav.enabled = true;
            animator.Rebind();
            animator.SetFloat("AttackSpeed", status.status.attackSpeed);
        }

        public virtual void Release()
        {
        }

        public void Win()
        {
            movement.ResetNav();
        }

        public void Defeat()
        {

        }

        public bool CheckDeadState()
        {
            // ���� �׾��ִ°�?
            if (status.IsDead)
            {
                DeadEvent();
                return true;
            }

            return false;
        }

        public bool CheckChaseState()
        {
            // Ÿ�ٵ� ���� ���°�?
            if (target == null)
            {
                // �ٸ� ���� �ִ°�?
                if (!SetTarget(out target))
                {
                    return false;
                }
            }

            // Ÿ�ٵ� ���� �ִ°�?
            if (target != null)
            {
                // Ÿ���� �׾��°�?
                if (target.status.IsDead)
                {
                    target = null;
                    // �ٸ� ���� �ִ°�?
                    if (!SetTarget(out target))
                    {
                        return false;
                    }
                }

                // ������ �Ÿ��� ���� ���� ��Ÿ����� �հ�?
                if(movement.MoveDistanceResult(target.transform))
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckAttackState()
        {
            // Ÿ�ٵ� ���� �ִ°�?
            if (target != null)
            {
                //Ÿ���� ����ִ°�?
                if(!target.status.IsDead)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckIdleState()
        {
            return true;
        }

        public void AttackEvent()
        {
            StartCoroutine(attack.WaitAttackDelay());
            StartCoroutine(attack.WaitAttackTime());
        }

        public virtual void DeadEvent()
        {
            nav.enabled = false;
            BattleManager.Instance.CharacterDead(this);
        }

        /// <summary>
        /// ã���� true ��ã���� false
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public abstract bool SetTarget(out Controller controller);
    }
}