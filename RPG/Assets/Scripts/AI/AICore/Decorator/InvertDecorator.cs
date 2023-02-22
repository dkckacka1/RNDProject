using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.AI
{
    /// <summary>
    /// 하위 노드가 SUCCESS 일시 FAILURE
    /// 하위 노드가 FAILURE 일시 SUCCESS
    /// 하위 노드가 UPDATE 일시 UPDATE
    /// </summary>
    public class InvertDecorator : DecoratorNode
    {
        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public override NodeStats OnUpdate()
        {
            switch (child.Update())
            {
                case NodeStats.FAILURE:
                    return NodeStats.SUCCESS;
                case NodeStats.SUCCESS:
                    return NodeStats.FAILURE;
            }

            return NodeStats.UPDATE;
        }
    }
}
