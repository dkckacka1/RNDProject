using System;
using System.Collections.Generic;
using RPG.Battle.Control;

namespace RPG.Battle.AI
{
    public class StateContext
    {
        public IState CurrentState
        {
            get; set;
        }
        private readonly Controller controller;

        public StateContext(Controller controller)
        {
            this.controller = controller;
        }

        public void Update()
        {
            if(CurrentState != null)
                CurrentState.OnUpdate();
        }

        public void SetState(IState state)
        {
            if (CurrentState == state)
                return;

            if(CurrentState != null)
                CurrentState.OnEnd();

            CurrentState = state;

            (CurrentState as State).Handle(controller);
            CurrentState.OnStart();
        }

    }
}
