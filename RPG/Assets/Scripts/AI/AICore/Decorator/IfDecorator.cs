using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.AI
{
    public class IfDecorator : DecoratorNode
    {
        public delegate bool Function();

        Function function;

        public IfDecorator(Function funcion)
        {
            this.function = funcion;
        }

        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public override NodeStats OnUpdate()
        {
            if (function.Invoke())
            {
                return child.Update();
            }

            return NodeStats.FAILURE;
        }
    }
}
