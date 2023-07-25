using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // Agent의 이동 및 회전 값을 가지고 있다.
    public class Steering 
    {
        public float angular;
        public Vector3 linear;

        public Steering()
        {
            angular = 0.0f;             // 회전값
            linear = new Vector3();     // 어디로 이동할지
        }
    }
}