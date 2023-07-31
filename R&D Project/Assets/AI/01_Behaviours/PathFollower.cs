using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // 경로 추적 행위
    public class PathFollower : Seek
    {
        public Path path;               // 대상 경로
        public float pathOffset = 0.0f; // 현재 지점에서 얼마나 앞으로 이동할 것인지
        float currentParam;             // 현재 지점

        public override void Awake()
        {
            base.Awake();
            target = new GameObject(); // 타겟을 만듭니다. 타겟은 경로를 따라가는 오브젝트이며, 실제 에이전트는 이 타겟을 추적합니다.
            currentParam = 0f;
        }

        public override Steering GetSteering()
        {
            // 현재 경로의 어느 지점에 있는지 확인합니다.
            currentParam = path.GetParam(this.transform.position, currentParam); 
            // 현재 지점에서 이동할 거리더해 다음 지점을 계산합니다.
            float targetParam = currentParam + pathOffset;
            // 타겟을 다음 지점으로 이동시킵니다.
            target.transform.position = path.GetPosition(targetParam);

            return base.GetSteering();
        }
    }
}