using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // Agent�� �̵� �� ȸ�� ���� ������ �ִ�.
    public class Steering 
    {
        public float angular;
        public Vector3 linear;

        public Steering()
        {
            angular = 0.0f;             // ȸ����
            linear = new Vector3();     // ���� �̵�����
        }
    }
}