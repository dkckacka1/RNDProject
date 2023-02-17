using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Pattern.BehaviourTree
{
    public class SuccessReturnAction : ActionNode
    {
        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public override State OnUpdate()
        {
            print("SuccessReturnAction : 호출 성공!");
            return State.SUCCESS;
        }
    }

}