using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // ��� ������ �⺻ Ŭ����
    public class AgentBehaviour : MonoBehaviour
    {
        public GameObject target;   // ������Ʈ�� �ֽ��ϰ� �ִ� ���
        protected Agent agent; // ������ ������ ������Ʈ
        public float weight = 1.0f; // ���� ����ġ
        public int priority = 1;    // �켱����

        public virtual void Awake()
        {
            agent = gameObject.GetComponent<Agent>();
        }

        public virtual void Update()
        {
            // ���� ������Ʈ�� ������ �����մϴ�.
            // ������Ʈ�� ������ ���� �켱������ �ο��մϴ�.
            agent.SetSteering(GetSteering(), priority);
        }

        // �⺻�����δ� �ƹ��͵� �������� �ʴ´�.
        public virtual Steering GetSteering()
        {
            return new Steering();
        }

        // �� ���� ���� �� �� ���� ȸ�� ������ ã�´�.
        public float MapToRange(float rotation)
        {
            // ȸ������ 360�� �Ѿ�� �ʵ��� �մϴ�.
            rotation %= 360.0f;

            if (Mathf.Abs(rotation) > 180.0f)
            // ȸ������ -180 ~ 180 ���� ������ �� �ֵ����մϴ�.
            {
                if (rotation < 0.0f)
                    rotation += 360.0f;
                else
                    rotation -= 360.0f;
            }

            return rotation;
        }

        // ������ ���Ⱚ�� ���ͷ� �ٲ��ݴϴ�.
        public Vector3 OriToVec(float orientation)
        {
            // ex) OriToVec(90) z �� ���� ���� ����
            Vector3 vector = Vector3.zero;
            vector.x = Mathf.Sin(orientation * Mathf.Deg2Rad) * 1.0f;
            vector.z = Mathf.Cos(orientation * Mathf.Deg2Rad) * 1.0f;
            return vector.normalized;
        }
    }
}