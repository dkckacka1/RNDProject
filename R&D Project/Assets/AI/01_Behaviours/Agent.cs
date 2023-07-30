using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // ������ �����ϴ� ��ü
    public class Agent : MonoBehaviour
    {
        public float maxSpeed;          // �ִ� �ӵ�
        public float maxAccel;          // �ִ� ���ӵ�
        public float maxRotation;       // �ִ� ȸ�� �ӵ�
        public float maxAngularAccel;   // �ִ� ȸ�� ���ӵ�
        public float orientation;       // ������ �� ȸ���ؾ��ϴ� ��ġ
        public float rotation;          // ȸ���ؾ��ϴ� ��
        public Vector3 velocity;        // �̵� ����
        protected Steering steering;    // ���� ����

        private void Start()
        {
            velocity = Vector3.zero;
            steering = new Steering();
        }

        // ���� velocity�� rotation ���� ���� �̵��� ȸ���� �����Ѵ�
        public virtual void Update()
        {
            Vector3 displacement = velocity * Time.deltaTime;   // �����Ӵ� ���ư����ϴ� ����
            orientation += rotation * Time.deltaTime;   // �����Ӵ� ȸ���ؾ��ϴ� ��ġ ����

            // ȸ�� ���� 0 ~ 360�� ����
            if (orientation < 0.0f)
                orientation += 360.0f;
            else if (orientation > 360.0f)
                orientation -= 360.0f;

            // �����Ӵ� �̵��ϰ� ȸ���մϴ�.
            transform.Translate(displacement, Space.World);
            transform.rotation = new Quaternion();
            transform.Rotate(Vector3.up, orientation);
        }

        // ���� ������ ���� ���� �������� �������� �����մϴ�.
        public virtual void LateUpdate()
        {
            // ���� �������׼� ��� �������� �󸶳� �̵��ϴ��� �����ɴϴ�.
            velocity += steering.linear * Time.deltaTime;
            // ���� �������׼� �󸶳� ȸ���ؾ��ϴ��� �����ɴϴ�.
            rotation += steering.angular * Time.deltaTime;

            if (velocity.magnitude > maxSpeed)
                // ���� �̵� ������ ũ�Ⱑ ���� �ִ� �ӵ��� �Ѿ�ٸ�
            {
                // �̵� ���͸� ����ȭ �� �� �ִ� �ӵ� ��ŭ �����ݴϴ�.
                velocity.Normalize();
                velocity = velocity * maxSpeed;
            }

            if (steering.angular == 0.0f)
                // ȸ�� ���� 0�̶�� rotation�� 0
            {
                rotation = 0.0f;
            }

            if (steering.linear.sqrMagnitude == 0.0f)
                // ���� �ӵ��� 0�̶��
            {
                // �̵� ���͵� 0
                velocity = Vector3.zero;
            }

            // ������ �� ���� ����� �ǽ��ϱ����� �ʱ�ȭ
            steering = new Steering();
        }


        // ������ �����մϴ�.
        // AgentBehaviour Ŭ������ �� �����Ӹ��� ������ �����մϴ�.
        public void SetSteering(Steering steering)
        {
            this.steering = steering;
        }
    }
}