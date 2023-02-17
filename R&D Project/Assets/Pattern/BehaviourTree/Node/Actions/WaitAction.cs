using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Pattern.BehaviourTree
{
    public class WaitAction : ActionNode
    {
        public float duration = 1f;
        float startTime;

        public WaitAction(float duration = 1f)
        {
            this.duration = duration;
        }

        public override void OnStart()
        {
            startTime = Time.time;
        }

        public override void OnStop()
        {
        }

        public override State OnUpdate()
        {
            if (Time.time - startTime > duration)
            {
                return State.SUCCESS;
            }

            return State.RUNNING;
        }
    }

}