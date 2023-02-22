using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AI
{
    public class SelectorComposite : CompositeNode
    {
        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public override NodeStats OnUpdate()
        {
            foreach (var node in children)
            {
                NodeStats stats = node.Update();

                switch (stats)
                {
                    case NodeStats.UPDATE:
                        return NodeStats.UPDATE;
                    case NodeStats.SUCCESS:
                        return NodeStats.SUCCESS;
                }
            }

            return NodeStats.FAILURE;
        }
    }
}
