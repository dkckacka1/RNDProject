using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // ����� ȸ���Ѵ�.
    public class AvoidAgent : AgentBehaviour
    {
        public float collisionRadius = 0.4f;
        GameObject[] targets;   // ȸ���� ������Ʈ��

        void Start()
        {
            // ȸ���� ����� 'Agent' �±װ� ���� ���� ������Ʈ (Agent ������Ʈ�� �־����)
            targets = GameObject.FindGameObjectsWithTag("Agent");
        }

        // ȸ���� ������Ʈ ����Ʈ���� ���� �������� �����
        // ����� ��������� ��, ���� ����� ������Ʈ�� ���� �ӵ� ���� ���� ��θ� �����Ѵ�.
        public override Steering GetSteering()
        {
            Steering steering = new Steering();
            float shortestTime = Mathf.Infinity; // �浹 �ð� ���ſ�
            GameObject firstTarget = null;      // ù��°�� �浹�� ���
            float firstMinSeparation = 0.0f;    // ù��°�� �浹�� ������ �ּ� �и� �Ÿ�
            float firstDistance = 0.0f;         // ù��°�� �浹�� ������ ���� �Ÿ�
            Vector3 firstRelativePos = Vector3.zero;    // ù��°�� �浹�� ������ ������� �Ÿ�
            Vector3 firstRelativeVel = Vector3.zero;    // ù��°�� �浹�� ������ ������� �ӵ�

            // ��� ȸ�� ����� ��ȸ�մϴ�.
            foreach (GameObject t in targets)
            {
                Vector3 relativePos;
                // ��� ������Ʈ�� �����մϴ�.
                Agent targetAgent = t.GetComponent<Agent>();
                // ��� ȸ�� ��󿡰Լ� ������� ��ġ�� �ӵ��� ����մϴ�.
                relativePos = t.transform.position - transform.position;
                Vector3 relativeVel = targetAgent.velocity - agent.velocity;
                float relativeSpeed = relativeVel.magnitude;

                // �浹 ���� �ð��� �����մϴ�.
                // ���� ȸ�Ǵ���� ��ġ�� ���ϰ� �ִ� ������ ������ ����մϴ�.
                // ������ �ӵ��� ������ �ݴϴ�. �ӵ��� ���� ���� �浹 ���� �ð��� ���� ���ɴϴ�.
                // ���ΰ� �ݴ� �������� ���ؼ� ������ ���� �� �ֱ⿡ �׻� ����� �������� -1�� �����ݴϴ�.
                float timeToCollision = Vector3.Dot(relativePos, relativeVel);
                timeToCollision /= relativeSpeed * relativeSpeed * -1;
                float distance = relativePos.magnitude;
                // ������ �浹�� �ּ� �ݰ��� üũ�մϴ�.
                float minSeparation = distance - relativeSpeed * timeToCollision;

                // ���� �浹�� �ּ� �ݰ��� �ڽ��� �浹 �ݰ� �̳��� �ƴ϶��
                // �浹���� �ʴ� ����̱⿡ Ÿ�� �������� �Ѿ�ϴ�.
                if (minSeparation > 2 * collisionRadius)
                    continue;

                Debug.DrawLine(this.transform.position, t.transform.position, Color.green);

                if (timeToCollision > 0.0f && timeToCollision < shortestTime)
                    // ���� ������ �ڽŰ� �浹�� ����� üũ�մϴ�.
                {
                    shortestTime = timeToCollision; // ���� ���� �浹�� �ð��� �����մϴ�.
                    firstTarget = t;
                    firstMinSeparation = minSeparation;
                    firstDistance = distance;
                    firstRelativePos = relativePos;
                    firstRelativeVel = relativeVel;
                }
            }

            // �浹 ����� ���ٸ� ������ �־ �ȴ�.
            if (firstTarget == null)
                return steering;

            // ������ �ּ� �и� �ݰ�� �Ÿ��� ����Ͽ� �����ġ���� �־��� �� �ӵ����� ����ϸ鼭 �־����� �����մϴ�.
            if (firstMinSeparation <= 0.0f || firstDistance < 2 * collisionRadius)
            {
                firstRelativePos = firstTarget.transform.position;
            }
            else
            {
                firstRelativePos += firstRelativeVel * shortestTime;
            }

            firstRelativePos.Normalize();
            // ù��°�� �浹�� ����� �ݴ� �������� �־����ϴ�.
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