using RPG.Battle.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle.AI
{
    public class DeadState : State, IState
    {
        public override void Handle(Controller controller)
        {
            base.Handle(controller);
        }

        public void OnStart()
        {
            controller.animator.SetTrigger("Dead");
        }

        public void OnEnd()
        {
        }

        public void OnUpdate()
        {
        }
    }
}

