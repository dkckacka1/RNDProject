using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Control;
using RPG.Battle.Fight;

namespace RPG.Battle.AI
{
    public class AttackState : State, IState
    {
        Attack attack;

        public AttackState(Controller controller) : base(controller)
        {
            attack = controller.attack;
        }

        public void OnEnd()
        {
        }

        public void OnStart()
        {
            controller.state = CombatState.Attack;
            attack.canAttack = true;
            Debug.Log($"{controller.name} attackDelay : {attack.attackDelay}\n" +
                $"{controller.name} attacktime : {attack.attackAnimPoint}");
        }

        public void OnUpdate()
        {
            if (!attack.canAttack) return;

            controller.animator.SetTrigger("Attack");
            controller.transform.LookAt(controller.target.transform);
            attack.AttackTarget();
            controller.AttackEvent();
        }
    }
}
