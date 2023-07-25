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


        public virtual void Awake()
        {
            agent = gameObject.GetComponent<Agent>();
        }

        public virtual void Update()
        {
            // 현재 에이전트의 행위를 수행합니다.
            agent.SetSteering(GetSteering());
        }

        // 기본적으로는 아무것도 수행하지 않는다.
        public virtual Steering GetSteering()
        {
            return new Steering();
        }

        public float MapToRange(float rotation)
        {
            rotation %= 360.0f;
            if (Mathf.Abs(rotation) > 180.0f)
            {
                if (rotation < 0.0f)
                    rotation += 360.0f;
                else
                    rotation -= 360.0f;
            }
            return rotation;
        }
    }
}