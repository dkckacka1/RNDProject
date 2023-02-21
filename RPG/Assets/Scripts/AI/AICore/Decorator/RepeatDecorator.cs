using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AI
{
    public class RepeatDecorator : DecoratorNode
    {
        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public override NodeStats OnUpdate()
        {
            child.Update();
            return NodeStats.UPDATE;
        }
    }

}