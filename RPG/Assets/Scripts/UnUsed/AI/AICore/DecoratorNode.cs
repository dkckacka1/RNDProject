using RPG.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UnUsed
{
    public abstract class DecoratorNode : Node
    {
        public Node child;

        public override void Init(Context context)
        {
            this.context = context;
            this.stats = Stats.UPDATE;
            child.Init(context);
            child.stats = Stats.UPDATE;
        }
    }
}