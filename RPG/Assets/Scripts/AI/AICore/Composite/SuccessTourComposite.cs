using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AI
{
    [CreateAssetMenu(fileName ="NewBT",menuName ="CreateComposite/SuccessTour",order = int.MinValue)]
    public class SuccessTourComposite : CompositeNode
    {
        public NodeStats statsTour;

        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public override NodeStats OnUpdate()
        {
            statsTour = NodeStats.SUCCESS;
            foreach (var node in children)
            {
                NodeStats stats = node.Update();

                if (stats == NodeStats.UPDATE)
                {
                    statsTour = NodeStats.UPDATE;
                }

                if (stats == NodeStats.FAILURE)
                {
                    return NodeStats.FAILURE;
                }
            }

            return statsTour;
        }
    }

}