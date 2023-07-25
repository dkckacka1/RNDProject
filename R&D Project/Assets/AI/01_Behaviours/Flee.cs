using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // ȸ�� ����
    public class Flee : AgentBehaviour
    {
        public override Steering GetSteering()
        {
            Steering steering = new Steering();
            steering.linear = transform.position - target.transform.position; // ������ Ÿ���� Agent�� �ٶ󺸴� ����
            steering.linear.Normalize();
            steering.linear = steering.linear * agent.maxAccel;
            return steering;
        }
    }
}