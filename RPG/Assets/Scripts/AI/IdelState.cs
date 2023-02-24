using RPG.Control;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AI
{
    public class IdelState : IState
    {
        private Controller controller;

        public void Handle(Controller controller)
        {
            if (controller == null)
            {
                this.controller = controller;
            }
        }

        public void OnEnd()
        {
            Debug.Log(controller.transform.name + " IdelEnd");
        }

        public void OnStart()
        {
            Debug.Log(controller.transform.name + " IdelStart");
        }

        public void OnUpdate()
        {
            Debug.Log(controller.transform.name + " IdelUpdate");
        }
    }
}
