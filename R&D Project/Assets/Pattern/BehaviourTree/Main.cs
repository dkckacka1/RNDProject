using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.BehaviourTree
{
    class Main : BehaviourTree
    {
        private void Awake()
        {
            Node ttNode1 = new WaitAction(1.5f);
            Node ttNode2 = new WaitAction(1.5f);
            Node ttNode3 = new WaitAction(1.5f);
            Node dNode = new DebugAction("Test");
            Node dNode1 = new DebugAction("Test1111111");
            Node dNode2 = new DebugAction("Test22222222");
            Node rnNode = new RepeatNumDecorator(3);
            Node sNode = new SequenceComposite();
            (sNode as SequenceComposite).children.Add(dNode1);
            (sNode as SequenceComposite).children.Add(ttNode1);
            (sNode as SequenceComposite).children.Add(dNode2);
            (sNode as SequenceComposite).children.Add(ttNode2);
            (sNode as SequenceComposite).children.Add(dNode);
            (sNode as SequenceComposite).children.Add(ttNode3);
            (rnNode as RepeatNumDecorator).child = sNode;


            rootNode = rnNode;
        }

        private void Start()
        {
            base.Update();
        }
    }
}
