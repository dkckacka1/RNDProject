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
            child.Init(context);
        }
    }
}