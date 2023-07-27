using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // 예상 추적 행동
    public class Pursue : Seek
    {
        public float maxPrediction;     // 최대 예상시간
        private GameObject targetAux;   // 타겟 보조 오브젝트
        private Agent targetAgent;      // 타겟 에이전트

        public override void Awake()
        {
            base.Awake();
            targetAgent = target.GetComponent<Agent>(); // 타겟이 가진 에이전트를 참조합니다.
            targetAux = target;                         // 타겟 보조 오브젝트
            target = new GameObject();                  // 실질적으로 에이전트가 추적할 게임 오브젝트입니다.
        }

        private void OnDestroy()
        {
            Destroy(target);
        }

        public override Steering GetSteering()
        {
            Vector3 direction = targetAux.transform.position - transform.position; // 방향은 Agnet가 타겟 보조 오브젝트을 바라보는 방향
            float distance = direction.magnitude;   // 거리
            float speed = agent.velocity.magnitude; // 속도
            float prediction; // 예상시간

            // 최대 예측 시간을 넘어가지 않도록 제한합니다.
            if (speed < distance / maxPrediction)
                prediction = maxPrediction;
            else
                prediction = distance / speed;
            target.transform.position = targetAux.transform.position;
            target.transform.position += targetAgent.velocity * prediction;     // 대상의 속도와 예상시간를 곱한만큼 추가로 이동합니다.
            Debug.DrawLine(this.transform.position, target.transform.position, Color.red);
            
            return base.GetSteering();
        }
    }
}