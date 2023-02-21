using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Move;

namespace RPG.AI
{
    public class MoveAction : ActionNode
    {
        Transform target;
        public override void OnStart()
        {
            target = context.controller.target.transform;
            context.controller.animator.SetBool("isMove", true);
        }

        public override void OnStop()
        {
            context.controller.animator.SetBool("isMove", false);
        }

        public override NodeStats OnUpdate()
        {
            context.movement.Move(target);
            return NodeStats.SUCCESS;
        }
    }
}