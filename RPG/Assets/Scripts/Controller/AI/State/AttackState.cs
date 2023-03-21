using RPG.Battle.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle.AI
{
    public class AttackState : State, IState
    {
        public void OnEnd()
        {
        }

        public void OnStart()
        {
            controller.attack.SetTarget(controller.target.status);
        }

        public void OnUpdate()
        {
            Debug.Log("Attack--");
            controller.attack.AttackTarget();
        }
    }
}
