using RPG.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AI
{
    public abstract class DecoratorNode : Node
    {
        public Node child;

        public override void Init(Context context)
        {
            this.context = context;
            this.stats = NodeStats.UPDATE;
            child.Init(context);
            child.stats = NodeStats.UPDATE;
        }
    }
}