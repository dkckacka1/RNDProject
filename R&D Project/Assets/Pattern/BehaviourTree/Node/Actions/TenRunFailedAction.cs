using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Pattern.BehaviourTree
{
    public class TenRunFailedAction : ActionNode
    {
        int current = 0;
        public override void OnStart()
        {
            current++;
            print($"{current} 번 호출");
        }

        public override void OnStop()
        {
        }

        public override State OnUpdate()
        {
            if (current >= 10)
            {
                print($"액션 종료");
                return State.FAILURE;
            }

            return State.SUCCESS;
        }
    }
}
