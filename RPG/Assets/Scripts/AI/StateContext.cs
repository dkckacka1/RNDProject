using System;
using System.Collections.Generic;
using RPG.Control;

namespace RPG.AI
{
    public class StateContext
    {
        public IState CurrentState;

        private readonly Controller controller;

        public StateContext(Controller controller, IState state)
        {
            this.controller = controller;
            CurrentState = state;
            CurrentState.Handle(controller);
        }

        public void Update()
        {
            CurrentState.OnUpdate();
        }

        public void SetState(IState state)
        {
            CurrentState.OnEnd();

            CurrentState = state;
            CurrentState.Handle(controller);
            CurrentState.OnStart();
        }

    }
}
