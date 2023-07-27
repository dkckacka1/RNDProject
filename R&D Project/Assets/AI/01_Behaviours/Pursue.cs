using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // ���� ���� �ൿ
    public class Pursue : Seek
    {
        public float maxPrediction;     // �ִ� ����ð�
        private GameObject targetAux;   // Ÿ�� ���� ������Ʈ
        private Agent targetAgent;      // Ÿ�� ������Ʈ

        public override void Awake()
        {
            base.Awake();
            targetAgent = target.GetComponent<Agent>(); // Ÿ���� ���� ������Ʈ�� �����մϴ�.
            targetAux = target;                         // Ÿ�� ���� ������Ʈ
            target = new GameObject();                  // ���������� ������Ʈ�� ������ ���� ������Ʈ�Դϴ�.
        }

        private void OnDestroy()
        {
            Destroy(target);
        }

        public override Steering GetSteering()
        {
            Vector3 direction = targetAux.transform.position - transform.position; // ������ Agnet�� Ÿ�� ���� ������Ʈ�� �ٶ󺸴� ����
            float distance = direction.magnitude;   // �Ÿ�
            float speed = agent.velocity.magnitude; // �ӵ�
            float prediction; // ����ð�

            // �ִ� ���� �ð��� �Ѿ�� �ʵ��� �����մϴ�.
            if (speed < distance / maxPrediction)
                prediction = maxPrediction;
            else
                prediction = distance / speed;
            target.transform.position = targetAux.transform.position;
            target.transform.position += targetAgent.velocity * prediction;     // ����� �ӵ��� ����ð��� ���Ѹ�ŭ �߰��� �̵��մϴ�.
            Debug.DrawLine(this.transform.position, target.transform.position, Color.red);
            
            return base.GetSteering();
        }
    }
}