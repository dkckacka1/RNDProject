using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Pattern.BehaviourTree
{
    public class RepeatDecorator : DecoratorNode
    {

        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public override State OnUpdate()
        {
            child.Update();
            return State.RUNNING;
        }
    }

}