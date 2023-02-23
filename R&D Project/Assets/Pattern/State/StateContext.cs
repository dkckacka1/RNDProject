using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.State
{
    public class StateContext
    {
        public IState CurrentState
        {
            get; set;
        }

        private readonly Controller _controller;

        public StateContext(Controller controller)
        {
            _controller = controller;
        }

        public void Transition()
        {
            CurrentState.Handle(_controller);
        }

        public void Transition(IState state)
        {
            CurrentState = state;
            CurrentState.Handle(_controller);
        }
    }
}
