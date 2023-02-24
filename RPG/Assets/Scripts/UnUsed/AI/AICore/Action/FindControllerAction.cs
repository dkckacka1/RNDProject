using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.UnUsed
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

        public override Stats OnUpdate()
        {
            if (controller == null)
            {
                // ���� Ÿ���� ����
                context.controller.FindNextTarget();
                if (controller == null)
                {
                    // ���� ã�Ƶ� ����
                    return Stats.FAILURE;
                }
            }

            // Ÿ���� ����
            return Stats.SUCCESS;
        }
    }

}