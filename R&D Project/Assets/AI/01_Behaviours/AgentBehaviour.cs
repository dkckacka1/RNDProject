using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // 모든 행위의 기본 클래스
    public class AgentBehaviour : MonoBehaviour
    {
        public GameObject target;   // 에이전트가 주시하고 있는 대상
        protected Agent agent; // 행위를 조작할 에이전트
        public float weight = 1.0f; // 행위 가중치
        public int priority = 1;    // 우선순위

        public virtual void Awake()
        {
            agent = gameObject.GetComponent<Agent>();
        }

        public virtual void Update()
        {
            // 현재 에이전트의 행위를 수행합니다.
            // 에이전트에 행위에 대한 우선순위도 부여합니다.
            agent.SetSteering(GetSteering(), priority);
        }

        // 기본적으로는 아무것도 수행하지 않는다.
        public virtual Steering GetSteering()
        {
            return new Steering();
        }

        // 두 방향 값을 뺀 후 실제 회전 방향을 찾는다.
        public float MapToRange(float rotation)
        {
            // 회전값이 360을 넘어가지 않도록 합니다.
            rotation %= 360.0f;

            if (Mathf.Abs(rotation) > 180.0f)
            // 회전값이 -180 ~ 180 으로 유지될 수 있도록합니다.
            {
                if (rotation < 0.0f)
                    rotation += 360.0f;
                else
                    rotation -= 360.0f;
            }

            return rotation;
        }

        // 현재의 방향값을 벡터로 바꿔줍니다.
        public Vector3 OriToVec(float orientation)
        {
            // ex) OriToVec(90) z 축 양의 방향 벡터
            Vector3 vector = Vector3.zero;
            vector.x = Mathf.Sin(orientation * Mathf.Deg2Rad) * 1.0f;
            vector.z = Mathf.Cos(orientation * Mathf.Deg2Rad) * 1.0f;
            return vector.normalized;
        }
    }
}