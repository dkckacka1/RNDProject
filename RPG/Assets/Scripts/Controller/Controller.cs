using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Move;
using RPG.Fight;
using RPG.Character;
using System;

namespace RPG.Control
{
    public class Controller : MonoBehaviour
    {
        public Controller target;
        public CombatStats combatStats = CombatStats.IDLE;

        // Component
        public Animator animator;
        protected Movement movement;
        protected Attack attack;
        protected Stats stats;

        // Encapsulation

        // TestCompoonent

        private void Awake()
        {
            animator = GetComponent<Animator >();
            movement = GetComponent<Movement>();
            attack = GetComponent<Attack>();
            stats = GetComponent<Stats>();
        }


        private void Update()
        {
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
        }

        protected virtual void FindNextTarget()
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