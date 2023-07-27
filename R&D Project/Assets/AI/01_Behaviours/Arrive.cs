using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    public class Arrive : AgentBehaviour
    {
        public float targetRadius;          // ���� �����ϴ� �ݰ�
        public float slowRadius;            // ���⶧���� ���� ������ �ݰ�

        public override Steering GetSteering()
        {
            Steering steering = new Steering();
            Vector3 direction = target.transform.position - transform.position; // ������ Agnet�� Ÿ���� �ٶ󺸴� ����
            float distance = direction.magnitude; // ������ �Ÿ�

            if (distance < targetRadius)
                // ���� ������ �Ÿ��� ���缭���ϴ� �ݰ���� �Դٸ�
                // �ӵ��� 0�� �˴ϴ�.
                return steering;

            float targetSpeed; // ����� �̵��ӵ�
            if (distance > slowRadius)
            // ���� ������ �Ÿ��� ������ �ݰ溸�� �ִٸ�
            // �ӵ��� �ִ� �ӵ��� �ȴ�.
            {
                targetSpeed = agent.maxSpeed;
            }
            else
            // ������ �Ÿ��� ������ �ݰ溸�� ���ٸ�
            // �ӵ��� ���� ��������.
            {
                targetSpeed = agent.maxSpeed * distance / slowRadius;
            }

            Vector3 desiredVelocity = direction;
            desiredVelocity.Normalize();
            desiredVelocity *= targetSpeed;
            // ���� �Ÿ��� ���� �ӵ����� ����մϴ�.
            steering.linear = desiredVelocity - agent.velocity;

            if (steering.linear.magnitude > agent.maxAccel)
            // �ѹ��� �̵��ؾ��� ��ġ�� �ִ밡�ӵ��� ���� �ʵ��� ����
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