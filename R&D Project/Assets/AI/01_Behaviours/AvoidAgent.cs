using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // 대상을 회피한다.
    public class AvoidAgent : AgentBehaviour
    {
        public float collisionRadius = 0.4f;
        GameObject[] targets;   // 회피할 오브젝트들

        void Start()
        {
            // 회피할 대상은 'Agent' 태그가 붙은 게임 오브젝트 (Agent 컴포넌트가 있어야함)
            targets = GameObject.FindGameObjectsWithTag("Agent");
        }

        // 회피할 에이전트 리스트에서 가장 가까운것을 고려해
        // 충분히 가까워졌을 때, 가장 가까운 에이전트의 현재 속도 값에 따라 경로를 수정한다.
        public override Steering GetSteering()
        {
            Steering steering = new Steering();
            float shortestTime = Mathf.Infinity; // 충돌 시간 갱신용
            GameObject firstTarget = null;      // 첫번째로 충돌할 대상
            float firstMinSeparation = 0.0f;    // 첫번째로 충돌할 대상과의 최소 분리 거리
            float firstDistance = 0.0f;         // 첫번째로 충돌할 대상과의 현재 거리
            Vector3 firstRelativePos = Vector3.zero;    // 첫번째로 충돌할 대상과의 상대적인 거리
            Vector3 firstRelativeVel = Vector3.zero;    // 첫번째로 충돌할 대상과의 상대적인 속도

            // 모든 회피 대상을 순회합니다.
            foreach (GameObject t in targets)
            {
                Vector3 relativePos;
                // 대상 에이전트를 참조합니다.
                Agent targetAgent = t.GetComponent<Agent>();
                // 모든 회피 대상에게서 상대적인 위치와 속도를 계산합니다.
                relativePos = t.transform.position - transform.position;
                Vector3 relativeVel = targetAgent.velocity - agent.velocity;
                float relativeSpeed = relativeVel.magnitude;

                // 충돌 예상 시간을 정의합니다.
                // 현재 회피대상의 위치와 향하고 있는 방향의 내적을 계산합니다.
                // 내적에 속도를 나누어 줍니다. 속도가 빠를 수록 충돌 예상 시간이 적게 나옵니다.
                // 서로가 반대 방향으로 향해서 음수가 나올 수 있기에 항상 양수로 나오도록 -1을 곱해줍니다.
                float timeToCollision = Vector3.Dot(relativePos, relativeVel);
                timeToCollision /= relativeSpeed * relativeSpeed * -1;
                float distance = relativePos.magnitude;
                // 대상과의 충돌할 최소 반경을 체크합니다.
                float minSeparation = distance - relativeSpeed * timeToCollision;

                // 만약 충돌할 최소 반경이 자신의 충돌 반경 이내가 아니라면
                // 충돌하지 않는 대상이기에 타겟 선정에서 넘어갑니다.
                if (minSeparation > 2 * collisionRadius)
                    continue;

                Debug.DrawLine(this.transform.position, t.transform.position, Color.green);

                if (timeToCollision > 0.0f && timeToCollision < shortestTime)
                    // 가장 빠르게 자신과 충돌할 대상을 체크합니다.
                {
                    shortestTime = timeToCollision; // 가장 빨리 충돌할 시간을 갱신합니다.
                    firstTarget = t;
                    firstMinSeparation = minSeparation;
                    firstDistance = distance;
                    firstRelativePos = relativePos;
                    firstRelativeVel = relativeVel;
                }
            }

            // 충돌 대상이 없다면 가만히 있어도 된다.
            if (firstTarget == null)
                return steering;

            // 대상과의 최소 분리 반경과 거리를 계산하여 대상위치에서 멀어질 지 속도까지 계산하면서 멀어질지 선택합니다.
            if (firstMinSeparation <= 0.0f || firstDistance < 2 * collisionRadius)
            {
                firstRelativePos = firstTarget.transform.position;
            }
            else
            {
                firstRelativePos += firstRelativeVel * shortestTime;
            }

            firstRelativePos.Normalize();
            // 첫번째로 충돌할 대상의 반대 방향으로 멀어집니다.
            steering.linear = -firstRelativePos * agent.maxAccel;
            return steering;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, collisionRadius);
        }
    }
}