using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AI
{
    public abstract class CompositeNode : Node
    {
        protected List<Node> children = new List<Node>();


        public List<Node> GetChilds()
        {
            return children;
        }
    }
}