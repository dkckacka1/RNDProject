using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Pattern.BehaviourTree
{
    public abstract class DecoratorNode : Node
    {
        public Node child;
    }

}