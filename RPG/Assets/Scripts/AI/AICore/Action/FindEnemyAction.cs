using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.AI
{
    [CreateAssetMenu(fileName = "NewAction", menuName = "CreateAction/FindEnemyAction", order = int.MaxValue)]
    public class FindEnemyAction : ActionNode
    {
        EnemyController enemy;

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
                return NodeStats.FAILURE;
            }

            Debug.Log("에너미 찾기");
            return NodeStats.SUCCESS;
        }
    }

}