using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AI
{
    public class PlayerTree : RPG.AI.BehaviourTree
    {
        RepeatDecorator repeat = new RepeatDecorator();

        private void Awake()
        {
            repeat.child = Move();

            rootNode = repeat;
        }

        private Node Move()
        {
            SequenceComposite sNode = NodeCreater.CreateSequence();
            FindEnemyAction findAction = new FindEnemyAction();
            MoveAction moveAction = new MoveAction();
            sNode.GetChilds().Add(findAction);
            sNode.GetChilds().Add(moveAction);

            return sNode;
        }
    }
}