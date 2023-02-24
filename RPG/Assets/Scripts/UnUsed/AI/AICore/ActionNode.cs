using RPG.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UnUsed
{
    public abstract class ActionNode : Node
    {
        public override void Init(Context context)
        {
            this.context = context;
            stats = Stats.UPDATE;
        }
    }
}