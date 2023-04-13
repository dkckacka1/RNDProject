﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Battle.Control;
using RPG.Battle.Move;

namespace RPG.Battle.AI
{
    public class ChaseState : State,IState
    {
        private Movement movement;

        public ChaseState(Controller controller) : base(controller)
        {
            movement = controller.movement;
        }

        public void OnEnd()
        {
            movement.isMove = false;
            controller.animator.SetBool("isMove", movement.isMove);
        }

        public void OnStart()
        {
            movement.isMove = true;
            controller.animator.SetBool("isMove", movement.isMove);
        }

        public void OnUpdate()
        {
            movement.MoveNav(controller.target.transform);
            //movement.Move(controller.Target.transform);
        }
    }
}
