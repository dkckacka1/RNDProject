using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // 회전할 수 있도록 만드는 행위
    public class Align : AgentBehaviour
    {
        public float targetRadius;  // 최대 회전값, 대상을 바라볼 회전값이 이값을 넘어가면 회전하지 않습니다.
        public float slowRadius;    // 최소 회전값, 회전해야할 양이 최소 회전값이내로 들어오면 회전속도가 점차 감소합니다.

        public override Steering GetSteering()
        {
            // 알아보기 쉽게 디버그용 라인을 그립니다.
            DrawDebugLine(slowRadius * 2, Color.red);
            DrawDebugLine(targetRadius * 2, Color.blue);
            Debug.DrawLine(this.transform.position, this.transform.position + Vector3.forward * 20);

            Steering steering = new Steering();
            // 대상을 바라보기 위한 회전값을 설정합니다.
            float targetOrientatin = target.GetComponent<Agent>().orientation;
            float rotation = targetOrientatin - agent.orientation;
            rotation = MapToRange(rotation);
            float rotationSize = Mathf.Abs(rotation);

            // 대상을 바라보기 위한 회전값이 최대 회전값을 넘어선다면
            if (rotationSize > targetRadius)
            {
                // 바라보지 않습니다.
                return steering;
            }

            float targetRotation;   // 한 프레임당 회전할 속도
            if (rotationSize > slowRadius)
                // 회전양이 최소회전양보다 많다면
            {
                // 회전 속도는 최대속도가 됩니다.
                targetRotation = agent.maxRotation;
            }
            else
            {
                // 아니면 회전속도는 점차 감소합니다.
                targetRotation = agent.maxRotation * rotationSize / slowRadius;
            }

            // 어느 방향으로 회전해야하는지 설정합니다.
            targetRotation *= rotation / rotationSize;

            // 회전값을 넣어주어 회전하도록 합니다.
            steering.angular = targetRotation - agent.rotation;
            // 회전 가속도가 최대 회전 가속도를 넘지 않도록 제한합니다.
            float angularAccel = Mathf.Abs(steering.angular);
            if (angularAccel > agent.maxAngularAccel)
            {
                steering.angular /= angularAccel;
                steering.angular *= agent.maxAngularAccel;
            }
            return steering;
        }

        // 회전각을 통한 바라보는 방향을 구합니다.
        private Vector3 DirFromAngle(float angle)
        {
            angle += transform.eulerAngles.y;
            return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
        }

        // 회전각을 보기좋게 디버그 라인으로 만듭니다.
        public void DrawDebugLine(float angle, Color color)
        {
            Vector3 leftBoundary = DirFromAngle(-angle / 2);
            Vector3 rightBoundary = DirFromAngle(angle / 2);
            Debug.DrawLine(transform.position, transform.position + leftBoundary * 10, color);
            Debug.DrawLine(transform.position, transform.position + rightBoundary * 10, color);
        }
    }
}