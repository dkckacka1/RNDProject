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
            angular = 0.0f;             // ȸ�� ���ӵ��� ������ �ִ�. ���� ũ�� ȸ���絵 ���⿡ �׸�ŭ ȸ���ӵ��� �����Ѵ�.
            linear = new Vector3();     // �̵� ���ӵ��� ������ �ִ�. ���� ũ�� ������ �Ÿ��� �б⿡ �׸�ŭ �ӵ��� �����Ѵ�.
        }
    }
}