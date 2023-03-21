﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Battle.Control;

namespace RPG.Battle.AI
{
    public class State
    {
        protected Controller controller;

        public virtual void Handle(Controller controller)
        {
            if (this.controller == null)
            {
                this.controller = controller;
            }
        }
    }
}
