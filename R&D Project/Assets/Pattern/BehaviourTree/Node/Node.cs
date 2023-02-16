using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.BehaviourTree
{
    public abstract class Node
    {
        public abstract bool Run();
    }
}
