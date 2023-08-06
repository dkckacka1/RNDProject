using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // 행위를 수행하는 주체
    public class Agent : MonoBehaviour
    {
        public float maxSpeed;          // 최대 속도
        public float maxAccel;          // 최대 가속도
        public float maxRotation;       // 최대 회전 속도
        public float maxAngularAccel;   // 최대 회전 가속도
        public float orientation;       // 프레임 당 회전해야하는 수치
        public float rotation;          // 회전해야하는 양
        public Vector3 velocity;        // 이동 벡터
        protected Steering steering;    // 현재 행위

        public float priorityThreshold = 0.2f;  // 우선순위 임계치 입니다. 각 행위의 속도 회전 값이 최소 이 수치를 넘어야지 실행됩니다.
        private Dictionary<int, List<Steering>> groups; // 행위 그룹입니다.

        private void Start()
        {
            velocity = Vector3.zero;
            steering = new Steering();
            groups = new Dictionary<int, List<Steering>>();
        }

        // 현재 velocity와 rotation 값에 따라 이동과 회전을 수행한다
        public virtual void Update()
        {
            Vector3 displacement = velocity * Time.deltaTime;   // 프레임당 나아가야하는 벡터
            orientation += rotation * Time.deltaTime;   // 프레임당 회전해야하는 수치 조정

            // 회전 값을 0 ~ 360도 고정
            if (orientation < 0.0f)
                orientation += 360.0f;
            else if (orientation > 360.0f)
                orientation -= 360.0f;

            // 프레임당 이동하고 회전합니다.
            transform.Translate(displacement, Space.World);
            transform.rotation = new Quaternion();
            transform.Rotate(Vector3.up, orientation);
        }

        // 현재 행위에 따라 다음 프레임의 움직임을 갱신합니다.
        public virtual void LateUpdate()
        {
            steering = GetPrioritySteering(); // 우선순위를 고려한 행위 수치를 얻습니다.
            groups.Clear();

            // 현재 행위한테서 어느 방향으로 얼마나 이동하는지 가져옵니다.
            velocity += steering.linear * Time.deltaTime;
            // 현재 행위한테서 얼마나 회전해야하는지 가져옵니다.
            rotation += steering.angular * Time.deltaTime;

            if (velocity.magnitude > maxSpeed)
            // 현재 이동 벡터의 크기가 현재 최대 속도를 넘어선다면
            {
                // 이동 벡터를 정규화 한 후 최대 속도 만큼 곱해줍니다.
                velocity.Normalize();
                velocity = velocity * maxSpeed;
            }

            if (steering.angular == 0.0f)
            // 회전 값이 0이라면 rotation도 0
            {
                rotation = 0.0f;
            }

            if (steering.linear.sqrMagnitude == 0.0f)
            // 현재 속도가 0이라면
            {
                // 이동 벡터도 0
                velocity = Vector3.zero;
            }

            // 프레임 당 행위 계산을 실시하기위한 초기화
            steering = new Steering();
        }


        // 행위를 세팅합니다.
        // AgentBehaviour 클래스가 매 프레임마다 행위를 갱신합니다.
        public void SetSteering(Steering steering, float weight)
        {
            // 현재 가중치만큼 속도와 회전 양을 결정합니다.
            this.steering.linear += (weight * steering.linear);
            this.steering.angular += (weight * steering.angular);
        }

        // 우선순위를 고려한 행위를 세팅합니다.
        public void SetSteering(Steering steering, int priority)
        {
            if (!groups.ContainsKey(priority))
            {
                groups.Add(priority, new List<Steering>());
            }
            groups[priority].Add(steering);
        }

        // 우선순위에 의한 행의 수치를 얻습니다.
        private Steering GetPrioritySteering()
        {
            Steering steering = new Steering();
            // 우선순위를 위한 임계치를 설정합니다.
            // sqrMagnitude 과 비교하기 위해 제곱합니다.
            // sqrMagnitude 를 사용하는 이유는 magnitude 보다 연산량이 적어서..
            float sqrThreshold = priorityThreshold * priorityThreshold;
            foreach (List<Steering> group in groups.Values)
            {
                steering = new Steering();
                foreach (Steering singleSteering in group)
                    // 같은 동일 우선순위 그룹내의 행위를 모두 연산합니다.
                {
                    steering.linear += singleSteering.linear;
                    steering.angular += singleSteering.angular;
                }

                if (steering.linear.sqrMagnitude > sqrThreshold ||
                    Mathf.Abs(steering.angular) > priorityThreshold)
                    // 만약 그룹내의 행위의 속도 및 회전양이 최소 임계치 영역을 넘었다면
                    // 해당 행위를 수행합니다.
                {
                    return steering;
                }
            }
            return steering;
        }
    }
}