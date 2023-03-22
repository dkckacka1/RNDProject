using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RPG.Battle.Control;

namespace RPG.Battle.AI
{
    public class State
    {
        protected Controller controller;
        protected Animator animator;

        public State(Controller controller)
        {
            this.controller = controller;
            this.animator = controller.animator;
        }
    }
}
