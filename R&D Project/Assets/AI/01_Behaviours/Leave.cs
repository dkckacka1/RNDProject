using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    public class Leave : AgentBehaviour
    {
        public float escapeRadius;              // Ż�� �ݰ�
        public float dangerRadius;              // �ִ� �ӵ��� ��� �ݰ�


        public override Steering GetSteering()
        {
            Steering steering = new Steering();
            Vector3 direction = transform.position - target.transform.position; // ������ Ÿ���� �ٶ󺸴� �ݴ� ����
            float distance = direction.magnitude;   // Ÿ�ٰ��� �Ÿ�
            if (distance > escapeRadius)
                // �Ÿ��� Ż�� �ݰ��� �Ѿ�ٸ� �ӵ��� 0�� �ȴ�.
                return steering;

            float reduce; // �ӵ� ���� ��ġ
            if (distance < dangerRadius)
                // ���� ���� �ݰ�ȿ� �ִٸ� �ӵ��� �ִ� �ӵ��� �ǵ��� ���Ҽ�ġ�� 0�̴�.
                reduce = 0;
            else
                // ���� ����ݰ��� ����ٸ� Ż��ݰ���� ������ ������ �ӵ��� ���� ��ŵ�ϴ�.
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