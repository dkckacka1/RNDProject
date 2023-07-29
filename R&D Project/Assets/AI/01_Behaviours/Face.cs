using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // ���� ���� ����
    public class Face : Align
    {
        protected GameObject targetAux; // Ÿ�� ���� ������Ʈ

        public override void Awake()
        {
            base.Awake();
            targetAux = target; // Ÿ�� ���� ������Ʈ�� �����մϴ�.
            target = new GameObject();  // Ÿ���� ���ο� ������Ʈ�� ����ϴ�. ������ ���Ӱ� ���� ������Ʈ�� ȸ�������� ������Ʈ�� ȸ����ŵ�ϴ�.
            target.AddComponent<Agent>();
        }

        private void OnDestroy()
        {
            Destroy(target);
        }

        public override Steering GetSteering()
        {
            // ȸ���ϱ� ���� ȸ�� ���͸� �����ݴϴ�.
            Vector3 direction = targetAux.transform.position - transform.position;

            if (direction.magnitude > 0.0f)
                // �Ÿ��� �������ִٸ�
            {
                // ȸ���ؾ��� y���� ���մϴ�.
                float targetOrientation = Mathf.Atan2(direction.x, direction.z);
                // ���Ȱ��� ��׸������� �������ݴϴ�.
                targetOrientation *= Mathf.Rad2Deg;
                // Ÿ���� ȸ������ �����մϴ�.
                target.GetComponent<Agent>().orientation = targetOrientation;
            }

            // Ÿ���� ȸ�������� �ڽŵ� ȸ���մϴ�.
            return base.GetSteering();
        }
    }
}