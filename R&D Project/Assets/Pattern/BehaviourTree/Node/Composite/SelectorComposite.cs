using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Pattern.BehaviourTree
{

    // 하위 노드에서 SUCCESS가 나올때 까지 DFS
    public class SelectorComposite : CompositeNode
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
                case State.SUCCESS:
                    return State.SUCCESS;
                case State.FAILURE:
                    {
                        current++;
                        break;
                    }
            }

            return current == children.Count ? State.FAILURE : State.RUNNING;
        }
    }

}