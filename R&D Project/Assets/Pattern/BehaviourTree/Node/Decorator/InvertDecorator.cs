using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Pattern.BehaviourTree
{
    public class InvertDecorator : DecoratorNode
    {
        public InvertDecorator(Node child = null) : base(child)
        {
        }

        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public override State OnUpdate()
        {
            switch (child.Update())
            {
                case State.SUCCESS:
                    print("성공 -> 실패 변환");
                    return State.FAILURE;
                case State.FAILURE:
                    print("실패 -> 성공 변환");
                    return State.SUCCESS;
            }

            return State.RUNNING;
        }
    }
}
