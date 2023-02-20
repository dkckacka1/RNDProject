using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AI
{
    [CreateAssetMenu(fileName ="NewBT",menuName ="CreateAction/DistanceAction",order = int.MinValue)]
    public class DistanceAction : ActionNode
    {
        public float minimumDistance = 0f;
        public Transform myTransform ,targetTransform;

        public override void OnStart()
        {
            minimumDistance = context.stats.attackRange;
            myTransform = context.transform;
            targetTransform = context.controller.target.transform;
        }

        public override void OnStop()
        {
            Debug.Log($"{myTransform.name}�� {targetTransform.name}�� �Ÿ��� �������ϴ�.");
        }

        public override NodeStats OnUpdate()
        {
            float distance = Vector3.Distance(myTransform.position, targetTransform.position);

            if (distance <= minimumDistance)
            {
                return NodeStats.SUCCESS;
            }

            Debug.Log($"{myTransform.name}�� {targetTransform.name}�� �Ÿ��� �ٴϴ�.");
            return NodeStats.UPDATE;
        }
    }
}