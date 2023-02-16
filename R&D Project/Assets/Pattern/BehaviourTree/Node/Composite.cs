using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Pattern.BehaviourTree
{
    public abstract class Composite : Node
    {
        List<Node> childList = new List<Node>();

        protected List<Node> GetChlid() { return childList; }
        public void AddChild(Node node)
        {
            childList.Add(node);
        }
    }
}
