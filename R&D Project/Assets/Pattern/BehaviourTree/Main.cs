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
            #region RepeatUtillFail Test (테스트 완료)
            /*
            {
                Node rufNode = new RepeatUntillFailDecorator();
                Node tNode1 = new WaitAction(1f);
                Node actionNode = new TenRunFailedAction();
                Node sequenceNode = new SequenceComposite();

                (rufNode as RepeatUntillFailDecorator).child = sequenceNode;

                (sequenceNode as SequenceComposite).children.Add(tNode1);
                (sequenceNode as SequenceComposite).children.Add(actionNode);


                rootNode = rufNode;
            }
            */
            #endregion
            #region Invert Test (테스트 완료)
            //{
            //    Node action1 = new SuccessReturnAction();
            //    Node action2 = new SuccessReturnAction();
            //    Node action3 = new SuccessReturnAction();
            //    Node action4 = new SuccessReturnAction();
            //    Node action5 = new SuccessReturnAction();
            //    Node invert = new InvertDecorator();

            //    (invert as InvertDecorator).child = action3;

            //    Node sequence = new SequenceComposite();

            //    (sequence as SequenceComposite).children.Add(action1);
            //    (sequence as SequenceComposite).children.Add(action2);
            //    (sequence as SequenceComposite).children.Add(invert);
            //    (sequence as SequenceComposite).children.Add(action4);
            //    (sequence as SequenceComposite).children.Add(action5);

            //    rootNode = sequence;
            //}
            #endregion
            #region Selector Test (테스트 완료)
            //{
            //    Node action1 = new SuccessReturnAction();
            //    Node action2 = new SuccessReturnAction();
            //    Node action3 = new SuccessReturnAction();
            //    Node action4 = new SuccessReturnAction();
            //    Node action5 = new SuccessReturnAction();

            //    Node invert1 = new InvertDecorator(action1);
            //    Node invert2 = new InvertDecorator(action2);
            //    Node invert3 = new InvertDecorator(action3);

            //    Node selector = new SelectorComposite();

            //    (selector as SelectorComposite).children.Add(invert1);
            //    (selector as SelectorComposite).children.Add(invert2);
            //    (selector as SelectorComposite).children.Add(invert3);
            //    (selector as SelectorComposite).children.Add(action4);
            //    (selector as SelectorComposite).children.Add(action5);

            //    rootNode = selector;
            //}
            #endregion
        }
        
        private void Start()
        {
            base.Update();
        }
    }
}
