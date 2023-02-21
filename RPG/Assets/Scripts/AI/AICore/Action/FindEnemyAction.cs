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
                // 현재 타겟이 없음
                context.controller.FindNextTarget();
                enemy = context.controller.target;
                if (enemy == null)
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