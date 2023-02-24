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

        // Encapsulation

        // TestCompoonent
        private Controller target;

        protected virtual void Awake()
        {
            animator = this.gameObject.GetComponent<Animator >();
            movement = this.gameObject.GetComponent<Movement>();
            attack = this.gameObject.GetComponent<Attack>();
            status = this.gameObject.GetComponent<Status>();

            stateContext = new StateContext(this);
        }

        protected virtual void Start()
        {
            stateContext.SetState(idelState);
        }

        private void Update()
        {
            if (BattleManager.GetInstance().CurrentStats != BattleState.BATTLE) return;

            FindTarget();
            CheckMoveDistacne();

            stateContext.Update();
        }

        private void CheckMoveDistacne()
        {
            if (!movement.MoveDistanceResult(target.transform))
            {
                stateContext.SetState(attackState);
            }
        }

        private void FindTarget()
        {
            if (Target == null)
            {
                FindNextTarget();
            }
        }

        public virtual void FindNextTarget()
        {
            stateContext.SetState(chaseState);
        }

        public virtual void DeadAction()
        {
            animator.SetTrigger("Dead");
            status.IsDead = true;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}