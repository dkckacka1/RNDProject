using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    public class Leave : AgentBehaviour
    {
        public float escapeRadius;              // 탈출 반경
        public float dangerRadius;              // 최대 속도로 벗어날 반경


        public override Steering GetSteering()
        {
            Steering steering = new Steering();
            Vector3 direction = transform.position - target.transform.position; // 방향은 타겟을 바라보는 반대 방향
            float distance = direction.magnitude;   // 타겟과의 거리
            if (distance > escapeRadius)
                // 거리가 탈출 반경을 넘어섰다면 속도는 0이 된다.
                return steering;

            float reduce; // 속도 감소 수치
            if (distance < dangerRadius)
                // 현재 위험 반경안에 있다면 속도는 최대 속도가 되도록 감소수치가 0이다.
                reduce = 0;
            else
                // 현재 위험반경을 벗어났다면 탈출반경까지 도달할 때까지 속도를 감소 시킵니다.
                reduce = distance / escapeRadius * agent.maxSpeed;
            float targetSpeed = agent.maxSpeed - reduce;

            Vector3 desiredVelocity = direction;
            desiredVelocity.Normalize();
            desiredVelocity *= targetSpeed;
            steering.linear = desiredVelocity - agent.velocity;

            if (steering.linear.magnitude > agent.maxAccel)
            {
                steering.linear.Normalize();
                steering.linear *= agent.maxAccel;
            }

            return steering;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(this.transform.position, escapeRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, dangerRadius);
        }
    }
}