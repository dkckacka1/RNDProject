using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // 벽을 회피합니다.
    public class AvoidWall : Seek
    {
        public float avoidDistance;     // 벽과 충돌하려 할때 벽에게서 멀어지는 거리
        public float lookAhead;         // 내가 가는 방향의 레이 길이

        public override void Awake()
        {
            base.Awake();
            target = new GameObject();  // 실제로 따라가게 될 타겟
        }

        public override Steering GetSteering()
        {
            Steering steering = new Steering();
            // 현재 위치에서 가고자 하는 방향으로 레이를 쏩니다.
            Vector3 position = transform.position;
            Vector3 rayVector = agent.velocity.normalized * lookAhead;
            Debug.DrawLine(this.transform.position, this.transform.position + rayVector, Color.red);

            // 해당 레이를 통해 레이캐스트 히트가 있는지 계산합니다.
            Vector3 direction = rayVector;
            RaycastHit hit;
            if (Physics.Raycast(position, direction, out hit, lookAhead))
            {
                // 회피를 위해 법선 벡터를 구하고 법선 벡터에서 회피 거리만큼 곱해줍니다.
                position = hit.point + hit.normal * avoidDistance;
                Debug.DrawLine(hit.point, hit.point + hit.normal * avoidDistance, Color.blue);
                target.transform.position = position;
                steering = base.GetSteering();
            }

            return steering;
        }
    }
}