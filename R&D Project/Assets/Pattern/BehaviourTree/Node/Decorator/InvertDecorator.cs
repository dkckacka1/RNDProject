using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Pattern.BehaviourTree
{
    public class InvertDecorator : DecoratorNode
    {
        public InvertDecorator(Node child = null) : base(child)
        {
        }

        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public override State OnUpdate()
        {
            switch (child.Update())
            {
                case State.SUCCESS:
                    print("���� -> ���� ��ȯ");
                    return State.FAILURE;
                case State.FAILURE:
                    print("���� -> ���� ��ȯ");
                    return State.SUCCESS;
            }

            return State.RUNNING;
        }
    }
}
