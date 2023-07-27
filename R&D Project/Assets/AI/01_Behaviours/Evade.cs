using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    // 예상 회피 행동
    // 부모 클래스가 Flee라는걸 제외하고는 Pursue와 동일하다.
    public class Evade : Flee
    {
        public float maxPrediction;
        private GameObject targetAux;
        private Agent targetAgent;

        public override void Awake()
        {
            base.Awake();
            targetAgent = target.GetComponent<Agent>();
            targetAux = target;
            target = new GameObject();
        }

        private void OnDestroy()
        {
            Destroy(target);
        }

        public override Steering GetSteering()
        {
            Vector3 direction = targetAux.transform.position - transform.position;
            float distance = direction.magnitude;
            float speed = agent.velocity.magnitude;
            float prediction;
            if (speed < distance / maxPrediction)
                prediction = maxPrediction;
            else
                prediction = distance / speed;
            target.transform.position = targetAux.transform.position;
            target.transform.position += targetAgent.velocity * prediction;
            Debug.DrawLine(this.transform.position, target.transform.position, Color.red);
            return base.GetSteering();
        }
    }
}