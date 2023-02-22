using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.AI
{
    public class FindControllerAction : ActionNode 
    {
        Controller controller;

        public override void OnStart()
        {
            controller = context.controller.target;
        }

        public override void OnStop()
        {
        }

        public override NodeStats OnUpdate()
        {
            if (controller == null)
            {
                // 현재 타겟이 없음
                context.controller.FindNextTarget();
                if (controller == null)
                {
                    // 새로 찾아도 없음
                    return NodeStats.FAILURE;
                }
            }

            // 타겟이 있음
            return NodeStats.SUCCESS;
        }
    }

}