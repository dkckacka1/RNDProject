using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // 마주 보기 행위
    public class Face : Align
    {
        protected GameObject targetAux; // 타겟 보조 오브젝트

        public override void Awake()
        {
            base.Awake();
            targetAux = target; // 타겟 보조 오브젝트를 세팅합니다.
            target = new GameObject();  // 타겟을 새로운 오브젝트로 만듭니다. 실제로 새롭게 생긴 오브젝트의 회전값으로 에이전트를 회전시킵니다.
            target.AddComponent<Agent>();
        }

        private void OnDestroy()
        {
            Destroy(target);
        }

        public override Steering GetSteering()
        {
            // 회전하기 위해 회전 벡터를 구해줍니다.
            Vector3 direction = targetAux.transform.position - transform.position;

            if (direction.magnitude > 0.0f)
                // 거리가 떨어져있다면
            {
                // 회전해야할 y값을 구합니다.
                float targetOrientation = Mathf.Atan2(direction.x, direction.z);
                // 라디안값을 디그리값으로 변경해줍니다.
                targetOrientation *= Mathf.Rad2Deg;
                // 타겟의 회전값을 설정합니다.
                target.GetComponent<Agent>().orientation = targetOrientation;
            }

            // 타겟의 회전값으로 자신도 회전합니다.
            return base.GetSteering();
        }
    }
}