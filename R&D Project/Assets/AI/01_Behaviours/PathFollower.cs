using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // ��� ���� ����
    public class PathFollower : Seek
    {
        public Path path;               // ��� ���
        public float pathOffset = 0.0f; // ���� �������� �󸶳� ������ �̵��� ������
        float currentParam;             // ���� ����

        public override void Awake()
        {
            base.Awake();
            target = new GameObject(); // Ÿ���� ����ϴ�. Ÿ���� ��θ� ���󰡴� ������Ʈ�̸�, ���� ������Ʈ�� �� Ÿ���� �����մϴ�.
            currentParam = 0f;
        }

        public override Steering GetSteering()
        {
            // ���� ����� ��� ������ �ִ��� Ȯ���մϴ�.
            currentParam = path.GetParam(this.transform.position, currentParam); 
            // ���� �������� �̵��� �Ÿ����� ���� ������ ����մϴ�.
            float targetParam = currentParam + pathOffset;
            // Ÿ���� ���� �������� �̵���ŵ�ϴ�.
            target.transform.position = path.GetPosition(targetParam);

            return base.GetSteering();
        }
    }
}