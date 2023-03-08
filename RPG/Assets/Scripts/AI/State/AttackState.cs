using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AI
{
    public class AttackState : State, IState
    {
        public void OnEnd()
        {
        }

        public void OnStart()
        {
            controller.attack.SetTarget(controller.Target.status);
        }

        public void OnUpdate()
        {
            controller.attack.AttackTarget();

        }
    }
}
