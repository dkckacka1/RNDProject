using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.AI
{
    public class FindEnemyAction : ActionNode
    {
        public Controller enemy;

        public override void OnStart()
        {

        }

        public override void OnStop()
        {
        }

        public override NodeStats OnUpdate()
        {
            if (enemy == null)
            {
                // ���� Ÿ���� ����
                context.controller.FindNextTarget();
                enemy = context.controller.target;
                if (enemy == null)
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