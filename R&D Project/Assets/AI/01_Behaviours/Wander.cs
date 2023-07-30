using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AI.Behaviour
{
    // 주변 배회
    public class Wander : Face
    {
        public float radius;    // 에이전트가 따라갈 타겟의 이동 반경
        public float offset;    // 이동 반경의 위치
        public float rate;      // 무작위 위치를 정하기 위한 범위

        public override void Awake()
        {
            target = new GameObject();                          // 에이전트가 따라갈 타겟을 만듭니다.
            target.transform.position = transform.position;
            base.Awake();
        }

        public override Steering GetSteering()
        {
            // 무작위 회전값
            float wanderOrientation = Random.Range(-1.0f, 1.0f) * rate;

            // 무작위 회전값에 현재 에이전트가 바라보는 방향만큼 더해줍니다.
            float targetOrientation = wanderOrientation + agent.orientation;

            // 에이전트가 바라 보는 방향의 방향 벡터값을 구합니다.
            Vector3 orientationVec = OriToVec(agent.orientation);

            // 에이전트가 바라보는 방향으로 오프셋만큼 이동 후 그곳에서 무작위 회전값만큼 회전후 radius만큼
            // 추가 이동한 곳이 최종 목적지 이다.
            Vector3 targetPosition = (offset * orientationVec) + transform.position;
            targetPosition = targetPosition + (OriToVec(targetOrientation) * radius);
            // 최종 목적지로 타겟을 설정합니다.
            targetAux.transform.position = targetPosition;
            Debug.DrawLine(this.transform.position, targetAux.transform.position, Color.black);

            Steering steering = new Steering();
            steering = base.GetSteering();
            // 대상을 향해 이동합니다.
            steering.linear = targetAux.transform.position - transform.position;
            steering.linear.Normalize();
            steering.linear *= agent.maxAccel;
            return steering;
        }

        // 디버깅용 함수
        private void OnDrawGizmos()
        {
            if (EditorApplication.isPlaying)
            {
                Vector3 targetPositions = (offset * OriToVec(agent.orientation)) + transform.position;
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(targetPositions, radius);
                DrawDebugLine(targetPositions, rate * 2, Color.green, radius);

            }
        }
    }
}