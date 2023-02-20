using RPG.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AI
{
    public class UntillFailureRepeatDecorator : DecoratorNode
    {
        public UntillFailureRepeatDecorator(Node child)
        {
            this.child = child;
        }



        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public override NodeStats OnUpdate()
        {
            return child.Update() == NodeStats.FAILURE ? NodeStats.SUCCESS : NodeStats.UPDATE;
        }
    }
}