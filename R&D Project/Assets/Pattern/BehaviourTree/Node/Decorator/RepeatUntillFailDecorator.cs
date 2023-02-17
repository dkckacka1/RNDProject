using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Pattern.BehaviourTree
{
    /// <summary>
    /// 실패할때까지 액션 노드 수행
    /// </summary>
    public class RepeatUntillFailDecorator : DecoratorNode
    {
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
                case State.RUNNING:
                    return State.RUNNING;
            }

            return State.FAILURE;
        }
    }

}