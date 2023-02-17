using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Pattern.BehaviourTree
{
    public class RepeatNumDecorator : DecoratorNode
    {
        int num;
        int index;

        public RepeatNumDecorator(int num)
        {
            this.num = num;
        }

        public override void OnStart()
        {
            index = 0;
        }

        public override void OnStop()
        {
        }

        public override State OnUpdate()
        {
            if (index < num)
            {
                if (child.Update() == State.SUCCESS)
                    index++;
                return State.RUNNING;
            }
            else
            {
                return State.SUCCESS;
            }
        }
    }
}