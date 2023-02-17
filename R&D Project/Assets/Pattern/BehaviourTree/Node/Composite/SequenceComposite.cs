using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Pattern.BehaviourTree
{
    // 다음 액션이 실패할때 까지 순차적으로 실행
    public class SequenceComposite : CompositeNode
    {
        int current;

        public override void OnStart()
        {
            current = 0;
        }

        public override void OnStop()
        {
        }

        public override State OnUpdate()
        {
            var child = children[current];

            switch (child.Update())
            {
                case State.RUNNING:
                    return State.RUNNING;
                case State.FAILURE:
                    return State.FAILURE;
                case State.SUCCESS:
                    current++;
                    break;
            }

            return current == children.Count ? State.SUCCESS : State.RUNNING;
        }

    }
}