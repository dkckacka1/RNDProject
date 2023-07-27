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
            linear = new Vector3();     // 가속도와 연관이 있다. 값이 크면 가야할 거리도 넓기에 그만큼 속도가 증가한다.
        }
    }
}