using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Pattern.BehaviourTree
{

    /// <summary>
    /// OR 연산 컴포지트 노드
    /// </summary>
    public class Selector : Composite
    {
        public override bool Run()
        {
            foreach (Node child in GetChlid())
            {
                if (child.Run())
                    return true;
            }
            return false;
        }
    }
}
