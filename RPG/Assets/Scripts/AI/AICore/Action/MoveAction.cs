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
        }

        public override void OnStop()
        {
        }

        public override NodeStats OnUpdate()
        {
            context.movement.Move(target);
            return NodeStats.SUCCESS;
        }
    }
}