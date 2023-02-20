using RPG.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AI
{
    public abstract class CompositeNode : Node
    {
        [SerializeReference] public List<Node> children = new List<Node>();


        public List<Node> GetChilds()
        {
            return children;
        }

        public override void Init(Context context)
        {
            this.context = context;
            foreach (var child in children)
            {
                child.Init(context);
            }
        }
    }
}