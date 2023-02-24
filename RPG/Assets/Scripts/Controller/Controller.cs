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
        public Controller target;
        public CombatStats combatStats = CombatStats.IDLE;
        public StateContext stateContext;

        public IdelState idelState = new IdelState();

        // Component
        public Animator animator;
        protected Movement movement;
        protected Attack attack;
        protected Status stats;

        // Encapsulation

        // TestCompoonent

        protected virtual void Awake()
        {
            animator = this.gameObject.GetComponent<Animator >();
            movement = this.gameObject.GetComponent<Movement>();
            attack = this.gameObject.GetComponent<Attack>();
            stats = this.gameObject.GetComponent<Status>();

            stateContext = new StateContext(this, idelState);
        }

        protected virtual void Start()
        {
        }

        private void Update()
        {
            stateContext.Update();
        }

        public virtual void FindNextTarget()
        {
            print("컨트롤러");
        }

        public void DeadAction()
        {
            animator.SetTrigger("Dead");
            stats.IsDead = true;
            GetComponent<Rigidbody>().isKinematic = true;
            Controller controller;
            if (controller = GetComponent<PlayerController>())
            {
                print("플레이어 확인");
                BattleManager.GetInstance().livePlayers.Remove((PlayerController)controller);
            }
            else if (controller = GetComponent<EnemyController>())
            {
                print("몬스터 확인");
                BattleManager.GetInstance().liveEnemys.Remove((EnemyController)controller);
            }

        }

        public void Attack(IDamagedable damagedable)
        {
            if (damagedable.IsDead) return;
            if (!attack.canAttack) return;

            attack.AttackTarget(damagedable);
            animator.SetBool("isMove", false);
            animator.SetTrigger("Attack");
        }
    }
}