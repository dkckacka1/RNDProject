using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    public class Arrive : AgentBehaviour
    {
        public float targetRadius;          // 멈춰 서야하는 반경
        public float slowRadius;            // 멈출때까지 점차 느려질 반경

        public override Steering GetSteering()
        {
            Steering steering = new Steering();
            Vector3 direction = target.transform.position - transform.position; // 방향은 Agnet가 타겟을 바라보는 방향
            float distance = direction.magnitude; // 대상과의 거리

            if (distance < targetRadius)
                // 만약 대상과의 거리가 멈춰서야하는 반경까지 왔다면
                // 속도는 0이 됩니다.
                return steering;

            float targetSpeed; // 변경될 이동속도
            if (distance > slowRadius)
            // 만약 대상과의 거리가 느려질 반경보다 멀다면
            // 속도는 최대 속도가 된다.
            {
                targetSpeed = agent.maxSpeed;
            }
            else
            // 대상과의 거리가 느려질 반경보다 좁다면
            // 속도는 점점 느려진다.
            {
                targetSpeed = agent.maxSpeed * distance / slowRadius;
            }

            Vector3 desiredVelocity = direction;
            desiredVelocity.Normalize();
            desiredVelocity *= targetSpeed;
            // 현재 거리에 따라 속도값을 계산합니다.
            steering.linear = desiredVelocity - agent.velocity;

            if (steering.linear.magnitude > agent.maxAccel)
            // 한번에 이동해야할 위치가 최대가속도를 넘지 않도록 제한
            {
                steering.linear.Normalize();
                steering.linear *= agent.maxAccel;
            }

            return steering;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(this.transform.position, targetRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, slowRadius);
        }
    }
}