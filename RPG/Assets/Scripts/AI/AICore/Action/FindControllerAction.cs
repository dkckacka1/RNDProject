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
                // ���� Ÿ���� ����
                context.controller.FindNextTarget();
                if (controller == null)
                {
                    // ���� ã�Ƶ� ����
                    return NodeStats.FAILURE;
                }
            }

            // Ÿ���� ����
            return NodeStats.SUCCESS;
        }
    }

}