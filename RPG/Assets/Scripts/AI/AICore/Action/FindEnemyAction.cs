using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.AI
{
    public class FindEnemyAction : ActionNode
    {
        public EnemyController enemy;

        public override void OnStart()
        {
            context.controller.target = BattleManager.GetInstance().ReturnNearDistanceController<EnemyController>(context.transform);
            enemy = (EnemyController)context.controller.target;
        }

        public override void OnStop()
        {
        }

        public override NodeStats OnUpdate()
        {
            if (enemy == null)
            {
                Debug.Log("없음!");
                return NodeStats.FAILURE;
            }

            Debug.Log("에너미 찾기");
            return NodeStats.SUCCESS;
        }
    }

}