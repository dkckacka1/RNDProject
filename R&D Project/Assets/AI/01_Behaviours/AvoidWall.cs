using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // ���� ȸ���մϴ�.
    public class AvoidWall : Seek
    {
        public float avoidDistance;     // ���� �浹�Ϸ� �Ҷ� �����Լ� �־����� �Ÿ�
        public float lookAhead;         // ���� ���� ������ ���� ����

        public override void Awake()
        {
            base.Awake();
            target = new GameObject();  // ������ ���󰡰� �� Ÿ��
        }

        public override Steering GetSteering()
        {
            Steering steering = new Steering();
            // ���� ��ġ���� ������ �ϴ� �������� ���̸� ���ϴ�.
            Vector3 position = transform.position;
            Vector3 rayVector = agent.velocity.normalized * lookAhead;
            Debug.DrawLine(this.transform.position, this.transform.position + rayVector, Color.red);

            // �ش� ���̸� ���� ����ĳ��Ʈ ��Ʈ�� �ִ��� ����մϴ�.
            Vector3 direction = rayVector;
            RaycastHit hit;
            if (Physics.Raycast(position, direction, out hit, lookAhead))
            {
                // ȸ�Ǹ� ���� ���� ���͸� ���ϰ� ���� ���Ϳ��� ȸ�� �Ÿ���ŭ �����ݴϴ�.
                position = hit.point + hit.normal * avoidDistance;
                Debug.DrawLine(hit.point, hit.point + hit.normal * avoidDistance, Color.blue);
                target.transform.position = position;
                steering = base.GetSteering();
            }

            return steering;
        }
    }
}