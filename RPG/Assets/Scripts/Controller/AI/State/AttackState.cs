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
        Controller target;
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
            target = controller.target;
            controller.attack.SetTarget(controller.target.status);
        }

        public void OnUpdate()
        {
            if (!attack.canAttack) return;

            animator.SetTrigger("Attack");
            controller.transform.LookAt(target.transform);
            attack.AttackTarget();
            controller.AttackEvent();
        }
    }
}
