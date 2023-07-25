using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // ���� �ൿ
    public class Seek : AgentBehaviour
    {
        public override Steering GetSteering()
        {
            Steering steering = new Steering();
            steering.linear = target.transform.position - transform.position;   // ������ Agnet�� Ÿ���� �ٶ󺸴� ����
            steering.linear.Normalize();    // ���� ���� ����ȭ
            steering.linear = steering.linear * agent.maxAccel; // �ӵ��� Agent�� �ִ� ���ӵ�
            return steering;
        }
    }
}