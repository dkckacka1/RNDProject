using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // ȸ���� �� �ֵ��� ����� ����
    public class Align : AgentBehaviour
    {
        public float targetRadius;  // �ִ� ȸ����, ����� �ٶ� ȸ������ �̰��� �Ѿ�� ȸ������ �ʽ��ϴ�.
        public float slowRadius;    // �ּ� ȸ����, ȸ���ؾ��� ���� �ּ� ȸ�����̳��� ������ ȸ���ӵ��� ���� �����մϴ�.

        public override Steering GetSteering()
        {
            // �˾ƺ��� ���� ����׿� ������ �׸��ϴ�.
            DrawDebugLine(slowRadius * 2, Color.red);
            DrawDebugLine(targetRadius * 2, Color.blue);

            Steering steering = new Steering();
            // ����� �ٶ󺸱� ���� ȸ������ �����մϴ�.
            float targetOrientatin = target.GetComponent<Agent>().orientation;
            float rotation = targetOrientatin - agent.orientation;
            rotation = MapToRange(rotation);
            float rotationSize = Mathf.Abs(rotation);

            // ����� �ٶ󺸱� ���� ȸ������ �ִ� ȸ������ �Ѿ�ٸ�
            if (rotationSize > targetRadius)
            {
                // �ٶ��� �ʽ��ϴ�.
                return steering;
            }

            float targetRotation;   // �� �����Ӵ� ȸ���� �ӵ�
            if (rotationSize > slowRadius)
                // ȸ������ �ּ�ȸ���纸�� ���ٸ�
            {
                // ȸ�� �ӵ��� �ִ�ӵ��� �˴ϴ�.
                targetRotation = agent.maxRotation;
            }
            else
            {
                // �ƴϸ� ȸ���ӵ��� ���� �����մϴ�.
                targetRotation = agent.maxRotation * rotationSize / slowRadius;
            }

            // ��� �������� ȸ���ؾ��ϴ��� �����մϴ�.
            targetRotation *= rotation / rotationSize;

            // ȸ������ �־��־� ȸ���ϵ��� �մϴ�.
            steering.angular = targetRotation - agent.rotation;
            // ȸ�� ���ӵ��� �ִ� ȸ�� ���ӵ��� ���� �ʵ��� �����մϴ�.
            float angularAccel = Mathf.Abs(steering.angular);
            if (angularAccel > agent.maxAngularAccel)
            {
                steering.angular /= angularAccel;
                steering.angular *= agent.maxAngularAccel;
            }
            return steering;
        }

        // ȸ������ ���� �ٶ󺸴� ������ ���մϴ�.
        protected Vector3 DirFromAngle(float angle)
        {
            angle += transform.eulerAngles.y;
            return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
        }

        // ȸ������ �������� ����� �������� ����ϴ�.
        protected void DrawDebugLine(float angle, Color color, float Distance = 10)
        {
            Vector3 leftBoundary = DirFromAngle(-angle / 2);
            Vector3 rightBoundary = DirFromAngle(angle / 2);
            Debug.DrawLine(transform.position, transform.position + leftBoundary * Distance, color);
            Debug.DrawLine(transform.position, transform.position + rightBoundary * Distance, color);
        }

        protected void DrawDebugLine(Vector3 targetVector3, float angle, Color color, float Distance = 10)
        {
            Vector3 leftBoundary = DirFromAngle(-angle / 2);
            Vector3 rightBoundary = DirFromAngle(angle / 2);
            Debug.DrawLine(targetVector3, targetVector3 + leftBoundary * Distance, color);
            Debug.DrawLine(targetVector3, targetVector3 + rightBoundary * Distance, color);
        }
    }
}