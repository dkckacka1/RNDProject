using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Pattern.BehaviourTree
{
    public abstract class CompositeNode : Node
    {
        [SerializeReference]
        public List<Node> children = new List<Node>();
    }

}