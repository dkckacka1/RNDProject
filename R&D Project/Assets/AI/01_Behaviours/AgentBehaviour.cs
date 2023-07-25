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


        public virtual void Awake()
        {
            agent = gameObject.GetComponent<Agent>();
        }

        public virtual void Update()
        {
            // ���� ������Ʈ�� ������ �����մϴ�.
            agent.SetSteering(GetSteering());
        }

        // �⺻�����δ� �ƹ��͵� �������� �ʴ´�.
        public virtual Steering GetSteering()
        {
            return new Steering();
        }

        public float MapToRange(float rotation)
        {
            rotation %= 360.0f;
            if (Mathf.Abs(rotation) > 180.0f)
            {
                if (rotation < 0.0f)
                    rotation += 360.0f;
                else
                    rotation -= 360.0f;
            }
            return rotation;
        }
    }
}