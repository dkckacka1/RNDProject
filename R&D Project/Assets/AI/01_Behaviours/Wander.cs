using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AI.Behaviour
{
    // �ֺ� ��ȸ
    public class Wander : Face
    {
        public float radius;    // ������Ʈ�� ���� Ÿ���� �̵� �ݰ�
        public float offset;    // �̵� �ݰ��� ��ġ
        public float rate;      // ������ ��ġ�� ���ϱ� ���� ����

        public override void Awake()
        {
            target = new GameObject();                          // ������Ʈ�� ���� Ÿ���� ����ϴ�.
            target.transform.position = transform.position;
            base.Awake();
        }

        public override Steering GetSteering()
        {
            // ������ ȸ����
            float wanderOrientation = Random.Range(-1.0f, 1.0f) * rate;

            // ������ ȸ������ ���� ������Ʈ�� �ٶ󺸴� ���⸸ŭ �����ݴϴ�.
            float targetOrientation = wanderOrientation + agent.orientation;

            // ������Ʈ�� �ٶ� ���� ������ ���� ���Ͱ��� ���մϴ�.
            Vector3 orientationVec = OriToVec(agent.orientation);

            // ������Ʈ�� �ٶ󺸴� �������� �����¸�ŭ �̵� �� �װ����� ������ ȸ������ŭ ȸ���� radius��ŭ
            // �߰� �̵��� ���� ���� ������ �̴�.
            Vector3 targetPosition = (offset * orientationVec) + transform.position;
            targetPosition = targetPosition + (OriToVec(targetOrientation) * radius);
            // ���� �������� Ÿ���� �����մϴ�.
            targetAux.transform.position = targetPosition;
            Debug.DrawLine(this.transform.position, targetAux.transform.position, Color.black);

            Steering steering = new Steering();
            steering = base.GetSteering();
            // ����� ���� �̵��մϴ�.
            steering.linear = targetAux.transform.position - transform.position;
            steering.linear.Normalize();
            steering.linear *= agent.maxAccel;
            return steering;
        }

        // ������ �Լ�
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