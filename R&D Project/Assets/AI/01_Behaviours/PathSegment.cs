using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // 경로를 나타내는 데이터 타입
    public class PathSegment
    {
        public Vector3 a;   // 출발 지점
        public Vector3 b;   // 도착 지점

        public PathSegment() : this(Vector3.zero, Vector3.zero)
        {
        }

        public PathSegment(Vector3 a, Vector3 b)
        {
            this.a = a;
            this.b = b;
        }
    }

}