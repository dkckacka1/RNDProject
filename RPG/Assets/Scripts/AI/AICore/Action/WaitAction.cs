using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AI
{
    public class WaitAction : ActionNode
    {
        public float duration;
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

        public override NodeStats OnUpdate()
        {
            if (Time.time - startTime > duration)
            {
                return NodeStats.SUCCESS;
            }

            return NodeStats.UPDATE;
        }
    }

}