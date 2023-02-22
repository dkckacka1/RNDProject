using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.AI
{
    public class PlayerTree : RPG.AI.BehaviourTree
    {
        public override void SetRootNode()
        {
            rootNode = SetStartNode();
        }

        private Node SetStartNode()
        {
            CompositeNode sequence = new SequenceComposite();
            // ���� ���۽� ����ִ� �� Ȯ��
            sequence.GetChilds().Add(new FindControllerAction());
            // ���� ���
            sequence.GetChilds().Add(SetBaattleNode());
            // �¸� ���
            sequence.GetChilds().Add(SetWinNode());
            return sequence;
        }

        private Node SetWinNode()
        {
            throw new NotImplementedException();
        }

        private Node SetBaattleNode()
        {
            DecoratorNode repeatUntillFail = new UntillFailureRepeatDecorator();
            CompositeNode selector = new SelectorComposite();
            repeatUntillFail.child = selector;
            IfDecorator ifs = new IfDecorator(() => { return false; });

            /*
                                                                   (���� �ݺ�)
                                                                   [������]
                [������]                                           [������]
                (Ÿ���� ���� �ִ°�?)
                {���ٸ�}
                {����ִ������ִ°�?)           
                {�ִٸ� Ÿ�ټ���}
             
             
             
             */


            throw new NotImplementedException();
        }
    }
}