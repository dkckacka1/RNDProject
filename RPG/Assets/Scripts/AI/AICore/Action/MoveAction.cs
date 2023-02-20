using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Move;

namespace RPG.AI
{
    [CreateAssetMenu(fileName = "NewBT", menuName = "CreateAction/MoveAction", order = int.MinValue)]
    public class MoveAction : ActionNode
    {
        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public override NodeStats OnUpdate()
        {
            context.movement.Move(context.controller.target.transform);
            return NodeStats.SUCCESS;
        }
    }
}