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
        public BehaviourTree aiTree;

        public Controller target;
        public CombatStats combatStats = CombatStats.IDLE;

        // Component
        public Animator animator;
        protected Movement movement;
        protected Attack attack;
        protected Stats stats;

        // Encapsulation

        // TestCompoonent

        protected virtual void Awake()
        {
            animator = this.gameObject.GetComponent<Animator >();
            movement = this.gameObject.GetComponent<Movement>();
            attack = this.gameObject.GetComponent<Attack>();
            stats = this.gameObject.GetComponent<Stats>();
            aiTree = this.gameObject.GetComponent<BehaviourTree>();
        }

        protected virtual void Start()
        {
            aiTree.InitNode();
        }

        private void Update()
        {
            if(!stats.IsDead)
                aiTree.Play();
            /*
            switch (combatStats)
            {
                case CombatStats.IDLE:
                    break;

                case CombatStats.CHASESTART:
                    animator.SetBool("isMove", true);
                    combatStats = CombatStats.CHASE;
                    break;

                case CombatStats.CHASE:
                    if (target == null)
                    {
                        animator.SetBool("isMove", false);
                        combatStats = CombatStats.IDLE;
                        break;
                    }

                    movement.Move(target.transform);
                    if (!movement.MoveDistanceResult(target.transform))
                        combatStats = CombatStats.CHASEEND;
                    break;

                case CombatStats.CHASEEND:
                    animator.SetBool("isMove", false);
                    combatStats = CombatStats.BATTLE;
                    break;

                case CombatStats.BATTLE:
                    if (target == null || target.stats.IsDead)
                    {
                        animator.SetTrigger("Idle");
                        combatStats = CombatStats.IDLE;
                        FindNextTarget();
                        break;
                    }
                    if (!attack.canAttack) break;

                    attack.AttackTarget(target.stats);

                    break;

                case CombatStats.DEAD:
                    break;
            }
            */
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