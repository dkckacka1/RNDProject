using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Move;
using RPG.Fight;
using RPG.Character;
using System;
using RPG.AI;

namespace RPG.Control
{
    public class Controller : MonoBehaviour
    {

        public CombatStats combatStats = CombatStats.IDLE;
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
            animator = this.gameObject.GetComponent<Animator>();
            movement = this.gameObject.GetComponent<Movement>();
            attack = this.gameObject.GetComponent<Attack>();
            status = this.gameObject.GetComponent<Status>();

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

        private void LateUpdate()
        {
            status.SetHpBarPosition(transform.position);
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