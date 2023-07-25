using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // 추적 행동
    public class Seek : AgentBehaviour
    {
        public override Steering GetSteering()
        {
            Steering steering = new Steering();
            steering.linear = target.transform.position - transform.position;   // 방향은 Agnet가 타겟을 바라보는 방향
            steering.linear.Normalize();    // 방향 벡터 정규화
            steering.linear = steering.linear * agent.maxAccel; // 속도는 Agent의 최대 가속도
            return steering;
        }
    }
}