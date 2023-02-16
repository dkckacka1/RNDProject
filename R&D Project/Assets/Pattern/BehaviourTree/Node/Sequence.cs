using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Pattern.BehaviourTree
{
    /// <summary>
    /// AND 연산 컴포지트 노드
    /// </summary>
    public class Sequence : Composite
    {
        public override bool Run()
        {
            foreach (Node child in GetChlid())
            {
                if (!child.Run())
                    return false;
            }

            return true;
        }
    }
}
