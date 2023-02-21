using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AI
{
    public class PlayerTree : RPG.AI.BehaviourTree
    {
        RepeatDecorator repeat = new RepeatDecorator();

        public override void SetRootNode()
        {
            repeat.child = Move();
            rootNode = repeat;
        }

        private Node Move()
        {
            SequenceComposite sNode = NodeCreater.CreateSequence();
            FindEnemyAction findAction = new FindEnemyAction();
            DistanceCheckDecorator checkDeco = new DistanceCheckDecorator();
            MoveAction moveAction = new MoveAction();
            checkDeco.child = moveAction;
            sNode.GetChilds().Add(findAction);
            sNode.GetChilds().Add(checkDeco);

            return sNode;
        }
    }
}