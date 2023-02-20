using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AI
{
    public class DistanceAction : ActionNode
    {
        float minimumDistance = 0f;
        Transform myTransform ,targetTransform;

        public DistanceAction(float minimumDistance, Transform mineTrasnfrom, Transform targetTransform)
        {
            this.minimumDistance = minimumDistance;
            this.myTransform = mineTrasnfrom;
            this.targetTransform = targetTransform;
        }

        public override void OnStart()
        {
        }

        public override void OnStop()
        {
            Debug.Log($"{myTransform.name}와 {targetTransform.name}의 거리가 가깝습니다.");
        }

        public override NodeStats OnUpdate()
        {
            float distance = Vector3.Distance(myTransform.position, targetTransform.position);

            if (distance <= minimumDistance)
            {
                return NodeStats.SUCCESS;
            }

            Debug.Log($"{myTransform.name}와 {targetTransform.name}의 거리가 멉니다.");
            return NodeStats.UPDATE;
        }
    }
}