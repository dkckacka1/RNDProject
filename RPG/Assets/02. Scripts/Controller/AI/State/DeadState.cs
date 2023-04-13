using RPG.Battle.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle.AI
{
    public class DeadState : State, IState
    {
        public DeadState(Controller controller) : base(controller)
        {
        }

        public void OnStart()
        {
            controller.battleStatus.currentState = CombatState.Dead;
            //Debug.Log(controller.name + "이 죽었습니다.");
            //controller.DeadEvent();
            //controller.StopAttack();
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

