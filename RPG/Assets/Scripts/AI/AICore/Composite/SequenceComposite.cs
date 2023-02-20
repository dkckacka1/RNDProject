using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AI
{
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

        public override NodeStats OnUpdate()
        {
            if (children.Count == 0)
                return NodeStats.SUCCESS;

            var Node = children[current];

            switch (Node.Update())
            {
                case NodeStats.UPDATE:
                    return NodeStats.UPDATE;
                case NodeStats.FAILURE:
                    return NodeStats.FAILURE;
                case NodeStats.SUCCESS:
                    break;
            }

            current++;
            return children.Count == current ? NodeStats.SUCCESS : NodeStats.FAILURE;
        }
    }
}